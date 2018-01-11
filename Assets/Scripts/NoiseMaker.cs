using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMaker : MonoBehaviour {
	private CharacterController cc;

	public bool playerNoise;

	// Use this for initialization
	void Start () {
		// character controller
		cc = GetComponent <CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		// if player is moving (velocity), he is making noise
		if (cc.velocity.magnitude > 0) {
			playerNoise = true;
		} 
		else 
		{
			playerNoise = false;
		}
	}
}
