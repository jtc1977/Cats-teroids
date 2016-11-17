using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CannonStatusHandler : ObjectStatusHandler {
	[SerializeField] Image _healthImage;
	[SerializeField] Canvas _UICanvas;

	public override void SetHealth (int newHealth)
	{
		base.SetHealth (newHealth);

		_healthImage.fillAmount = (float)_health / (float)_maxHelath;
	}

	void Start(){
		_UICanvas.worldCamera = Camera.main;
	}
}
