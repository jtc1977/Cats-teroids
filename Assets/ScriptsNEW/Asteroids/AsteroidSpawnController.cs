using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the spawning of asteroids.
/// </summary>
public class AsteroidSpawnController : MonoBehaviour
{
    #region Variables
    public static AsteroidSpawnController singletonInstance;

    [Header("Spawning")]
    [SerializeField] private Transform[] _asteroidSpawnPositions;
    /// <summary>
    /// The Vector2 positions of each of the spawners.
    /// </summary>
    public Vector2[] asteroidSpawnPositions
    {
        get
        {
            Vector2[] v = new Vector2[_asteroidSpawnPositions.Length];
            for (int i = 0; i < _asteroidSpawnPositions.Length; i++)
                v[i] = _asteroidSpawnPositions[i].position;
            return v;
        }
    }

    /// <summary>
    /// The minimum amount of time that can pass between asteroid spawns.
    /// </summary>
    public float spawnTimeMin;

    /// <summary>
    /// The maximum amount of time that can pass between asteroid spawns.
    /// </summary>
    public float spawnTimeMax;

    /// <summary>
    /// The time until the next asteroid spawns. Should be a value between spawnTimeMin and spawnTimeMax.
    /// </summary>
    [HideInInspector] public float nextSpawnTime;

    private float _spawnTimer = 0;
    /// <summary>
    /// Tracks how much time has passed since the previous asteroid spawned.
    /// </summary>
    public float spawnTimer
    {
        get { return _spawnTimer; }
        private set { _spawnTimer = value; }
    }

    [Header("Prefabs")]
    [SerializeField] private GameObject[] _largeAsteroids;
    public GameObject[] largeAsteroids { get { return _largeAsteroids; } }

    [SerializeField] private GameObject[] _smallAsteroids;
    public GameObject[] smallAsteroids { get { return _smallAsteroids; } }

    [SerializeField] private GameObject[] _explosions;
    public GameObject[] explosionPrefabs { get { return _explosions; } }
    #endregion

    #region Functions

    #endregion

    #region MonoBehaviors
    private void Awake()
    {
        if (singletonInstance == null)
            singletonInstance = this;
        else
            Destroy(this);
    }

    private void Start() { nextSpawnTime = spawnTimeMin; }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= nextSpawnTime)
        {
            // Spawn Timer
            spawnTimer = 0;
            nextSpawnTime = Random.Range(spawnTimeMin, spawnTimeMax);
            
            // Spawn Function


            // Asteroid Stats
        }
    }
    #endregion
}