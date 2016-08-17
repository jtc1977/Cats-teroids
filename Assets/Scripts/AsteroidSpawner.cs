using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidSpawner : MonoBehaviour
{
	public static List<Transform> ASTEROIDS = new List<Transform> ();
	public static List<Vector2> SPAWNGRID = new List<Vector2> ();
	
	public GameObject prefab;
	//public float spawnX;
	//public float spawnYMin;
	//public float spawnYMax;
	public float spawnTime;
	
	private float timeToSpawn;
	
	// Use this for initialization
	void Start ()
	{
		timeToSpawn = 0f;
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		timeToSpawn -= Time.deltaTime;
		
		if (timeToSpawn <= 0) {
//			Camera.main.
			//
			print("Width : " + Screen.width + ", height : " + Screen.height);
			Vector3 temp = Camera.main.ScreenToWorldPoint(new Vector3 (Screen.width, Screen.height,0));
			temp.z = 0f;
			print (temp.ToString ());
			GameObject go = (GameObject)Instantiate (prefab, new Vector3 (0, 0, 0), Quaternion.identity);
			GameObject go2 = (GameObject)Instantiate (prefab, temp, Quaternion.identity);
			Debug.Break ();


//			GameObject go = (GameObject)Instantiate (prefab, new Vector3(Random.Range (-3,3), Random.Range (3,7),0f), Quaternion.identity);
//			ASTEROIDS.Add (go.transform);
			timeToSpawn = spawnTime;
		}
	}

	/// <summary>
	/// randomly search free spaces from grids
	/// </summary>
	/// <returns>The empty area position</returns>
//	Vector2 getEmptyArea ()
//	{
//		Vector2 emptyPos = null;
//		int maxSearchTries = 100;
//
////		Camera.main.ScreenToWorldPoint(
//
//		return emptyPos;
//	}

	/// <summary>
	/// Sets the spawn grid.
	/// Spawn grid is Screen space cut into sections in pixel(gridsize)
	/// </summary>
	/// <param name="gridSize">Grid size in pixel</param>
	void setSpawnGrid (int gridSize)
	{
		int width = Screen.width;
		int height = Screen.height;

		//all width, two third of height from top to bottom is the spawn area


		SPAWNGRID.Clear ();

		for (int i = 0; i < (width / gridSize); i++) {
			for (int j = 0; j < (height / gridSize); j++) {				
				SPAWNGRID.Add (new Vector2 (i * (width / gridSize), j * (height / gridSize)));
			}
		}
	}
}
