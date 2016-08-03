using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GUIText))]
public class TextUpdateLives : MonoBehaviour {
	GUIText _text;
	//how often should this text be updated?
	[Range(0.1f, 5.0f)]
	[SerializeField] float _updateInterval;

	// Use this for initialization
	void Start () {
		_text = GetComponent<GUIText> ();
		InvokeRepeating ("UpdateText", 0.5f, _updateInterval);
	}

	/// <summary>
	/// Updates the UI text element
	/// </summary>
	void UpdateText(){
		_text.text = "LIVES: " + GameController.GC.Lives;
	}
}
