using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _durationShake = 0.5f;
    [SerializeField] private Image _brokenScreen;

   // private Booster _booster;
    private Sequence _sequenceRotate;
    private Sequence _sequenceMoveBackwards;

    private void Start()
    {
        EventManager.PlayerDied += ShakeCameraPlay;
        EventManager.PlayerTookNitro += StartMoveCameraBackwards;

        _sequenceMoveBackwards = DOTween.Sequence();

        DOVirtual.DelayedCall(20f, CameraRotate);
    }

    private void CameraRotate()
    {
        _sequenceRotate = DOTween.Sequence();
        _sequenceRotate.Append(transform.DORotate(new Vector3(0f, 3f, 0f), 2f, RotateMode.Fast).SetEase(Ease.Linear));
        _sequenceRotate.Append(transform.DORotate(new Vector3(0f, 0f, 0f), 2f, RotateMode.Fast).SetEase(Ease.Linear));
        _sequenceRotate.Append(transform.DORotate(new Vector3(0f, -3f, 0f), 2f, RotateMode.Fast).SetEase(Ease.Linear));
        _sequenceRotate.Append(transform.DORotate(new Vector3(0f, 0f, 0f), 2f, RotateMode.Fast).SetEase(Ease.Linear));
        _sequenceRotate.SetLoops(-1);
    }

    private void StartMoveCameraBackwards()
    {
        StartCoroutine(MoveCameraBackwards());
    }

    private IEnumerator MoveCameraBackwards()
    {
        if (_sequenceMoveBackwards != null)
            _sequenceMoveBackwards.Kill();
        _sequenceMoveBackwards.Append(transform.DOMoveZ(-1.5f, 0.5f));
        yield return new WaitForSeconds(3.5f);
        _sequenceMoveBackwards.Append(transform.DOMoveZ(0f, 0.5f));
        StopCoroutine(MoveCameraBackwards());


    }


    private void OnDestroy()
    {
        EventManager.PlayerDied -= ShakeCameraPlay;
        EventManager.PlayerDied -= CameraRotate;
        EventManager.PlayerTookNitro -= StartMoveCameraBackwards;
        _sequenceRotate.Kill();
    }

    private void ShakeCameraPlay()
    {
        StartCoroutine(Shaking());
        _brokenScreen.enabled = true;
        StartCoroutine(DeactivateBrokenScreen());
    }


    private IEnumerator DeactivateBrokenScreen()
    {
        yield return new WaitForSeconds(2f);
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
