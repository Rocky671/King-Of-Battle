using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// if user clicks menu button, game will load menu
	public void mainMenu(){
		SceneManager.LoadScene ("Menu");
	}

	// if the user clicks start game, loads game
	public void startGame(){
		SceneManager.LoadScene ("GeneratedMap");
	}

	// quits the application
	public void quitGame(){
		Application.Quit();
	}
}
