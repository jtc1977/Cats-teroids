using UnityEngine;
using System.Collections;

/// <summary>
/// Wave manager.
/// Create code for random waves of asteroid fields followed by 3-5 second pauses. 
/// Eventually the pause will be filled with an opportunity for the player to shoot at bonuses that heal the earth or unlock new cats, etc. 
/// The waves should have random intensity from light (fewer, slower moving asteroids) to heavy (dense clouds of asteroids that move quicker)
/// </summary>
public class WaveManager : MonoBehaviour {
	public static WaveManager WM;

	void Start(){
		if (WM == null) {
			WM = this;
		} else {
			Debug.LogWarning ("WARNING : More than one manager");
		}
	}
}
