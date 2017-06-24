using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelControl : MonoBehaviour
{
    #region Variables
    /// <summary>
    /// Singleton instance of LevelController.
    /// </summary>
    public static LevelControl lc;

    public uint currentLevel = 0;

    private uint _nextMassWave;
    /// <summary>
    /// At what level does the next mass wave occur?
    /// </summary>
    public uint nextMassWave
    {
        get { return _nextMassWave; }
        private set { _nextMassWave = value; }
    }

    private uint _nextBossLevel;
    /// <summary>
    /// When does the next boss level occur?
    /// </summary>
    public uint nextBossLevel
    {
        get { return _nextBossLevel; }
        private set { _nextBossLevel = value; }
    }

    /// <summary>
    /// The number of asteroids left to spawn.
    /// </summary>
    public uint remainingAsteroids;

    //[HideInInspector] private List<Asteroid Class> existingAsteroids;

    /// <summary>
    /// A small amount of time that separates levels.
    /// </summary>
    public float restPeriod;

    private float _restTimer = 0;
    public float restTimer
    {
        get { return _restTimer; }
        private set { _restTimer = value; }
    }
    #endregion

    #region Functions
    // public void DestroyAsteroid(asteroid class)

    public void DestroyAsteroid(uint a)
    {

    }
    #endregion

    #region MonoBehaviors
    private void Awake()
    {
        if (lc == null)
            lc = this;
        else
            Destroy(this);
    }

    private void Start()
    {

        nextMassWave = 5;
        nextBossLevel = 10;
    }
    #endregion
}
