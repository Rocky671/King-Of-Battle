using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIStates{idle, patrol, chase, flee}
public enum AIPersonality{Kamikaze, Guerilla, Coward, Artillery}
public enum AIAvoidanceStates{normal, turnToAvoid, moveToAvoid}

public class AIController : MonoBehaviour {

	public AIStates AIState;
	[HideInInspector]
	public TankData data;
	[HideInInspector]
	public Transform playerTransform;
	protected Health currentHealth;

	// private variables
	private NoiseMaker noise;
	private Transform tf;

	[Header("Patrol Data")]
	private List<Transform> waypoints = new List<Transform>();
	public int currentWaypoint;
	public float stopDistance;
	public float pauseAtWaypoint = 5.0f;
	public float lastWaypointTime;

	[Header("Sense Data")]
	public bool canSeePlayer = false;
	public float lastStateChangeTime;
	public float hearingRadius;
	public float sightDistance;
	public float fieldOfView;

	[Header("Flee Data")]
	public float fleeDistance = 5.0f;

	[Header("Avoidance Data")]
	public AIAvoidanceStates avoidState = AIAvoidanceStates.normal;
	public float avoidMovementTime = 1.0f;
	public float lastAvoidanceStateChangeTime;
	public float avoidDistance = 3.0f;
	public float timeToChangeState;

	// Use this for initialization
	void Start () {
		// Get the waypoints
		waypoints.Clear();
		// for each game object with the tag "Waypoint"
		foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag ("Waypoint"))
		{
			// add waypoints 
			waypoints.Add (waypoint.transform);
		}

		// Get Components
		data = GetComponent<TankData> ();
		tf = GetComponent<Transform> ();
		noise = GameManager.gm.player.GetComponent<NoiseMaker> ();
		playerTransform = GameManager.gm.player.GetComponent<Transform> ();
		AIState = AIStates.idle;
		currentHealth = GetComponent<Health> ();

