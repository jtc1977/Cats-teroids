using UnityEngine;
using System.Collections;

public class CannonMove : MonoBehaviour {


	public float paddleSpeed = 1f;
	
	private Vector3 playerPos = new Vector3 (0, -3.74f, 0);

	float shipBoundaryRadius = 1f;

	
	// Use this for initialization
	
	// Update is called once per frame
	void Update ()
	{
		float xPos = transform.position.x + (Input.GetAxis("Horizontal") * paddleSpeed);
		playerPos = new Vector3(Mathf.Clamp (xPos, -8f, 8f), -3.74f, 0f);
		transform.position = playerPos;

		Vector3 pos = transform.position;


		if(pos.y+shipBoundaryRadius > Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
		}
		if(pos.y-shipBoundaryRadius < -Camera.main.orthographicSize) {
			pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
		}
		
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
