using UnityEngine;
using System.Collections;

public class AsteroidStatusHandler : ObjectStatusHandler
{
	[SerializeField] GameObject _explosionPrefab;
	[SerializeField] int scoreValue;
	[SerializeField] bool _isSplitable = false;


	public override void Die ()
	{
//		base.die ();
		Instantiate (_explosionPrefab, transform.position, transform.rotation);
		//split if there are splitastroids
		//if splitable, make it split
		if (_isSplitable) {
			LevelController.LC.Split (transform.position, 0.15f);
		}
		Destroy (gameObject);
		GameController.GC.AddScore (scoreValue);
	}
//	void split(){
//	}
}
