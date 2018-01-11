using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager gm;
	public GameObject player;

	public Text currentScore;
	public int score;


	// Before any Start
	void Awake() {
		if (gm != null) {
			Destroy (gameObject);
		} else {
			gm = this;
			DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		// starting score will be 0
		score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Adds a point to score. If user reaches 10 points, user wins game (loads win screen)
	/// </summary>
	public void addScore(){
		score++;
		updatePointText ();

		if (score >= 10) {
			SceneManager.LoadScene ("Win");
		}
	}

	// updates the text box with a point
	void updatePointText(){
		currentScore.text = "Kills: " + score;
	}
}
