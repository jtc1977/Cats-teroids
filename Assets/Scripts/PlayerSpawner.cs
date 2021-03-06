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
		if (!GameController.GC.GetIsPaused ()) {
			SpawnPlayer ();

//			print (GameController.GC.GetIsPaused ().ToString ());
		}
	}

	public void SpawnPlayer ()
	{
		GameController.GC.SubtractLife (1);
//		livesText.text = "LIVES: " + lives;
		respawnTimerCounter = _respawnTimer;
		GameController.GC.PlayerObject = ((GameObject)Instantiate (playerPrefab, transform.position, Quaternion.identity)).transform;
		GameController.GC.PlayerObject.position = new Vector3 (GameController.GC.PlayerObject.position.x, -3.25f, 0f);
	}

	void Update ()
	{
		if (GameController.GC.GetIsPaused ())
			return;
		
		//if dead, spawn after respawn timer
		if (GameController.GC.PlayerObject == null && GameController.GC.GetLife()> 0) {
			respawnTimerCounter -= Time.deltaTime;
			if (respawnTimerCounter <= 0) {
				SpawnPlayer ();
			}			
		}		
	}
}