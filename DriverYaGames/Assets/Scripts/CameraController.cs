using Cinemachine;
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

    [Header("Camera Y moving effect")]
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private float _cameraMovingDuration = 10f;
    [SerializeField] private float _shoulderOffsetValue = 15f;

    [Header("Shaking effect")]
    [SerializeField] private float _shakeDuration = 0.5f;
    [SerializeField] private float _shakeIntensity = 1f;
    [SerializeField] private float _shakeFrequency = 25f;

    [Header("Nitro Effect")]
    [SerializeField] private float _targetFOV = 55f;
    [SerializeField] private float _nitroEffectDuration = 2f;
    private float _originalShoulderOffset;
    private Transform _followPoint;



    // private Booster _booster;
    private Sequence _sequenceRotate;
    private Sequence _sequenceMoveBackwards;

    private void Start()
    {
        EventManager.PlayerDied += ShakeCameraPlay;
        Invoke(nameof(FindFollowPoint), 0.3f);
        EventManager.PlayerTookNitro += StartMoveCameraBackwards;
        StartCoroutine(WaitForStartAnim());
        //StartCoroutine(MoveShoulderOffset(_shoulderOffsetValue, _cameraMovingDuration)); ------- метод для изменения позиции камеры
    }

    private IEnumerator WaitForStartAnim()
    {
        _virtualCamera.enabled = false;
        yield return new WaitForSeconds(4f);
        _virtualCamera.enabled = true;
    }

    private void FindFollowPoint()
    {
        _followPoint = GameObject.FindGameObjectWithTag("CameraFollowPoint").GetComponent<Transform>();
        _virtualCamera.Follow = _followPoint;
    }

    private IEnumerator MoveShoulderOffset(float offsetValue, float duration)
    {
        Cinemachine3rdPersonFollow follow = _virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        float originalOffset = follow.ShoulderOffset.x;

        // Плавное отклонение вправо
        float elapsedTime = 0f;
        while (elapsedTime < duration / 2f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / (duration / 2f));
            follow.ShoulderOffset.x = Mathf.Lerp(originalOffset, originalOffset + offsetValue, t);
            yield return null;
        }

        // Плавное возвращение на исходное значение
        elapsedTime = 0f;
        while (elapsedTime < duration / 2f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / (duration / 2f));
            follow.ShoulderOffset.x = Mathf.Lerp(originalOffset + offsetValue, originalOffset, t);
            yield return null;
        }

        // Плавное отклонение влево
        elapsedTime = 0f;
        while (elapsedTime < duration / 2f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / (duration / 2f));
            follow.ShoulderOffset.x = Mathf.Lerp(originalOffset, originalOffset - offsetValue, t);
            yield return null;
        }

        // Плавное возвращение на исходное значение
        elapsedTime = 0f;
        while (elapsedTime < duration / 2f)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / (duration / 2f));
            follow.ShoulderOffset.x = Mathf.Lerp(originalOffset - offsetValue, originalOffset, t);
            yield return null;
        }

        // Возвращаем значение ShoulderOffset обратно на исходное значение
        follow.ShoulderOffset.x = originalOffset;
    }

    private void StartMoveCameraBackwards()
    {
        StartCoroutine(StartCameraZoom());
    }

    private IEnumerator StartCameraZoom()
    {
        float startFov = _virtualCamera.m_Lens.FieldOfView;
        float elapsedTime = 0f;

        while (elapsedTime < _nitroEffectDuration)
        {
            elapsedTime += Time.deltaTime;
            float newFov = Mathf.Lerp(startFov, _targetFOV, elapsedTime / _nitroEffectDuration);
            _virtualCamera.m_Lens.FieldOfView = newFov;
            yield return null;
        }

        elapsedTime = 0f;

        while (elapsedTime < _nitroEffectDuration)
        {
            elapsedTime += Time.deltaTime;
            float newFOV = Mathf.Lerp(_targetFOV, startFov, elapsedTime / _nitroEffectDuration);
            _virtualCamera.m_Lens.FieldOfView = newFOV;
            yield return null;
        }

        // возвращаем значение FieldOfView обратно
        _virtualCamera.m_Lens.FieldOfView = startFov;
    }



    private void OnDestroy()
    {
        EventManager.PlayerDied -= ShakeCameraPlay;
        EventManager.PlayerTookNitro -= StartMoveCameraBackwards;
        _sequenceRotate.Kill();
    }

    private void ShakeCameraPlay()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeCamera());
        _brokenScreen.enabled = true;
        StartCoroutine(DeactivateBrokenScreen());
    }


    private IEnumerator DeactivateBrokenScreen()
    {
        yield return new WaitForSeconds(2f);
        _brokenScreen.enabled = false;
    }



    private IEnumerator ShakeCamera()
    {
        // Получаем компонент следования 3-го лица Cinemachine3rdPersonFollow
        var thirdPersonFollow = _virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();

        // Сохраняем начальное значение ShoulderOffset.x
        var originalOffset = thirdPersonFollow.ShoulderOffset.x;

        print("Трясись, алее");

        // Задаем длительность встряски
        var elapsed = 0f;
        while (elapsed < _shakeDuration)
        {
            // Вычисляем смещение на каждом шаге встряски
            var shakeOffset = new Vector3(
                Mathf.PerlinNoise(Time.time * _shakeFrequency, 0) - 0.5f,
                Mathf.PerlinNoise(0, Time.time * _shakeFrequency) - 0.5f,
                0f
            ) * _shakeIntensity;

            // Обновляем значение ShoulderOffset.x, чтобы сместить камеру
            thirdPersonFollow.ShoulderOffset.x = originalOffset + shakeOffset.x;

            // Приостанавливаем выполнение на один кадр
            yield return null;

            // Обновляем счетчик времени
            elapsed += Time.deltaTime;
        }

        // Возвращаем значение ShoulderOffset.x обратно в исходное состояние
        thirdPersonFollow.ShoulderOffset.x = originalOffset;
    }


}
