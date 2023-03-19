using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class PanelAnimation : MonoBehaviour
{
    [Header("Garage Animation")]
    [SerializeField] private Vector3 _garageStartPosition;
    [SerializeField] private Vector3 _garageEndPosition;
    [SerializeField] private Vector3 _streetEndPosition;
    [SerializeField] private float _animationDuration = 1.0f;
    [SerializeField] private int _currentCarIndex = 0;
    [SerializeField] private List<Transform> carsInGarage;

    [Header("Panel Anim")]
    [SerializeField] private RectTransform _topPanelTransform;
    [SerializeField] private RectTransform _bottomPanelTransform;
    [SerializeField] private float _panelMoveDuration = 1f;
    [SerializeField] private float _panelAlphaDuration = 0.5f;

    [Header("Object Scaling")]
    [SerializeField] private List<Transform> _objectsToScale;
    [SerializeField] private float _timeBeforeUpScale = 0.4f;
    [SerializeField] private float _upScaleDuration = 1.0f;
    [SerializeField] private float _downScaleDuration = 0.4f;

    [Header("Left Panel")]
    [SerializeField] private RectTransform _leftPanelTransform;
    [SerializeField] private GameObject _carsPanel;
    [SerializeField] private GameObject _locationsPanel;
    [SerializeField] private List<Transform> _contentOfLocations;
    [SerializeField] private List<Transform> _contentOfCars;
    [SerializeField] private float _leftPanelMoveDuration = 1f;
    [SerializeField] private float _leftPanelAlphaDuration = 0.5f;


    [Header("Canvas Transition")]




    private CanvasGroup _leftPanelCanvasGroup;

    private CanvasGroup _topPanelCanvasGroup;
    private CanvasGroup _bottomPanelCanvasGroup;


    private void Start()
    {
        LeftPanelAnim();
    }

    private void LeftPanelAnim()
    {
        PanelsStartAnim();

        _leftPanelCanvasGroup = _leftPanelTransform.gameObject.GetComponent<CanvasGroup>();

        // Устанавливаем начальную позицию панели за пределами экрана
        Vector2 startPos = new Vector2(-Screen.width, 0f);
        _leftPanelTransform.anchoredPosition = startPos;

        // Устанавливаем начальное значение прозрачности панели
        _leftPanelCanvasGroup.alpha = 0;

        // Анимация появления панели
        _leftPanelTransform.DOAnchorPosX(0f, _leftPanelMoveDuration);
        _leftPanelCanvasGroup.DOFade(1f, _leftPanelAlphaDuration);
    }

    public void CallLocationsPanel()
    {
        StartCoroutine(ShowContentOfLocations());
    }

    public void CallCarsPanel()
    {
        StartCoroutine(ShowContentOfCars());
    }


    private IEnumerator ShowContentOfLocations()
    {
        DownScaleObjects(_contentOfCars);
        yield return new WaitForSeconds(0.4f);
        _locationsPanel.SetActive(true);
        UPScaleObjects(_contentOfLocations);
    }

    private IEnumerator ShowContentOfCars()
    {
        DownScaleObjects(_contentOfLocations);
        yield return new WaitForSeconds(0.4f);
        _carsPanel.SetActive(true);
        UPScaleObjects(_contentOfCars);
    }

    private void PanelsStartAnim()
    {

        _topPanelCanvasGroup = _topPanelTransform.gameObject.GetComponent<CanvasGroup>();
        _bottomPanelCanvasGroup = _bottomPanelTransform.gameObject.GetComponent<CanvasGroup>();

        _topPanelCanvasGroup.alpha = 0;
        _bottomPanelCanvasGroup.alpha = 0;

        // Анимация появления верхней панели
        _topPanelTransform.anchoredPosition -= new Vector2(_topPanelTransform.rect.width, 0);
        _topPanelTransform.DOAnchorPosX(0, _panelMoveDuration);
        _topPanelCanvasGroup.DOFade(1f, _panelAlphaDuration);

        // Анимация появления нижней панели
        _bottomPanelTransform.anchoredPosition += new Vector2(_bottomPanelTransform.rect.width, 0);
        _bottomPanelTransform.DOAnchorPosX(0, _panelMoveDuration);
        _bottomPanelCanvasGroup.DOFade(1f, _panelAlphaDuration);

        foreach (Transform obj in _objectsToScale)
        {
            obj.gameObject.SetActive(false);
        }
        Invoke("UPScaleObjects", _timeBeforeUpScale);
    }

    private void UPScaleObjects()
    {
        foreach (Transform obj in _objectsToScale)
        {
            obj.gameObject.SetActive(true);

            Vector3 startScale = new Vector3(0f, 0f, 0f);
            Vector3 endScale = obj.localScale;

            obj.localScale = startScale;
            obj.DOScale(endScale, _upScaleDuration);
        }
    }


    private void UPScaleObjects(List<Transform> objects)
    {
        foreach (Transform obj in objects)
        {
            obj.gameObject.SetActive(true);

            Vector3 startScale = new Vector3(0f, 0f, 0f);
            Vector3 endScale = new Vector3(1f, 1f, 1f);

            obj.localScale = startScale;
            obj.DOScale(endScale, _upScaleDuration);
        }
    }


    private void DownScaleObjects(List<Transform> objects)
    {
        foreach (Transform obj in objects)
        {
            obj.gameObject.SetActive(true);

            Vector3 startScale = obj.localScale;
            Vector3 endScale = new Vector3(0f, 0f, 0f);

            obj.DOScale(endScale, _downScaleDuration);
        }
    }



    public IEnumerator MoveCarToGarage(Transform tr)
    {
        yield return new WaitForSeconds(0.4f);
        tr.position = _garageStartPosition;
        tr.DOMove(_garageEndPosition, _animationDuration).SetAutoKill(false).OnComplete(() =>
        {
            Debug.Log("Машина прибыла в гараж");
        });
    }

    public void MoveCarToStreet(Transform tr)
    {
        tr.position = _garageEndPosition;
        tr.DOMove(_streetEndPosition, _animationDuration).SetAutoKill(false).OnComplete(() =>
        {
            Debug.Log("Машина прибыла на улицу");
        });
    }



    public void CheckCar(int indexInCarsList)
    {
        if (_currentCarIndex == indexInCarsList)
            return;
        MoveCarToStreet(carsInGarage[_currentCarIndex]);
        StartCoroutine(MoveCarToGarage(carsInGarage[indexInCarsList]));
        _currentCarIndex = indexInCarsList;
    }

}
