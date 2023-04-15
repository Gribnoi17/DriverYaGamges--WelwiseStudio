using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shield : MonoBehaviour
{
	private Image _shieldImage;
	private Tween _tween;

	private void Start()
	{
		_shieldImage = GetComponent<Image>();
		_shieldImage.enabled = false;
		EventManager.PlayerTookShield += TurnOnVisualTimerShield;
	}

	private void TurnOnVisualTimerShield()
	{
		if (_tween != null)
			_tween.Kill();
		_shieldImage.enabled = true;
		_shieldImage.fillAmount = 1;
		_tween = _shieldImage.DOFillAmount(0f, 3f);
	}

    private void OnDestroy()
	{
		EventManager.PlayerTookShield -= TurnOnVisualTimerShield;
	}


}
