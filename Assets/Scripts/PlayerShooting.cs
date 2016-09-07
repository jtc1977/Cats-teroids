using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
	public static int LAST_SHOT_FRAME = 0;
	public static GameObject LastFiredBullet;

	public Vector3 offset = new Vector3 (0, .75f, 0);
	public GameObject PlayerShotPrefab;
	public float fireDelay = 0.25f;
	float fireCooldownTimer = 0;

	// Update is called once per frame
	void Update ()
	{
		fireCooldownTimer -= Time.deltaTime;

		if (!GameController.GC.GetIsPaused ()) {
			if (InputController.IC.GetFire (transform.position)) {
				Fire ();
			}	
		}
	}

	/// <summary>
	/// Fire the cat(bullet)
	/// </summary>
	public void Fire ()
	{
		if (fireCooldownTimer <= 0) {
			fireCooldownTimer = fireDelay;
			Vector3 offset = transform.rotation * new Vector3 (0, .75f, 0);
			LastFiredBullet = (GameObject)Instantiate (PlayerShotPrefab, transform.position + offset, transform.rotation);
			LAST_SHOT_FRAME = Time.frameCount;
		}
	}

	public void SetFireCooldownTimer (float newTimer)
	{
		fireCooldownTimer = newTimer;
	}
}
