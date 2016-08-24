using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{

	public Vector3 offset = new Vector3 (0, .75f, 0);
	public GameObject PlayerShotPrefab;
	public float fireDelay = 0.25f;
	float fireCooldownTimer = 0;

	// Update is called once per frame
	void Update ()
	{
		fireCooldownTimer -= Time.deltaTime;

		if (InputController.IC.GetFire (transform.position, fireCooldownTimer)) {
			Fire ();
		}	
	}

	/// <summary>
	/// Fire the cat(bullet)
	/// </summary>
	public void Fire ()
	{
		fireCooldownTimer = fireDelay;
		Vector3 offset = transform.rotation * new Vector3 (0, .75f, 0);
		Instantiate (PlayerShotPrefab, transform.position + offset, transform.rotation);
	}
}
