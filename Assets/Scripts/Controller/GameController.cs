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
	[SerializeField] bool _isPaused = false;

	//Controllers handle initialization in Awake since controllers needed to be initialized before other scripts
	void Awake ()
	{
		if (GC == null)
			GC = this;

		_life = 4;
		_score = 0;
		_gameOver = false;
//		_isPaused = false;
	}


	void Update ()
	{
		if (GameController.GC._life < 1) {
			UIController.UIC.DisplayGameOver ();
			UIController.UIC.DisplayRestart ();
			_gameOver = true;
			Pause ();
		}

		if (_gameOver) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
				Unpause ();

				if (GameController.GC.PlayerObject != null)
					Destroy (GameController.GC.PlayerObject.gameObject);
			}
		}

		//dev
		if (Input.GetKeyDown (KeyCode.Backspace)) {
			Pause ();
//			foreach (Transform t in AsteroidSpawner.ASTEROIDS.ToArray())
//				Destroy (t.gameObject);
			for (int i = 0; i < AsteroidSpawner.ASTEROIDS.Count; i++)
				Destroy (AsteroidSpawner.ASTEROIDS [i].gameObject);
			Destroy (PlayerObject.gameObject);
			UIController.UIC.InGame.SetActive (false);
			UIController.UIC.MainMenu.SetActive (true);
		}
	}

	public void SetGameOver(bool isOver){
		_gameOver = isOver;
	}

	public void PauseToggle(){
		//to disable from shooting when button is pushed
		if (PlayerObject != null) {
			PlayerObject.GetComponent<PlayerShooting> ().SetFireCooldownTimer (0.2f);
		}
//		print ("PauseToggle");
//		if(PlayerShooting.LastFiredBullet != null)
//			print ("Current frame : " + Time.frameCount + ", last shot frame : " + PlayerShooting.LAST_SHOT_FRAME);
		
//		if (Time.timeScale == 0f)
		if(_isPaused)
			Unpause ();
		else
			Pause ();
	}

	public void Pause(){
		Time.timeScale = 0f;
		_isPaused = true;
	}

	public void Unpause(){
		Time.timeScale = 1f;
		_isPaused = false;
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
	public bool GetIsPaused(){
		return _isPaused;
	}
	public void MoveCannon(DIRECTION dir){
		PlayerObject.GetComponent<CannonMove> ().Move (dir);
	}

	public void Quit(){
		Application.Quit ();
	}
}
