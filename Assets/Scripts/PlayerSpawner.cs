using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
	
	public GameObject playerPrefab;
	GameObject playerInstance;
	
	public int lives = 4;
	float respawnTimer;
	public GUIText livesText;
	public GUIText scoreText;
	public int score;
	public GUIText restartText;
	public GUIText gameOverText;
	
	
	public bool gameOver;
	public bool restart;
	
	// Use this for initialization
	void Start()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore();
		//Duke : I want to delete this line of code
//		livesText = GameObject.Find ("livesText").GetComponent<GUIText>();
		SpawnPlayer();
		
	}
	
	void SpawnPlayer()
	{
		lives--;
		livesText.text = "LIVES: " + lives;
		respawnTimer = 2;
		playerInstance = (GameObject)Instantiate(playerPrefab, transform.position, Quaternion.identity);
	}
	
	
	// Update is called once per frame
	void Update()
	{
		if (playerInstance == null && lives < 1) {
			gameOverText.text = "SUCKS TO BE YOU";
			gameOver = true;
		}
		
		if (gameOver){
			restartText.text = "Press 'R' to Restart";
			restart = true;
		}
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
		
		if (playerInstance == null && lives > 0) {
			respawnTimer -= Time.deltaTime;
			
			if (respawnTimer <= 0) {
				SpawnPlayer ();
			}
			
		}
		
	}
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore();
	}
	
	void UpdateScore()
	{
		scoreText.text = "SCORE: " + score;

	}
	public void GameOver(){
		
		gameOverText.text = "SUCKS TO BE YOU";
		gameOver = true;
		
		
	}
}