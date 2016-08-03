using UnityEngine;
using System.Collections;

/// <summary>
/// GameController holds key reference like Player object and handles High-level precedures like changing gamestates.
/// Singleton design pattern.
/// </summary>
public class GameController : MonoBehaviour
{
	//singleton instance.
	public static GameController GC;

	//Reference to player object(I.e. cannon)
	[HideInInspector] public Transform PlayerObject;

	public int Lives;
	[HideInInspector] public int Score;
	bool _gameOver;
	bool _restart;

	void Start(){
		if (GC == null)
			GC = this;
		Score = 0;
		_gameOver = false;
		_restart = false;
	}

	void Update(){
		if (GameController.GC.Lives < 1) {
//			gameOverText.text = "SUCKS TO BE YOU";
			_gameOver = true;
		}

		if (_gameOver){
//			restartText.text = "Press 'R' to Restart";
			_restart = true;
		}
		if (_restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
}
