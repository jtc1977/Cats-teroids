using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AsteroidSpawner : MonoBehaviour
{
	public static List<Transform> ASTEROIDS = new List<Transform> ();
	public static List<Vector3> SPAWNGRID = new List<Vector3> ();
	
	//	public GameObject prefab;
	public List<GameObject> prefabs;
	public float spawnTimeMin;
	public float spawnTimeMax;
	float spawnTimeMinOrigin;
	float spawnTimeMaxOrigin;
	private float timeToSpawn;
	float _nearCheckDistance = 2f;

	//number of asteroids left to spawn
	public int Pool = 0;
	public float SpeedMultiplier = 1f;
//	float _asteroidCountTimer = 0f;
//	bool _isAsteroidEmpty = false;
	
	// Use this for initialization
	void Start ()
	{
		timeToSpawn = 0f;

		spawnTimeMaxOrigin = spawnTimeMax;
		spawnTimeMinOrigin = spawnTimeMin;

		setSpawnGrid (20);

//		_asteroidCountTimer = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (GameController.GC.GetIsPaused ())
			return;
		
//		if (Pool > 0) {
		if (Pool > 0) {
			timeToSpawn -= Time.deltaTime;
			if (timeToSpawn <= 0) {
				ASTEROIDS.RemoveAll (x => x == null);
				int rndPrefabs = Random.Range (0, prefabs.Count);
				if (haveEmptyArea ()) {
					GameObject go = (GameObject)Instantiate (prefabs [rndPrefabs], getEmptyArea (), Quaternion.identity);
					ASTEROIDS.Add (go.transform);
					Pool--;
					AsteroidMovementHandler amh = go.GetComponent<AsteroidMovementHandler> ();
					amh.speed = SpeedMultiplier;
					amh.Initialize ();
				} else {
					print ("No empty space, asteroids count : " + ASTEROIDS.Count);
				}
				timeToSpawn = Random.Range (spawnTimeMin, spawnTimeMax);
			}
		} else if (ASTEROIDS.Count <= 0) {
//			if (!_isAsteroidEmpty) {
//				_isAsteroidEmpty = true;
//
//			}

			LevelController.LC.LevelComplete ();

		} else {
			ASTEROIDS.RemoveAll (x => x == null);
		}
//		print (ASTEROIDS.Count);
	}

	/// <summary>
	/// Does the grid have an empty slot?
	/// </summary>
	/// <returns><c>true</c>, if empty area was found, <c>false</c> otherwise.</returns>
	/// <param name="nearCheckDistance">How much space does the grid need to be from asteroids to be considered empty?</param>
	bool haveEmptyArea ()
	{
		bool emptyAreaFound = false;

		foreach (var area in SPAWNGRID) {
			if (isAreaEmpty (area)) {
				emptyAreaFound = true;
				break;
			}
		}
		return emptyAreaFound;
	}

	/// <summary>
	/// Check if the area is empty
	/// </summary>
	/// <returns><c>true</c>, if area is empty, <c>false</c> otherwise.</returns>
	/// <param name="area">Area.</param>
	/// <param name="nearCheckDistance">How much space does the grid need to be from asteroids to be considered empty?</param>
	bool isAreaEmpty (Vector3 area)
	{
		bool isEmpty = true;
		foreach (var aster in ASTEROIDS) {
			if (Vector3.Distance (area, aster.position) < _nearCheckDistance) {
				isEmpty = false;
				break;
			}
		}
		return isEmpty;
	}

	/// <summary>
	/// randomly search free spaces from grids
	/// </summary>
	/// <returns>The empty area position</returns>
	Vector3 getEmptyArea ()
	{
		Vector3 emptyPos = Vector3.zero;
		int maxSearchTries = 100;

		for (int i = 0; i < maxSearchTries; i++) {
			int k = Random.Range (0, SPAWNGRID.Count);
			if (isAreaEmpty (SPAWNGRID.ElementAt (k))) {
				return SPAWNGRID.ElementAt (k);
			}
		}

		//if fails finding out randomly, find by searching from top to bottom
		foreach (var area in SPAWNGRID) {
			if (isAreaEmpty (area))
				return area;
		}

		print ("WARNING : EMPTY AREA NOT FOUND");
		return emptyPos;
	}

	/// <summary>
	/// Sets the spawn grid.
	/// Spawn grid is Screen space cut into sections into certain numbers(gridCount)
	/// </summary>
	/// <param name="gridCount">Grid size in count</param>
	void setSpawnGrid (int gridCount)
	{
		//starts from bottom left corner(0,0)
		int widthMax = Screen.width;
		int heightMax = Screen.height + 100;
		int widthMin = 0;

		//all width, two third of height from top to bottom is the spawn area
		int heightMin = (int)((float)Screen.height / 1.1f);

		int width = widthMax - widthMin;
		int height = heightMax - heightMin;

		int widthGridSize = width / gridCount;
		int heightGridSize = height / gridCount;

		int widthGridCount = width / widthGridSize;
		int heightGridCount = height / heightGridSize;


		SPAWNGRID.Clear ();

		for (int i = 0; i < widthGridCount; i++) {
			for (int j = 0; j < heightGridCount; j++) {		
				Vector3 gridPoint = Camera.main.ScreenToWorldPoint (new Vector3 (widthMin + i * widthGridSize, heightMin + j * heightGridSize, 0));
				gridPoint.z = 0;
				SPAWNGRID.Add (gridPoint);
			}
		}
	}

	public void ResetSpawnTimes ()
	{
		spawnTimeMax = spawnTimeMaxOrigin;
		spawnTimeMin = spawnTimeMinOrigin;
	}
}
