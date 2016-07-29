﻿using UnityEngine;
using System.Collections;

public class CatBullet : MonoBehaviour {
	public float speed = 3;
	public float rotSpeed = 90f;
	
	float shipBoundaryRadius = .5f;
	
	//public float force;
	
	void Start() {
		// Initial Velocity
		GetComponent<Rigidbody2D>().velocity = Vector2.up * speed;
		gameObject.transform.Rotate (0,0,90);
		//GetComponent<Rigidbody2D>().AddForce (new Vector2 (1f, .5f) * Time.deltaTime * force);
	}
	
	void Update(){
		Vector3 pos = transform.position;
		
		
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
			pos.x = widthOrtho - shipBoundaryRadius ;
			Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
			currVel.x = -currVel.x;
			//currVel.y = 1.5f;
			//currVel = currVel.normalized * speed;
			GetComponent<Rigidbody2D> ().velocity = currVel;
		}
		if(pos.x-shipBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + shipBoundaryRadius;
			Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
			currVel.x = Random.Range (-2f, 2f);
			currVel.y = 1.5f;
			currVel = currVel.normalized * speed;
			GetComponent<Rigidbody2D> ().velocity = currVel;

		}
		// Finally, update our position!!
		transform.position = pos;
		
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
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == TagHelper.ASTEROIRD) {
			Debug.Log("cat hit astorid");
			Vector3 currVel = GetComponent<Rigidbody2D> ().velocity;
			currVel.x = Random.Range (-1f, 1f);
			currVel.y = 2f;
			currVel = currVel.normalized * speed;
			GetComponent<Rigidbody2D> ().velocity = currVel;
		}
		
	}
	
	
	
	
}	
