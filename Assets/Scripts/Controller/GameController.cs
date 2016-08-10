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

	int _life;
	int _score;
	bool _gameOver;

	//Controllers handle initialization in Awake since controllers needed to be initialized before other scripts
	void Awake ()
	{
		if (GC == null)
			GC = this;

		_life = 4;
		_score = 0;
		_gameOver = false;
	}


	void Update ()
	{
		if (GameController.GC._life < 1) {
			UIController.UIC.DisplayGameOver ();
			UIController.UIC.DisplayRestart ();
			_gameOver = true;
		}

		if (_gameOver) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);

				if (GameController.GC.PlayerObject != null)
					Destroy (GameController.GC.PlayerObject.gameObject);
			}
		}
	}

	//get/set Score
	public int GetScore ()
	{
		return _score;
	}

	public void AddScore (int amount)
	{
		SetScore (_score + amount);
	}

	public void SubtractScore (int amount)
	{
		SetScore (_score - amount);
	}

	public void SetScore (int newScore)
	{
		_score = newScore;
		//update UI Text
		UIController.UIC.SetScore (_score);
	}
	//get/set Life
	public int GetLife ()
	{
		return _life;
	}

	public void AddLife (int amount)
	{
		SetLife (_life + amount);
	}

	public void SubtractLife (int amount)
	{
		SetLife (_life - amount);
	}

	public void SetLife (int newLife)
	{
		_life = newLife;
		//Update UI Text
		UIController.UIC.SetLife (newLife);
	}
}
