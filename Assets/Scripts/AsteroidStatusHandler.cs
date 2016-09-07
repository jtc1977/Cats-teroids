using UnityEngine;
using System.Collections;

public class AsteroidStatusHandler : ObjectStatusHandler
{
	[SerializeField] GameObject _explosionPrefab;
	[SerializeField] int scoreValue;

	public override void Die ()
	{
//		base.die ();
		Instantiate (_explosionPrefab, transform.position, transform.rotation);
		Destroy (gameObject);
		GameController.GC.AddScore (scoreValue);
	}
}
