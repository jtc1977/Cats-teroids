using UnityEngine;
using System.Collections;

public class AsteroidMovement : MonoBehaviour {

	public float maxSpeed = 3f;
	public float rotSpeed = 90f;
	public float speed;

	float shipBoundaryRadius = 1f;

	void Start () {
		//GetComponent<Rigidbody2D>();
		GetComponent<Rigidbody2D>().velocity = Vector3.down * speed;
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
		 
		Vector3 velocity = new Vector3(0,  maxSpeed * Time.deltaTime, 0);

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
		}
		if(pos.x-shipBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + shipBoundaryRadius;
		}

		// Finally, update our position!!
		transform.position = pos;


	}
}
