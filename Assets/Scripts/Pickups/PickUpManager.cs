using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(Collider))]

public abstract class PickUpManager : MonoBehaviour {
	private Transform tf;

	public float timeToRespawn = 30;

	[Header("Spin Speed")]
	public int SpinSpeed = 90;

	[SerializeField]
	private UnityEvent OnTrigger;

	private new Collider collider;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider> ();
		collider.isTrigger = true;
		tf = GetComponent<Transform> ();

	}

	// Update is called once per frame
	void Update () {
		Spin ();
	}

	/// <summary>
	/// Once anything picks up the item, destroy game object
	/// </summary>
	/// <param name="other">Other.</param>
	void OnTriggerEnter(Collider other) {
		pickUp (other);

		// Call any other functions 
		OnTrigger.Invoke ();

		// Invokes the pickupactive function and waits the respawn time
		Invoke ("PickupActive", timeToRespawn);

		// deactivates pickup
		gameObject.SetActive (false);

	}

	// sets pickup to active
	void PickupActive(){
		gameObject.SetActive (true);
	}

	// spins the game object
	void Spin(){
		tf.Rotate(0, SpinSpeed * Time.deltaTime, 0);
	}

	// pick up inheritance class
	public virtual void pickUp(Collider other){

	}

}