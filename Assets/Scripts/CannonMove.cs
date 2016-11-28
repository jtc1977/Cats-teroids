using UnityEngine;
using System.Collections;

public class CannonMove : MonoBehaviour
{
	public float paddleSpeed = 1f;
	private Vector3 playerPos = new Vector3 (0, -3.25f, 0);
	float shipBoundaryRadius = 1f;

	void FixedUpdate ()
	{
		float xPos = InputController.IC.GetHorizontalInput (transform.position.x, paddleSpeed);

		playerPos = new Vector3 (Mathf.Clamp (xPos, -8f, 8f), -3.25f, 0f);
		transform.position = playerPos;

		Vector3 pos = transform.position;


		if (pos.y + shipBoundaryRadius > Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
		}
		if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize) {
			pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
		}
		
		// Now calculate the orthographic width based on the screen ratio
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;
		
		// Now do horizontal bounds
		if (pos.x + shipBoundaryRadius > widthOrtho) {
			pos.x = widthOrtho - shipBoundaryRadius;
		}
		if (pos.x - shipBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + shipBoundaryRadius;
		}
		
		// Finally, update our position!!
		transform.position = pos;
	}

	public void Move(DIRECTION dir){
		float xPos;
		if (dir == DIRECTION.LEFT) {
			xPos = transform.position.x + (-1f * paddleSpeed);
		} else {
			xPos = transform.position.x + (1f * paddleSpeed);
		}

		playerPos = new Vector3 (Mathf.Clamp (xPos, -8f, 8f), -3.74f, 0f);
		transform.position = playerPos;

		Vector3 pos = transform.position;


		if (pos.y + shipBoundaryRadius > Camera.main.orthographicSize) {
			pos.y = Camera.main.orthographicSize - shipBoundaryRadius;
		}
		if (pos.y - shipBoundaryRadius < -Camera.main.orthographicSize) {
			pos.y = -Camera.main.orthographicSize + shipBoundaryRadius;
		}

		// Now calculate the orthographic width based on the screen ratio
		float screenRatio = (float)Screen.width / (float)Screen.height;
		float widthOrtho = Camera.main.orthographicSize * screenRatio;

		// Now do horizontal bounds
		if (pos.x + shipBoundaryRadius > widthOrtho) {
			pos.x = widthOrtho - shipBoundaryRadius;
		}
		if (pos.x - shipBoundaryRadius < -widthOrtho) {
			pos.x = -widthOrtho + shipBoundaryRadius;
		}

		// Finally, update our position!!
		transform.position = pos;
	}
}
public enum DIRECTION{
	LEFT,
	RIGHT,
}
