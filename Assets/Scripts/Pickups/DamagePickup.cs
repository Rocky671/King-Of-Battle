using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : PickUpManager {
	private int damageBoost = 50;
	private float timeElapsed = 10;

	Shooter shoot;

	/// <summary>
	/// accesses shooter component and changes shell damage according to variables
	/// waits the time elapsed variable so the boost will disappear accordingly
	/// </summary>
	/// <param name="other">Other.</param>
	public override void pickUp(Collider other){
		shoot = other.GetComponent<Shooter> ();
		// adds 50 to player damage
		if (shoot != null) {
			shoot.shellDamage += damageBoost;

			// if pickUp exceeds 10 seconds
			// remove damageBoost
			Invoke ("damageReset", timeElapsed);
		}
	}

	// removes the damage boost
	void damageReset(){
		shoot.shellDamage -= damageBoost;
	}
}
