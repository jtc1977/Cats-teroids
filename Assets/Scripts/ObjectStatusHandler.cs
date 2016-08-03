using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for any object with status(health & other basic info,or "Status" of this object)
/// </summary>
public class ObjectStatusHandler : MonoBehaviour {
	[SerializeField] int _health;

	public int GetHealth(){
		return _health;
	}
	public void SetHealth(int newHealth){
		_health = newHealth;
	}
}
