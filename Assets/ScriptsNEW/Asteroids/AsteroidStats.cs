using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AsteroidMovement))]
public class AsteroidStatus : MonoBehaviour
{
    #region Variables
    private AsteroidSpawnController asc;

    [SerializeField] private bool _isLarge;
    /// <summary>
    /// Is the asteroid large and can it break apart?
    /// </summary>
    public bool isLarge { get { return _isLarge; } }

    [SerializeField] private int _damage;
    /// <summary>
    /// The amount of damage the asteroid deals when colliding with the cannon or Earth.
    /// </summary>
    public int damage { get { return _damage; } }
    #endregion

    #region Functions
    /// <summary>
    /// A message that says what the asteroid collided with.
    /// </summary>
    /// <param name="fromCat">Did the asteroid collide with a cat?</param>
    private void ReceiveHit(bool fromCat)
    {
        // Spawns an explosion with a random rotation, if there are any prefabs available.
        if (asc.explosionPrefabs.Length > 0)
            Instantiate(asc.explosionPrefabs[Random.Range(0, asc.explosionPrefabs.Length - 1)], transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));

        // If hit by a cat, add score. Also split if it is large.
        if (fromCat)
        {
            //NewGC.gc
            if (isLarge)
            {
                //Split function
            }
        }
    }
    #endregion

    #region MonoBehaviors
    private void Start()
    {
        asc = AsteroidSpawnController.singletonInstance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.SendMessage("ReceiveDamage", SendMessageOptions.DontRequireReceiver);
    }
    #endregion
}
