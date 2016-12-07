using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HelmetCatStatusHandler : ObjectStatusHandler {
	
	[SerializeField] public Transform helmetSpawnPoint;
	[SerializeField] Animator _myAnim;
	[SerializeField] public GameObject helmet;

	void Start (){
		_myAnim = GetComponent<Animator> ();
	}

	public override void  SubtractHealth(int amount){
		if (_health == _maxHealth) {
			_myAnim.SetTrigger ("FirstHit");
			Instantiate (helmet, helmetSpawnPoint.position, helmetSpawnPoint.rotation);
		}
		base.SetHealth (_health - amount);
	}


}
