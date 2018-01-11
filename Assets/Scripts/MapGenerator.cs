using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator: MonoBehaviour {

	public int cols;
	public int rows;
	public Room[,] grid;
	public float tileWidth = 50;
	public float tileHeight = 50;
	[Header("Seed or No Seed")]
	public int seed = 420699001;
	public bool mapOfTheDay = false;

	public List<GameObject> roomPrefabs;
	public GameObject[] playerPrefab;
	public GameObject[] canvasses;

	// Use this for initialization
	void Start () {
		if (mapOfTheDay) {
			// Find unix timestamp (kind of)
			seed = (int)(System.DateTime.UtcNow.Subtract(new System.DateTime(1970, 1, 1))).TotalDays;
		}
		Random.InitState (seed);

		GenerateGrid ();
		playerSpawner ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void GenerateGrid(){
		// Setup our grid array
		grid = new Room[cols,rows];

		// One for each row
		for (int currentRow = 0; currentRow < rows; currentRow++) {
			// then... once for each column in that row
			for (int currentCol = 0; currentCol < cols; currentCol++) {
				// Set our position based on our tile height width and position
				Vector3 position = new Vector3 (currentCol * tileWidth, 0, currentRow * tileHeight);
				// Instantiate the tile
				GameObject temp = Instantiate (roomPrefabs [Random.Range (0, roomPrefabs.Count)], position, Quaternion.identity) as GameObject;
				// Name it
				temp.name = "Tile " + currentCol + "," +currentRow;
				// Set parent
				temp.transform.parent = transform.parent;

				// Open appropriate doors
				Room tempRoom = temp.GetComponent<Room> ();
				if (currentCol != 0) {
					tempRoom.doorWest.SetActive (false);
				}
				if (currentCol != cols - 1) {
					tempRoom.doorEast.SetActive (false);
				}
				if (currentRow != 0) {
					tempRoom.doorSouth.SetActive (false);
				}
				if (currentRow != rows - 1) {
					tempRoom.doorNorth.SetActive (false);
				}

				// store it in our grid
				grid[currentCol, currentRow] = tempRoom;
			}
		}

	}

	void playerSpawner(){
		// select spawnpoint
		GameObject[] pSpawnLocations = GameObject.FindGameObjectsWithTag("PlayerSpawn");

		// if 1 player selected
		if (false) {
			// spawn player
			canvasses[0].SetActive(true);
			playerPrefab[0].SetActive(true);
			//GameObject player = Instantiate (playerPrefab [0], pSpawnLocations [Random.Range (0, pSpawnLocations.Length)].transform.position, Quaternion.identity);
			GameManager.gm.player = playerPrefab[0];
		} else {
			// two player selected
			// spawn 2 player
			canvasses[0].SetActive(true);
			canvasses[1].SetActive(true);
			playerPrefab[0].SetActive(true);
			playerPrefab[1].SetActive(true);
			//GameObject player1 = Instantiate (playerPrefab [0], pSpawnLocations [Random.Range (0, pSpawnLocations.Length)].transform.position, Quaternion.identity);
			//player1.name = "Player One";
			//GameObject player2 = Instantiate (playerPrefab [1], pSpawnLocations [Random.Range (0, pSpawnLocations.Length)].transform.position, Quaternion.identity);
			//player2.name = "Player Two";
			// TODO: add both players to game manager and then fix every single script that uses GameManager.gm.player and do something with it
			GameManager.gm.player = playerPrefab[0];
		}
	}
}