using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour {
	public float initialDelay;
	public float timeBetweenSpawns;
	public GameObject enemyToSpawn;
	public int maxEnemies;
	public static int numberOfEnemiesSpawned;

	[Header("Gizmos Data")]
	public Vector3 scale;


	// Use this for initialization
	void Start () {
		StartSpawning ();


	}

	// Update is called once per frame
	void Update () {



	}

	// draws a blue cube in place of the invisible area, then draws a ray pointing to where the enemies would spawn
	void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Vector3 offset = new Vector3 (0, scale.y / 2.0f, 0);
		Gizmos.DrawCube (transform.position + offset, scale);
		Gizmos.DrawRay (transform.position + offset, transform.forward);
	}

	// draws a black cube in place of the invisible area, then draws a ray pointing to where the enemies would spawn
	void OnDrawGizmosSelected(){
		Gizmos.color = Color.black;
		Vector3 offset = new Vector3 (0, scale.y / 2.0f, 0);
		Gizmos.DrawRay (transform.position + offset, transform.forward);
		Gizmos.DrawCube (transform.position + offset, scale);
	}

	/// <summary>
	/// Spawns enemies. if number of enemies spawned is less than the max amount of enemies, keep spawning
	/// </summary>
	public void Spawner(){
		if (numberOfEnemiesSpawned < maxEnemies){
			Instantiate (enemyToSpawn, transform.position, transform.rotation);

			numberOfEnemiesSpawned++;
		}
	}


	/// <summary>
	/// Stops the spawn
	/// </summary>
	public void StopSpawning(){
		CancelInvoke ("Spawner");
	}

	/// <summary>
	/// Starts spawning
	/// </summary>
	public void StartSpawning (){
		InvokeRepeating ("Spawner", initialDelay, timeBetweenSpawns);
	}

}