using UnityEngine;
using System.Collections;

public class DieOnPosition : MonoBehaviour {

	public bool greaterThan = true;
	public float yPosition;

	void Start(){

	}

	void Update () 
	{
		if ((greaterThan && transform.position.y < yPosition)) {
			GameController.GC.SubtractScore (20);
			Destroy (gameObject);
		}
	}
}
