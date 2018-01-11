using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

	[Header("Tank Parts")]
	public Transform tankCannon;
	public GameObject tankShell;

	[Header("Shooter Data")]
	public float shootForce;
	public float shotsPerSecond;
	public float shellLifeSpan = 2.0f;

	private float nextShotTime;

	[Header("Bullet Damage")]
	public int shellDamage = 10;

	// Use this for initialization
	void Start () {
		nextShotTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		
	}

	public void Shoot() {
		// If it's not time for the next shot yet, return (don't shoot)
		if (Time.time < nextShotTime) {
			return;
		}

		// spawns a shell from cannon
		GameObject spawnedShell = Instantiate (tankShell, tankCannon.position, tankCannon.rotation * Quaternion.Euler(new Vector3(0, 0, 180)));
		// adds force to the shell
		spawnedShell.GetComponent<Rigidbody> ().AddForce (tankCannon.right * shootForce);
		// the spawned shell will take the amount of damage from shell damage
		spawnedShell.GetComponent <BulletDamage> ().setDamage (shellDamage);
		// this will destroy the shell after a set amount of time
		Destroy (spawnedShell, shellLifeSpan);
		// limits the amount of shots user can make 
		nextShotTime = Time.time + 1.0f / shotsPerSecond;
	}
}
