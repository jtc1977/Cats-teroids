using UnityEngine;
using System.Collections;

public class CollisionDamage : MonoBehaviour
{
	public int health = 1;
	public int scoreValue;
	public GameObject explosion;

	void OnTriggerEnter2D (Collider2D other)
	{
//		Debug.Log ("Trigger!");
		health--;
		GameController.GC.AddScore (scoreValue);
	}

	void Update ()
	{
		if (health <= 0) {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}

	}
}