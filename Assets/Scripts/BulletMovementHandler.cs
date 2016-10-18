using UnityEngine;
using System.Collections;

public class BulletMovementHandler : MonoBehaviour
{
	//Y speed(Upward)
	public float speed = 3;
	public float rotSpeed = 90f;
	float shipBoundaryRadius = .5f;
	float minXVel;//minimumX velocity
	float maxXVel;
	float angularVel;

	void Start ()
	{
		// Initial Velocity
		GetComponent<Rigidbody2D> ().velocity = Vector2.up * speed;
		gameObject.transform.Rotate (0, 0, 90);

		minXVel = 1f;
		maxXVel = speed * 0.8f;
		angularVel = 0f;
	}

	void Update ()
	{
		Vector3 pos = transform.position;
		
		// Now calculate the orthographic width based on the screen ratio
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;
		
		// Now do horizontal bounds
		if (pos.x + shipBoundaryRadius > widthOrtho) {
			pos.x = widthOrtho - shipBoundaryRadius;
			Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
			currVel.x = -currVel.x;
			//currVel.y = 1.5f;
			//currVel = currVel.normalized * speed;
			GetComponent<Rigidbody2D> ().velocity = currVel;
		}
		if (pos.x - shipBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + shipBoundaryRadius;
			Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
			currVel.x = Random.Range (-2f, 2f);
			currVel.y = 1.5f;
			currVel = currVel.normalized * speed;
			GetComponent<Rigidbody2D> ().velocity = currVel;
		}

//		 Finally, update our position!!
		transform.position = pos;		

//		// Grab our rotation quaternion
//		Quaternion rot = transform.rotation;
//		
//		// Grab the Z euler angle
//		float z = rot.eulerAngles.z;
//		
//		// Change the Z angle based on input
//		z -= rotSpeed * Time.deltaTime;
//		
//		// Recreate the quaternion
//		rot = Quaternion.Euler (0, 0, z);
//		
//		// Feed the quaternion into our rotation
//		transform.rotation = rot;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == TagHelper.ASTEROIRD) {
//			Debug.Log("cat hit astorid");

			//slowdown & change direction when hit
//			Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
//			currVel.x = Random.Range (-1f, 1f);
//			currVel.y = 2f;
//			currVel = currVel.normalized * speed;
//			GetComponent<Rigidbody2D> ().velocity = currVel;

			Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
			//if compared object position is left, move left, move right otherwise
//			print ("cat x : " + transform.position.x + ", ast x : " + other.transform.position.x);
			float xPosDiff = transform.position.x - other.transform.position.x;
			if (xPosDiff > 0f) {
//				print ("right");
				//if cat is on the right side

				//randomize X speed if it's too low
				if (Mathf.Abs (currVel.x) < 0.2f) {
					currVel.x = Random.Range (speed * 0.4f, speed * 0.6f);
					currVel.y = speed * 0.3f;
					currVel = currVel.normalized * speed;
				} else if (currVel.x < 0f) {
					//going left
					//othewise, flip x value
					currVel.x = -currVel.x;
					currVel = currVel.normalized * speed;
				} else {
					//going right
					currVel.x += Random.Range (0f, 0.5f);
//					Mathf.Clamp (currVel.x, 0.5f, 1.5f);
					Mathf.Clamp (currVel.x, minXVel, maxXVel);
					currVel = currVel.normalized * speed;
				}
				//hit from the right, spin to the left
				angularVel += Random.Range(rotSpeed/ 3f, rotSpeed/ 2f);
				Mathf.Clamp (angularVel, rotSpeed / 3f, rotSpeed);

			} else {
				//left
//				print("left");

				//randomize X speed if it's too low
				if (Mathf.Abs (currVel.x) < 0.2f) {
					currVel.x = Random.Range (-speed * 0.6f, -speed * 0.4f);
					currVel.y = speed * 0.3f;
					currVel = currVel.normalized * speed;
				} else if (currVel.x > 0f) {
					//going right
					//othewise, flip x value
					currVel.x = -currVel.x;
					currVel = currVel.normalized * speed;					
				} else {
					//going left
					currVel.x -= Random.Range (0f, 0.5f);
					Mathf.Clamp (currVel.x, -maxXVel, -minXVel);
					currVel = currVel.normalized * speed;	
				}
				//hit from the left, spin to the left
				angularVel -= Random.Range(rotSpeed/ 3f, rotSpeed/ 2f);
				Mathf.Clamp (angularVel, -rotSpeed, -rotSpeed / 3f);
			}
//			Debug.Break ();
			//apply velocity
			GetComponent<Rigidbody2D> ().velocity = currVel;

			//apply spin
			GetComponent<Rigidbody2D>().angularVelocity = angularVel;
		}	
	}
}
