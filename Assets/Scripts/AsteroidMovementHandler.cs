using UnityEngine;
using System.Collections;

public class AsteroidMovementHandler : MonoBehaviour {

//	public float maxSpeed = 3f;
	public float rotSpeed = 90f;
	public float speed;

	float shipBoundaryRadius = 0.5f;

	void Start () {
		//GetComponent<Rigidbody2D>();
		//initialize();
		transform.rotation = Quaternion.Euler( 0, 0, Random.Range(0f, 360f));
	}
	public void Initialize(){
		GetComponent<Rigidbody2D>().velocity = Vector3.down * Random.Range(0.5f, speed);
	}

	public void Initialize(DIRECTION dir){
		Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
		if (dir == DIRECTION.RIGHT) {
			currVel.x = Random.Range (speed * 0.4f, speed * 0.6f);
			currVel.y = -speed * 0.3f;
			currVel = currVel.normalized * speed;
		} else {
			currVel.x = Random.Range (-speed * 0.6f, -speed * 0.4f);
			currVel.y = -speed * 0.3f;
			currVel = currVel.normalized * speed;
		}
		//apply velocity
		GetComponent<Rigidbody2D> ().velocity = currVel;
	}
	
	void Update () {

		// ROTATE the ship.

		// Grab our rotation quaternion
		Quaternion rot = transform.rotation;

		// Grab the Z euler angle
		float z = rot.eulerAngles.z;

		// Change the Z angle based on input
		z -= rotSpeed * Time.deltaTime;

		// Recreate the quaternion
		rot = Quaternion.Euler( 0, 0, z );

		// Feed the quaternion into our rotation
		transform.rotation = rot;

		// MOVE the ship.
		Vector3 pos = transform.position;
		 
//		Vector3 velocity = new Vector3(0,  maxSpeed * Time.deltaTime, 0);

		//pos += rot * velocity;

		// RESTRICT the player to the camera's boundaries!

		// First to vertical, because it's simpler
		/*if(pos.y+shipBoundaryRadius > Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
		}
		if(pos.y-shipBoundaryRadius < -Camera.main.orthographicSize) {
			pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
		}*/

		// Now calculate the orthographic width based on the screen ratio
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		// Now do horizontal bounds
		if(pos.x+shipBoundaryRadius > widthOrtho) {
			pos.x = widthOrtho - shipBoundaryRadius;
			Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
			currVel.x = -currVel.x;
			GetComponent<Rigidbody2D> ().velocity = currVel;
		}
		if(pos.x-shipBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + shipBoundaryRadius;
			Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
			currVel.x = -currVel.x;
			GetComponent<Rigidbody2D> ().velocity = currVel;
		}

		// Finally, update our position!!
		transform.position = pos;


	}
}
