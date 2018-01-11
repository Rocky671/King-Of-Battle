using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPickup : PickUpManager {
	private int speedBoost = 5;

	/// <summary>
	/// accesses speed variable and then adds speedBoost value to current speed 
	/// </summary>
	/// <param name="other">Other.</param>
	public override void pickUp(Collider other){
		TankData data = other.GetComponent<TankData> ();
		data.forwardSpeed += speedBoost;
		data.reverseSpeed += speedBoost;

	}

}