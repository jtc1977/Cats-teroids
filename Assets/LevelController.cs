using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
	public static LevelController LC;

	[SerializeField] AsteroidSpawner _as;
	[SerializeField] int _massWaveIntervalMin = 1;
	[SerializeField] int _massWaveIntervalMax = 4;
	[SerializeField] int _difficulty = 1;
	int _asteroidCnt = 15;
	float _asteroidSpdMultiplier = 1f;
	[SerializeField] int _currentLevel = 1;

	// Use this for initialization
	void Start ()
	{
		if (LC == null)
			LC = this;
		else
			Debug.LogWarning ("WARNING : More than one controller");
	}

	public void StartLevel (int levelToStart)
	{
		_currentLevel = levelToStart;
		_as.Pool = (int)((1f + _currentLevel * 0.2f) * _asteroidCnt);
		_asteroidSpdMultiplier += 0.2f * _difficulty;

	}

	public int GetCurrentLevel ()
	{
		return _currentLevel;
	}
}
