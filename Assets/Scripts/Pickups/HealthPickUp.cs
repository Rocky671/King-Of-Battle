using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : PickUpManager {
	// heals player for 50 upon pickup
	private int healthBoost = 50;

	/// <summary>
	/// accesses health component and then heals player upon pickup
	/// </summary>
	/// <param name="other">Other.</param>
	public override void pickUp(Collider other){
		Health h = other.GetComponent<Health> ();
		if (h != null) {
			h.giveHealth(healthBoost);
		}
	}
}