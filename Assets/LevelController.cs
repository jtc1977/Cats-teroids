using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class LevelController : MonoBehaviour
{
	public static LevelController LC;
	[SerializeField] AsteroidSpawner _as;
	[SerializeField] List<GameObject> _splitAsteroids = new List<GameObject>();
	[SerializeField] int _massWaveIntervalMin = 1;
	[SerializeField] int _massWaveIntervalMax = 4;
	[SerializeField] int _difficulty = 1;
	int _asteroidCnt = 5;
	//	float _asteroidSpdMultiplier = 1f;
	[SerializeField] int _currentLevel = 1;
	bool levelLoading = false;
	string statusText = "";
	bool bossLevel = false;
	// Use this for initialization
	void Start ()
	{
		if (LC == null)
			LC = this;
		else
			Debug.LogWarning ("WARNING : More than one controller");

		StartLevel (_currentLevel);
	}

	public void StartLevel (int levelToStart)
	{
		_currentLevel = levelToStart;
		_as.Pool = (int)((1f + _currentLevel * 0.2f) * _asteroidCnt);
//		_asteroidSpdMultiplier += 0.2f * _difficulty;

		//boss levels
		if (bossLevel) {
			_as.spawnTimeMin = 0.1f;
			_as.spawnTimeMax = 1f;
			_as.SpeedMultiplier += 0.2f * _difficulty;
		}
	}

	void Update ()
	{
	}

	public int GetCurrentLevel ()
	{
		return _currentLevel;
	}

	public void LevelComplete (float timerCount = 4f)
	{
		if (!levelLoading) {
			_currentLevel++;
			if (_currentLevel % Random.Range (3, 5) == 0) {
//			if (_currentLevel % 2 == 0)
				bossLevel = true;
			} else {
				bossLevel = false;
				timerCount = 0f;
			}

			levelLoading = true;
			StartCoroutine (IETimerCount (timerCount));
		}
	}

	IEnumerator IETimerCount (float timerCount)
	{
		float timerOrigin = Time.time;
		while (Time.time - timerOrigin < timerCount) {
//			statusText = "Level " + (_currentLevel - 1) + " Completed!\nNext round starts in " + (int)(timerCount - (Time.time - timerOrigin));

			if (bossLevel)
				statusText = "\nWARNING : MASS WAVE!";
					
			UIController.UIC.SetStatusText (statusText);
			yield return null;
		}
		UIController.UIC.SetStatusText ("");
		StartLevel (_currentLevel);
		levelLoading = false;
	}

	public void Split (Vector3 position, float seconds)
	{
		StartCoroutine (IESplit (position, seconds));
	}

	IEnumerator IESplit(Vector3 position, float seconds){
		yield return new WaitForSeconds (seconds);

		//if splitable, make it split
		int i = Random.Range(0, _splitAsteroids.Count);
		GameObject go = Instantiate (_splitAsteroids.ElementAt(i));
		go.GetComponent<AsteroidMovementHandler> ().Initialize (DIRECTION.LEFT);
		go.transform.position = position;
		AsteroidSpawner.ASTEROIDS.Add (go.transform);
		i = Random.Range(0, _splitAsteroids.Count);
		go = Instantiate (_splitAsteroids.ElementAt(i));
		go.GetComponent<AsteroidMovementHandler> ().Initialize (DIRECTION.RIGHT);
		go.transform.position = position;
		AsteroidSpawner.ASTEROIDS.Add (go.transform);
	}
}
