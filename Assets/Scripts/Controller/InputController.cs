using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	public static InputController IC;

	float touchBeganTimer = 0;
	float touch2BeganTimer = 0;
	float tapTimer = 0.5f;
	//how quick of a touch is considered as a tap? in seconds
	Vector2 touchBeginPos = Vector2.zero;
	Vector2 touch2BeginPos = Vector2.zero;
	float tapCancelPos = 0.5f;
	//if touch is moved(swiped), don't fire
	bool _gyroControl = false;
//Is player movement using touch or gyroscope?
	float _gyroSensitivity = 0.25f;

	void Start ()
	{
		if (IC == null)
			IC = this;
		else
			print ("WARNING : More than one controller");
	}

	/// <summary>
	/// Sets the gyro control.
	/// </summary>
	/// <param name="isGyroControl">If set to <c>true</c> is gyro control.</param>
	public void SetGyroControl (bool isGyroControl)
	{
		_gyroControl = isGyroControl;
	}

	/// <summary>
	/// Gets cannon fire input from multiple platform
	/// </summary>
	/// <returns><c>true</c>, if input is triggered to fire, <c>false</c> otherwise.</returns>
	/// <param name="pos">Position.</param>
	public bool GetFire (Vector3 pos)
	{
		switch (Application.platform) {
		case RuntimePlatform.WindowsPlayer:
		case RuntimePlatform.WindowsEditor:
		case RuntimePlatform.WindowsWebPlayer:
		case RuntimePlatform.LinuxPlayer:
		case RuntimePlatform.WebGLPlayer:
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXDashboardPlayer:
		case RuntimePlatform.OSXPlayer:
		case RuntimePlatform.OSXWebPlayer:
			if (Input.GetButtonDown ("Fire1"))
				return true;
			break;
		case RuntimePlatform.Android:
		case RuntimePlatform.IPhonePlayer:
		case RuntimePlatform.BlackBerryPlayer:

			if (Input.touchCount < 1)
				return false;

			Touch touchInput = Input.GetTouch (0);
			Debug.Log ("here");
			if (touchInput.phase == TouchPhase.Began) {
				touchBeganTimer = Time.time + tapTimer;
				//				touchBeginPos = pos;
				touchBeginPos = pos;
				Debug.Log ("3");
			} else if (touchInput.phase == TouchPhase.Ended && Vector2.Distance (touchBeginPos, pos) < tapCancelPos && touchBeganTimer > Time.time) {
				Debug.Log ("fire");
				return true;
			}

			if (Input.touchCount < 2)
				return false;
			
			Touch touchInput2 = Input.GetTouch (1);

			if (touchInput2.phase == TouchPhase.Began) {
				touch2BeganTimer = Time.time + tapTimer;
				touch2BeginPos = pos;
//				touch2BeginPos = touchInput2.position;
				Debug.Log ("1");
				return false;
			} else if (touchInput2.phase == TouchPhase.Ended && Vector2.Distance (touch2BeginPos, pos) < tapCancelPos && touch2BeganTimer > Time.time) {
				return true;
			} else if (touchInput2.phase == TouchPhase.Moved || touchInput2.phase == TouchPhase.Stationary) {
				return false;
				Debug.Log ("2");
			}

			if (Input.touchCount > 2) {
				if (Input.GetTouch (2).phase == TouchPhase.Ended) {
					GameController.GC.SwitchBullet ();
				}
			}

			break;
		default:			
			Debug.LogWarning ("Platform Not Defined");
			break;
		}
		return false;
	}

	/// <summary>
	/// Gets the input from multiple platforms
	/// </summary>
	/// <returns>Horizontal Input(X)</returns>
	/// <param name="xPos">X position.</param>
	/// <param name="speed">Speed.</param>
	public float GetHorizontalInput (float xPos, float speed)
	{
//		float InputX = transform.position.x;
		float InputX = xPos;
		switch (Application.platform) {
		case RuntimePlatform.WindowsPlayer:
		case RuntimePlatform.WebGLPlayer:
		case RuntimePlatform.WindowsEditor:
		case RuntimePlatform.WindowsWebPlayer:
		case RuntimePlatform.LinuxPlayer:
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXDashboardPlayer:
		case RuntimePlatform.OSXPlayer:
		case RuntimePlatform.OSXWebPlayer:
			InputX = xPos + (Input.GetAxis	("Horizontal") * speed);
			break;
		case RuntimePlatform.Android:
		case RuntimePlatform.IPhonePlayer:
		case RuntimePlatform.BlackBerryPlayer:			
			if (_gyroControl) {
				if (Mathf.Abs (Input.acceleration.x) > _gyroSensitivity) {
					InputX = xPos + Mathf.Clamp (Input.acceleration.x, -0.1f, 0.1f);
					DevText.DT.SetText ("Acclerometer input : " + Input.acceleration.x);
				}
			} else {
				if (Input.touchCount > 0) {				
					Touch touchInput = Input.GetTouch (0);
					InputX = xPos + Mathf.Clamp (touchInput.deltaPosition.x, -1.5f, 1.5f) * speed;
				}
			}
			break;
		default:			
			Debug.LogWarning ("Platform Not Defined");
			break;
		}
		return InputX;
	}
}
public enum CANNON_CONTROL_TYPE{
	TOUCH_DRAG,
	TOUCH_AREA,
	ACCLEROMETER,
}
