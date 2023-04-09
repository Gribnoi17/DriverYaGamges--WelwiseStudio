using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
	private Image _shieldImage;

	private void Start()
	{
		_shieldImage = GetComponent<Image>();
		_shieldImage.enabled = false;
		EventManager.PlayerTookShield += TurnOnVisualTimerShield;
	}

	private void TurnOnVisualTimerShield()
	{
		StartCoroutine(StartVisualTimerShield());
	}

	private IEnumerator StartVisualTimerShield()
	{
		_shieldImage.enabled = true;
		_shieldImage.fillAmount = 1;
		while (_shieldImage.fillAmount > 0.001)
		{
			_shieldImage.fillAmount -= 0.007f; //магическое число +- 3 секунды
			yield return new WaitForFixedUpdate();
		}
		_shieldImage.fillAmount = 0;
		StopCoroutine(StartVisualTimerShield());

	}

	private void OnDestroy()
	{
		EventManager.PlayerTookShield -= TurnOnVisualTimerShield;
	}


}
