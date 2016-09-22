using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShooting : MonoBehaviour
{
	public static int LAST_SHOT_FRAME = 0;
	public static GameObject LastFiredBullet;

	public Vector3 offset = new Vector3 (0, .75f, 0);
	[SerializeField] List<GameObject> _catBulletPrefabs = new List<GameObject>();
	/// <summary>
	/// Current catbullet type id(index)
	/// </summary>
	int _currentCatBulletNum = 0;
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
			//dev only
			if (Input.GetKeyDown (KeyCode.Tab)) {
				DevText.DT.SetText ("Catbullet type swtiched");
				if (_currentCatBulletNum == 0)
					_currentCatBulletNum = 1;
				else
					_currentCatBulletNum = 0;
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
			LastFiredBullet = (GameObject)Instantiate (_catBulletPrefabs[_currentCatBulletNum], transform.position + offset, transform.rotation);
			LAST_SHOT_FRAME = Time.frameCount;
		}
	}

	public void SetFireCooldownTimer (float newTimer)
	{
		fireCooldownTimer = newTimer;
	}
}
