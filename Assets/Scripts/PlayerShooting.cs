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
//	CAT_TYPE _currentCatType;
	public float fireDelay = 0.25f;
	float fireCooldownTimer = 0;


	// Update is called once per frame
	void Update ()
	{
		fireCooldownTimer -= Time.deltaTime;

		if (!GameController.GC.GetIsPaused ()) {
			if (InputController.IC.GetFire (transform.position)) {
				Fire ();
//				Debug.Break ();
			}	
			//dev only
			if (Input.GetKeyDown (KeyCode.Tab)) {
				DevText.DT.SetText ("Catbullet type swtiched");
				_currentCatBulletNum++;
				if(_currentCatBulletNum >= _catBulletPrefabs.Count)
					_currentCatBulletNum = 0;
				UIController.UIC.UIBDH.SwitchCat (_currentCatBulletNum);
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
			LastFiredBullet.transform.rotation = _catBulletPrefabs [_currentCatBulletNum].transform.rotation;
			LAST_SHOT_FRAME = Time.frameCount;
		}
	}

	public void SetFireCooldownTimer (float newTimer)
	{
		fireCooldownTimer = newTimer;
	}
}
//public enum CAT_TYPE{
//	DEFAULT = 0,
//	BLUE,
//	EVIL_CAT_EVIL,
//};