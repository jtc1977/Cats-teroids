﻿using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour
{
	
	public GameObject playerPrefab;
	//how long does it take to respawn player?
	[SerializeField] float _respawnTimer = 2f;
	float respawnTimerCounter;
	
	// Use this for initialization
	void Start ()
	{
		SpawnPlayer ();		
	}

	void SpawnPlayer ()
	{
		GameController.GC.Lives--;
//		livesText.text = "LIVES: " + lives;
		respawnTimerCounter = _respawnTimer;
		GameController.GC.PlayerObject = ((GameObject)Instantiate (playerPrefab, transform.position, Quaternion.identity)).transform;
	}

	void Update ()
	{
		//if dead, spawn after respawn timer
		if (GameController.GC.PlayerObject == null && GameController.GC.Lives > 0) {
			respawnTimerCounter -= Time.deltaTime;
			if (respawnTimerCounter <= 0) {
				SpawnPlayer ();
			}			
		}		
	}
	//	public void AddScore (int newScoreValue)
	//	{
	//		score += newScoreValue;
	//		UpdateScore();
	//	}
	//	void UpdateScore()
	//	{
	//		scoreText.text = "SCORE: " + score;
	//	}
	//	public void GameOver(){
	//
	//		gameOverText.text = "SUCKS TO BE YOU";
	//		gameOver = true;
	//	}
}