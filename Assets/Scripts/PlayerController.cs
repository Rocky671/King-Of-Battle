using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Private Variables
	private TankData data;
	private Transform tf;
	private Shooter shooter;


	// Use this for initialization
	void Start () {
		data = GetComponent<TankData> ();
		tf = GetComponent<Transform> ();
		shooter = GetComponent<Shooter> ();
	}
	
	// Update is called once per frame
	void Update () {
		// create the variable but set no movement
		Vector3 moveVector = Vector3.zero;

		float speed = 0;

		if (gameObject.name == "Player One") {
			// if the key press is less than 0, use forward speed
			if (Input.GetAxis ("Vertical") < 0) {
				speed = data.forwardSpeed;
				// if the key press is greater than zero, use reverse speed
			} else if (Input.GetAxis ("Vertical") > 0) {
				speed = data.reverseSpeed;
			}

			// Change moveVector based on keypress
			// speed will go based off of speed
			moveVector = tf.forward * Input.GetAxis ("Vertical") * speed;
			tf.Rotate (Vector3.up * Input.GetAxis ("Horizontal") * data.turnSpeed);

			// this is will actually move the tank
			SendMessage ("Move", moveVector, SendMessageOptions.DontRequireReceiver);

			// if user presses the appropriate fire button, call shoot function from shooter script
			if (Input.GetAxis ("Fire1") > 0) {
				shooter.Shoot ();
			}
		}

		///////////////////////////////// Player Two /////////////////////////////////////
		/// 
		// if the key press is less than 0, use forward speed
		else if (gameObject.name == "Player Two") {
			if (Input.GetAxis ("PlayerTwoVertical") < 0) {
				speed = data.forwardSpeed;
				// if the key press is greater than zero, use reverse speed
			} else if (Input.GetAxis ("PlayerTwoVertical") > 0) {
				speed = data.reverseSpeed;
			}

			// Change moveVector based on keypress
			// speed will go based off of speed
			moveVector = tf.forward * Input.GetAxis ("PlayerTwoVertical") * speed;
			tf.Rotate (Vector3.up * Input.GetAxis ("PlayerTwoHorizontal") * data.turnSpeed);

			// this is will actually move the tank
			SendMessage ("Move", moveVector, SendMessageOptions.DontRequireReceiver);

			// if user presses the appropriate fire button, call shoot function from shooter script
			if (Input.GetAxis ("Fire2") > 0) {
				shooter.Shoot ();
			}
		}	
	}
}
