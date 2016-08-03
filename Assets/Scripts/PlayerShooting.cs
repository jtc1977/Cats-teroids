using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public Vector3 offset = new Vector3(0, .75f, 0);
    public GameObject PlayerShotPrefab;
    public float fireDelay = 0.25f;
    float cooldownTimer = 0;

	// Update is called once per frame
	void Update () {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetButton("Fire1") && cooldownTimer <= 0)
        {
//            Debug.Log("Pew!");
            cooldownTimer = fireDelay;

            Vector3 offset = transform.rotation * new Vector3(0, .75f, 0);

            Instantiate(PlayerShotPrefab, transform.position + offset, transform.rotation);
        }
	
	}
}
