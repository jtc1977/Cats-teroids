using UnityEngine;
using System.Collections;

/// <summary>
/// Gateway to global UIs(for example, score text)
/// Singleton Design pattern.
/// </summary>
public class UIController : MonoBehaviour {
	//singleton instance
	public static UIController UIC;

	[SerializeField] GUIText TextLife;
	[SerializeField] GUIText TextScore;
	public GUIText TextGameOver;
	public GUIText TextRestart;

	void Start () {
		if (UIC == null)
			UIC = this;
		else
			print ("WARNING : More than one controller");

		SetLife (GameController.GC.GetLife());
		SetScore (GameController.GC.GetScore ());
		TextGameOver.text = "";
		TextRestart.text = "";
	}
	public void SetLife(int newLife){
		TextLife.text = "LIVES : " + newLife;
	}
	public void SetScore(int newScore){
		TextScore.text = "SCORE : " + newScore;
	}
	public void OnLevelWasLoaded(int level)
	{
		//Duke 8/2/2016
		//Note : I think I ran into singleton instance remaining as if this gameobject is called with DontDestroyOnLoad,
		//But does not replicate here. So if problem shows, handle here by destroying old instance, if not, ignore or delete this.
	}
}
