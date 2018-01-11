using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// death function to destroy game object and add score to player
	public void death(){
		Destroy (gameObject);
		GameManager.gm.addScore ();
	}
}
