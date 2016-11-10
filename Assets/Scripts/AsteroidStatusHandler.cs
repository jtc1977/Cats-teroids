using UnityEngine;
using System.Collections;

public class AsteroidStatusHandler : ObjectStatusHandler
{
	[SerializeField] GameObject _explosionPrefab;
	[SerializeField] int scoreValue;
	[SerializeField] GameObject _splitAsteroids;


	public override void Die ()
	{
//		base.die ();
		Instantiate (_explosionPrefab, transform.position, transform.rotation);
		//split if there are splitastroids
		Invoke("split", 0.5f);
		split ();
		Destroy (gameObject);
		GameController.GC.AddScore (scoreValue);
	}
	void split(){
		//if splitable, make it split
		if (_splitAsteroids != null) {
			GameObject go = Instantiate (_splitAsteroids);
			go.GetComponent<AsteroidMovementHandler> ().Initialize (DIRECTION.LEFT);
			go.transform.position = transform.position;
			go = Instantiate (_splitAsteroids);
			go.GetComponent<AsteroidMovementHandler> ().Initialize (DIRECTION.RIGHT);
			go.transform.position = transform.position;
		}
	}
}
