using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kamikaze : AIController {

	[Header("Kamikaze")]
	public GameObject explosionRadius;

	protected override void specialMoves()
	{
		
	}

	// when the player collides with the tank
	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			Debug.Log ("Player has entered");
			// remove 20 health from player and destroy the tank
			other.GetComponent<Health> ().health -= 20;
			Destroy (gameObject);
		}
	}

}
