using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{
	public static InputController IC;

	float touchBeganTimer = 0;
	float touch2BeganTimer = 0;
	float tapTimer = 0.3f;
	//how quick of a touch is considered as a tap? in seconds
	Vector2 touchBeginPos = Vector2.zero;
	Vector2 touch2BeginPos = Vector2.zero;
	float tapCancelPos = 0.2f;
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
		case RuntimePlatform.OSXEditor:
		case RuntimePlatform.OSXDashboardPlayer:
		case RuntimePlatform.OSXPlayer:
		case RuntimePlatform.OSXWebPlayer:
			if (Input.GetButton ("Fire1"))
				return true;
			break;
		case RuntimePlatform.Android:
		case RuntimePlatform.IPhonePlayer:
		case RuntimePlatform.BlackBerryPlayer:
			Touch touchInput = Input.GetTouch (0);
			Touch touchInput2 = Input.GetTouch (1);

			if (touchInput2.phase == TouchPhase.Began) {
				touch2BeganTimer = Time.time + tapTimer;
				touch2BeginPos = pos;
				return false;
			} else if (touchInput2.phase == TouchPhase.Ended && Vector2.Distance (touch2BeginPos, pos) < tapCancelPos && touch2BeganTimer > Time.time) {
				return true;
			} else if (touchInput2.phase == TouchPhase.Moved || touchInput2.phase == TouchPhase.Stationary) {
				return false;
			}

			if (touchInput.phase == TouchPhase.Began) {
				touchBeganTimer = Time.time + tapTimer;
				touchBeginPos = pos;
			} else if (touchInput.phase == TouchPhase.Ended && Vector2.Distance (touchBeginPos, pos) < tapCancelPos && touchBeganTimer > Time.time) {
				return true;
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
		float InputX = transform.position.x;
		switch (Application.platform) {
		case RuntimePlatform.WindowsPlayer:
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
				Touch touchInput = Input.GetTouch (0);
				InputX = xPos + touchInput.deltaPosition.x * speed;
			}
			break;
		default:			
			Debug.LogWarning ("Platform Not Defined");
			break;
		}
		return InputX;
	}
}
