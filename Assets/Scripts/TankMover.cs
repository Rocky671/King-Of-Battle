using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]

public class TankMover : MonoBehaviour {

	[HideInInspector]
	public Transform tf;
	[HideInInspector]
	public CharacterController cc;
	[HideInInspector]
	public TankData data;

	// Use this for initialization
	void Start () {
		tf = GetComponent<Transform> ();
		cc = GetComponent<CharacterController> ();
		data = GetComponent<TankData> ();
	}

	// will actually move the tank
	public void Move (Vector3 speed){
		cc.SimpleMove (speed);
	}

	// rotates the tank 
	public void Rotate ( float turnSpeedAndDirection ) {
		tf.Rotate (new Vector3(0,turnSpeedAndDirection * Time.deltaTime,0));
	}

	public void RotateTowards(Vector3 newDirection){
		// create variable to hold how we Want to end up turned
		Quaternion goalRotation;
		// set variable to how we need to turn in order to look down newDirection
		goalRotation = Quaternion.LookRotation (newDirection);
		// Rotate from our current rotation towards our target rotation
		tf.rotation = Quaternion.SlerpUnclamped(tf.rotation, goalRotation, data.turnSpeed * Time.deltaTime);
	}
}
