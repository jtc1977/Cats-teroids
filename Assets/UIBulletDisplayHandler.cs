using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIBulletDisplayHandler : MonoBehaviour {
	public List<GameObject> CatUIList;
	int currentCat = 0;

	void Start(){
		foreach (var go in CatUIList) {
			go.SetActive (false);
		}
		CatUIList [currentCat].SetActive (true);
	}

	public void SwitchCat(int _catIndex){
		foreach (var go in CatUIList) {
			go.SetActive (false);
		}
//		currentCat++;
//		if(currentCat >= CatUIList.Count)
//			currentCat = 0;
		CatUIList [_catIndex].SetActive (true);
	}
}
