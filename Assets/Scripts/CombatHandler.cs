using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ObjectStatusHandler))]
public class CombatHandler : MonoBehaviour
{
	[SerializeField] int _damage = 1;
	ObjectStatusHandler _osh;

	void Start(){
		_osh = GetComponent<ObjectStatusHandler> ();
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag (TagHelper.ASTEROIRD)) {
			ObjectStatusHandler osh = other.GetComponent<ObjectStatusHandler> ();
			if (osh) {
				osh.SubtractHealth (_damage);
			}
			_osh.SubtractHealth (1);
		}
	}
}