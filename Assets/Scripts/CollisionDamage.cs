using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour {

   public int health = 1;
    public int scoreValue;
    private PlayerSpawner playerSpawner;
	public GameObject explosion;

    void Start()
    {
        GameObject playerSpawnerObject = GameObject.FindWithTag("PlayerSpawnSpot");
        if (playerSpawnerObject != null)
        {
            playerSpawner = playerSpawnerObject.GetComponent<PlayerSpawner>();
        }
    }
    void OnTriggerEnter2D(){
        Debug.Log("Trigger!");

        health--;
        playerSpawner.AddScore(scoreValue);
    }

    void Update(){
		if (health <= 0) {
			Die ();
			Instantiate (explosion, transform.position, transform.rotation);
		}

		} 

    void Die(){
        Destroy(gameObject);
    }

}