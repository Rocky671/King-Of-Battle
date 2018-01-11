using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMover : MonoBehaviour {

	// private variables
	private Transform tf;
	private TankData data;

	// Use this for initialization
	void Start () {
		data = GetComponent<TankData> ();
		tf = GetComponent<Transform> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 moveVector = Vector3.zero;

		if (gameObject.name == "Turret1") {
			// Change moveVector based on keypress
			tf.Rotate (Vector3.back * Input.GetAxis ("HorizontalTurret") * data.turnSpeed);

			// actually move (Google unity Transform.rotate)
			SendMessage ("Move", moveVector, SendMessageOptions.DontRequireReceiver);
		}
		else if (gameObject.name == "Turret2") {
			// Change moveVector based on keypress ( Player Two)
			tf.Rotate (Vector3.back * Input.GetAxis ("TwoTurret") * data.turnSpeed);	

			// actually move (Google unity Transform.rotate)
			SendMessage ("Move", moveVector, SendMessageOptions.DontRequireReceiver);
		}
	}
}