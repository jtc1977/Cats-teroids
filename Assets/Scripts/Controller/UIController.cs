﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// Gateway to global UIs(for example, score text)
/// Singleton Design pattern.
/// </summary>
public class UIController : MonoBehaviour {
	//singleton instance
	public static UIController UIC;

	[SerializeField] Text TextLife;
	[SerializeField] Text TextScore;
	[SerializeField] Text TextGameOver;
	[SerializeField] Text TextRestart;
	[SerializeField] Text TextStatus;

	public UIBulletDisplayHandler UIBDH;

	public GameObject InGame;
	public GameObject MainMenu;

	void Awake () {
		if (UIC == null)
			UIC = this;
		else
			print ("WARNING : More than one controller");

		SetLife (GameController.GC.GetLife());
		SetScore (GameController.GC.GetScore ());
		TextGameOver.text = "";
		TextRestart.text = "";
		TextStatus.text = "";
	}
	public void SetLife(int newLife){
		TextLife.text = "LIVES : " + newLife;
	}
	public void SetScore(int newScore){
		TextScore.text = "SCORE : " + newScore;
	}
	public void DisplayGameOver(){
		TextGameOver.text = "SUCKS TO BE YOU";
	}
	public void DisplayRestart(){
		TextRestart.text = "Press 'R' to Restart";
	}
	public void OnLevelWasLoaded(int level)
	{
		//Duke 8/2/2016
		//Note : I think I ran into singleton instance remaining as if this gameobject is called with DontDestroyOnLoad,
		//But does not replicate here. So if problem shows, handle here by destroying old instance, if not, ignore or delete this.
	}
	public void SetStatusText(string newText){
		TextStatus.text = newText;
	}

	//for test
	public void PrintToEditor(string content){
		print (content);
	}
}