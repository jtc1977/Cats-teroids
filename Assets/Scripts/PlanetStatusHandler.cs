﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlanetStatusHandler : ObjectStatusHandler {
	//[SerializeField]int _maxHealth;
	[SerializeField]Image _healthUI;

	void Start(){

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag (TagHelper.ASTEROIRD)) {
			AsteroidStatusHandler ash = other.GetComponent<AsteroidStatusHandler> ();
			if (ash) {
				ash.Die ();
			}
			SubtractHealth (ash.GetDamageToEarth());
		}
	}
	public override void SubtractHealth (int amount)
	{
		base.SubtractHealth (amount);
		_healthUI.fillAmount = (float)(_maxHealth-GetHealth ()) / (float)_maxHealth;
	}

	public override void Die ()
	{
		//base.Die ();
		UIController.UIC.DisplayGameOver ();
		UIController.UIC.DisplayRestart ();
		GameController.GC.SetGameOver (true);
		GameController.GC.Pause ();
	}
}
