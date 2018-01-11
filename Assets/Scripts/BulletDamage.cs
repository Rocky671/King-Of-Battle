using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour {

	private int damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// gets the damage
	public int getDamage() {
		return damage;
	}

	// sets the damage
	public void setDamage(int tempDamage){
		damage = tempDamage;

	}
}
