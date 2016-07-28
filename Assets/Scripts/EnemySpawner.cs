using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	
	public GameObject enemy;
	//public float spawnX;
	//public float spawnYMin;
	//public float spawnYMax;
	public float spawnTime;
	
	private float timeToSpawn;
	
	// Use this for initialization
	void Start () {
		timeToSpawn = 0f;
		
	}
	
	// Update is called once per frame
	void Update () {
		timeToSpawn -= Time.deltaTime;
		
		if (timeToSpawn <= 0) {
			Instantiate (enemy, new Vector3(Random.Range (-3,3), Random.Range (3,7),0f), Quaternion.identity);
			timeToSpawn = spawnTime;
		}
		
	}
}
