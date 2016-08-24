using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Text))]
public class DevText : MonoBehaviour
{
	public static DevText DT;
	Text _txt;

	// Use this for initialization
	void Start ()
	{
		if (DT == null)
			DT = this;
		else
			print ("More than one devtext");
		
		_txt = GetComponent<Text> ();
	}

	public void SetText(string text){
		_txt.text = text;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