		lastStateChangeTime = Time.time;
		lastAvoidanceStateChangeTime = Time.time;
		lastWaypointTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		// switch case for ai states
		switch (AIState) {
		// idle state
		case AIStates.idle:
			doIdle ();
			// changes state to chase if the player is seen or heard
			if (playerIsSeen () || playerMakesNoise ()) {
				changeState (AIStates.chase);
			}
			// changes state to patrol if too much time has passed without seeing player
			else if (Time.time - lastStateChangeTime > timeToChangeState ) {
				changeState (AIStates.patrol);
			}
			break;
			// patrol state
		case AIStates.patrol:
			doPatrol ();
			DoSeekPlayer ();

			// when health reaches below 20, chases player again
			if (currentHealth.health <= 20) {
				changeState (AIStates.chase);
				// if health is below 50, flees from player
			} else if (currentHealth.health <= 50) {
				changeState (AIStates.flee);
			}

			// if the time exceeds 15 seconds, goes idle state
			if (Time.time - lastStateChangeTime > 15.0f) {
				changeState (AIStates.idle);
			}
			break;
			// chase state
		case AIStates.chase:
			doChase ();
			// if health is below 50, flees from player
			if (currentHealth.health <= 50)
				changeState (AIStates.flee);
			break;
			// flee state
		case AIStates.flee:
			DoFleePlayer ();
			// when health reaches below 20, chases player again
			if (currentHealth.health <= 20)
				changeState (AIStates.chase);
			break;
		}
	}

	void changeState(AIStates newState){
		// time last changed state
		lastStateChangeTime = Time.time;
		// saves the state
		AIState = newState;
	}

	void ChangeAvoidanceState (AIAvoidanceStates newState){
		// sets variable to Time.time
		lastAvoidanceStateChangeTime = Time.time;
		// changes the avoidstate to the new state
		avoidState = newState;
	}

	void DoSeekPlayer(){

		switch (avoidState) {
		case AIAvoidanceStates.normal:

			// find direction to player
			Vector3 newDirection = playerTransform.position - tf.position;

			// rotates to new direction towards player
			SendMessage ("RotateTowards", newDirection);

			// moves forward with the forward speed in inspector
			SendMessage ("Move", transform.forward * data.forwardSpeed);
			break;
		case AIAvoidanceStates.turnToAvoid:
			// rotates 
			SendMessage("Rotate", data.turnSpeed);
			// if raycast hits nothing, change state to movetoavoid
			if (CanMoveForward ()) {
				ChangeAvoidanceState (AIAvoidanceStates.moveToAvoid);
			}
			break;	
		case AIAvoidanceStates.moveToAvoid:
			// move forward
			SendMessage("Move", transform.forward * data.forwardSpeed);
			// if raycast hits something, turn to avoid
			if (!CanMoveForward ()) {
				ChangeAvoidanceState (AIAvoidanceStates.turnToAvoid);
			}
			// if time has passed, move to normal state
			if (Time.time - lastAvoidanceStateChangeTime > avoidMovementTime) {
				ChangeAvoidanceState (AIAvoidanceStates.normal);
			}
			break;
		}
	}

	bool CanMoveForward(){
		// Raycast forward
		RaycastHit hitdata;
		// shoots a raycast from position, stores data into hitdata, only goes as far as avoiddistance variable
		if (Physics.Raycast (tf.position, tf.forward, out hitdata, avoidDistance)) {
			return false;
		} else {
			return true;
		}
	}

	void DoFleePlayer(){
		goToWaypoint();
	}

	void doIdle()
	{
		// Does nothing
	}

	void doPatrol()
	{
		goToWaypoint ();
		// if player is seen or makes noise, chase the player
		if (playerIsSeen () || playerMakesNoise()) {
			AIState = AIStates.chase;
		}
	}

	void doChase()
	{
		// create the variable but set no movement
		Vector3 moveVector = Vector3.zero;

		// sets speed to the tank data in inspector
		float speed = data.forwardSpeed;

		// take players position minus the tanks position
		moveVector = playerTransform.position - tf.position;
		// rotates to new direction towards player
		SendMessage ("RotateTowards", moveVector.normalized * speed, SendMessageOptions.DontRequireReceiver);

		// moves the tank
		SendMessage ("Move", moveVector.normalized * speed, SendMessageOptions.DontRequireReceiver);

		// special personality moves
		specialMoves ();
	}

	// list of waypoints for tanks
	void goToWaypoint(){
		// create the variable but set no movement
		Vector3 moveVector = Vector3.zero;
		// sets speed to the tank data in inspector
		float speed = data.forwardSpeed;

		// takes the time elapsed and minus's the lastwaypointtime
		// if the time is greater than or equal to the time elapsed
		if (Time.time - lastWaypointTime >= pauseAtWaypoint) {
			// take players position minus the tanks position
			moveVector = (waypoints [Random.Range (0, waypoints.Count)].transform.position - tf.position);

			// sets the lastwaypointtime to the current time
			lastWaypointTime = Time.time;
		}

		// moves the tank
		SendMessage ("Move", moveVector.normalized * speed, SendMessageOptions.DontRequireReceiver);
		// rotates to new direction towards player
		SendMessage ("RotateTowards", moveVector.normalized * speed, SendMessageOptions.DontRequireReceiver);

	}

	protected bool playerMakesNoise(){
		// if the player is moving, he is making noise
		if (Vector3.Distance (playerTransform.position, tf.position) < hearingRadius && noise.playerNoise) {
			return true;
		}
		return false;
	}

	// checks if player is seen by the enemy
	protected bool playerIsSeen()
	{
		Vector3 vectorToTarget = playerTransform.position - tf.position;
		float angleToTarget = Vector3.Angle (tf.forward, vectorToTarget);

		// checks if ray hits player
		RaycastHit mark;

		// checks if enemy has collided with player
		if (angleToTarget <= fieldOfView) {
			if (Physics.Raycast (data.transform.position, vectorToTarget, out mark, sightDistance)) {
				if (mark.collider.gameObject == GameManager.gm.player) {
					return true;
				}
			}
		}
		return false;
	}

	protected virtual void specialMoves()
	{
		// has tanks shooting
		SendMessage ("Shoot");
	}
}
