using Redcode.Tweens;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _durationShake = 0.5f;
    [SerializeField] private Image _brokenScreen;

    private float _period = 6f;


    public float Period
    {
        get => _period;
        set => _period = value;
    }

    private void Start()
    {
        EventManager.PlayerDied += ShakeCameraPlay;
        EventManager.PlayerDied += DeactivateBrokenScreen;
    }


    private void OnDestroy()
    {
        EventManager.PlayerDied -= ShakeCameraPlay;
        EventManager.PlayerDied -= DeactivateBrokenScreen;
    }

    private void ShakeCameraPlay()
    {
        StartCoroutine(Shaking());
        _brokenScreen.enabled = true;
        Invoke(nameof(DeactivateBrokenScreen), 2f);
    }


    private void DeactivateBrokenScreen()
    {
        _brokenScreen.enabled = false;
    }



    private IEnumerator Shaking()
    {
        Vector3 startPosotion = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < _durationShake)
        {
            elapsedTime += Time.deltaTime;
            float strength = _curve.Evaluate(elapsedTime / _durationShake);
            transform.position = startPosotion + Random.insideUnitSphere * strength;
            yield return null;

        }

        transform.position = startPosotion;

    }


}
