﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for any object with status(health & other basic info,or "Status" of this object)
/// </summary>
public class ObjectStatusHandler : MonoBehaviour {
	[SerializeField] protected int _health;
	[SerializeField] protected int _maxHealth;

	public int GetHealth(){

		return _health;
	}

	public int GetMaxHealth(){

		return _maxHealth;
	}
	public virtual void SetHealth(int newHealth){
		_health = newHealth;
		if (_health <= 0)
			Die ();
	}
	public virtual void SubtractHealth(int amount){
		SetHealth (_health - amount);
	}
	/// <summary>
	/// kill this object
	/// </summary>
	public virtual void Die(){
		Destroy (gameObject);
	}
}
