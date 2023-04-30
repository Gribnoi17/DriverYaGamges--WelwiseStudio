using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [SerializeField] private RectTransform _controlChoicePanel;

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
    [SerializeField] private GameObject _tasksPanel;
    [SerializeField] private List<Transform> _contentOfLocations;
    [SerializeField] private List<Transform> _contentOfTasks;
    [SerializeField] private List<Transform> _contentOfCars;
    [SerializeField] private float _leftPanelMoveDuration = 1f;
    [SerializeField] private float _leftPanelAlphaDuration = 0.5f;


    [Header("Canvas Transition")]
    [SerializeField] private CanvasGroup _firstCanvas;
    [SerializeField] private CanvasGroup _secondCanvas;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _backButton;


    [Header("Stats setting")]
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _milageText;

    [Header("Settings")]
    [SerializeField] private GameObject _slantChoicePanel;
    [SerializeField] private GameObject _swipeChoicePanel;

    [Header("Other")]
    [SerializeField] private GameObject _canvasWithSliderCont;

    private CanvasGroup _leftPanelCanvasGroup;
    private enum _panels {CarsPanel, LocationsPanel, TasksPanel };
    private _panels _currentOpenedPanel = _panels.CarsPanel;
    private CanvasGroup _topPanelCanvasGroup;
    private CanvasGroup _bottomPanelCanvasGroup;
    private bool isTransitioningBtwCanvases = false;
    private bool isTransitioningBtwLeftPanels = false;
    private bool isTransitioningBtwCars = false;

    private void Awake()
    {
        PlayerPrefs.SetInt("MoneyNameConst", 16000);
        Time.timeScale = 1f;
        DOTween.Init();
    }

    private void Start()
    {
        SetPlayerPrefs();
        SetCurrentCarByPrefs();
        _secondCanvas.alpha = 0f;

        // При нажатии на кнопку Play запускаем анимацию перехода
        _playButton.onClick.AddListener(StartTransition);
        _backButton.onClick.AddListener(BackCanvasTransition);
    }

    private void SetPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("MoneyNameConst"))
        {
            UpdateMoneyText();
        }
        else
        {
            PlayerPrefs.SetInt("MoneyNameConst", 0);
        }
        if (PlayerPrefs.HasKey("BestMilage"))
        {
            _milageText.text = PlayerPrefs.GetInt("BestMilage").ToString();
        }
        else
        {
            PlayerPrefs.SetInt("BestMilage", 0);
        }


        if (!PlayerPrefs.HasKey("Car"))
        {
            PlayerPrefs.SetString("Car", "PoliceCar");
        }

        if (PlayerPrefs.HasKey("ControllerType"))
        {
            LeftPanelAnim();
            if(PlayerPrefs.GetString("ControllerType") == "Swipe")
            {
                _swipeChoicePanel.SetActive(true);
            }
            else if(PlayerPrefs.GetString("ControllerType") == "Slant")
            {
                _slantChoicePanel.SetActive(true);
            }
        }
        else if (!PlayerPrefs.HasKey("ControllerType") && PlayerPrefs.GetInt("IsMobile") == 1)
        {
            _firstCanvas.gameObject.SetActive(false);
            ControlChoicePanelAppears();
        }
        else
        {
            PlayerPrefs.SetString("ControllerType", "Keyboard");
        }
    }

    public void UpdateMoneyText()
    {
        _moneyText.text = PlayerPrefs.GetInt("MoneyNameConst").ToString();
    }

    public void ChouseSlant(bool isSettings)
    {
        PlayerPrefs.SetString("ControllerType", "Slant");
        if(isSettings == false)
        {
            _controlChoicePanel.transform.parent.gameObject.SetActive(false);
            LeftPanelAnim();
        }
        _slantChoicePanel.SetActive(true);
    }


    public void ChouseSwipe(bool isSettings)
    {
        PlayerPrefs.SetString("ControllerType", "Swipe");
        if(isSettings == false)
        {
            _controlChoicePanel.transform.parent.gameObject.SetActive(false);
            LeftPanelAnim();
        }
        _swipeChoicePanel.SetActive(true);
    }

    public void CallSettingsPanel(GameObject panel)
    {
        if (isTransitioningBtwCanvases) return;
        isTransitioningBtwCanvases = true;
        panel.GetComponent<CanvasGroup>().alpha = 1f;
        // Анимация отодвигания первого канваса влево
        _firstCanvas.transform
            .DOLocalMoveX(_firstCanvas.transform.position.x - 4000f, 0.7f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                // Когда первый канвас отодвинулся, появляем второй канвас с постепенным увеличением прозрачности
                panel.transform.parent.gameObject.SetActive(true);
                panel.SetActive(true);
                SettingsChoicePanelAppears(panel);
                //ControlChoicePanelAppears();

                _firstCanvas.gameObject.SetActive(false);
                _firstCanvas.transform.localPosition = new Vector3(0f, 0f, 0f);
                isTransitioningBtwCanvases = false;
            });
    }

    private void SettingsChoicePanelAppears(GameObject panel)
    {
        Vector3 startScale = new Vector3(0f, 0f, 0f);
        Vector3 endScale = _controlChoicePanel.localScale;

        panel.transform.localScale = startScale;
        panel.transform.parent.gameObject.SetActive(true);

        panel.transform.DOScale(endScale, _upScaleDuration);
    }


    private void ControlChoicePanelAppears()
    {
        Vector3 startScale = new Vector3(0f, 0f, 0f);
        Vector3 endScale = _controlChoicePanel.localScale;

        _controlChoicePanel.localScale = startScale;
        _controlChoicePanel.transform.parent.gameObject.SetActive(true);

        _controlChoicePanel.DOScale(endScale, _upScaleDuration);
    }

    private void BackCanvasTransition()
    {
        if (isTransitioningBtwCanvases) return;
        isTransitioningBtwCanvases = true;

        _secondCanvas.DOFade(0f, 0.7f).OnComplete(() =>
        {
            _firstCanvas.alpha = 0f;
            _firstCanvas.gameObject.SetActive(true);
            _firstCanvas.DOFade(1f, 0.7f);
            isTransitioningBtwCanvases = false;
            _secondCanvas.gameObject.SetActive(false);
        }); ;
    }

    public void BackCanvasTransition(CanvasGroup canvasGroup)
    {
        if (isTransitioningBtwCanvases) return;
        isTransitioningBtwCanvases = true;
        //CanvasGroup parentSettingsPanel = canvasGroup.transform.parent.gameObject.GetComponent<CanvasGroup>();
        canvasGroup.DOFade(0f, 0.7f).OnComplete(() =>
        {
            _firstCanvas.alpha = 0f;
            _firstCanvas.gameObject.SetActive(true);
            _firstCanvas.DOFade(1f, 0.7f);
            isTransitioningBtwCanvases = false;
            canvasGroup.gameObject.SetActive(false);
            canvasGroup.transform.parent.gameObject.SetActive(false);
            canvasGroup.alpha = 1f;
            //parentSettingsPanel.alpha = 1f;
        }); ;
    }

    private void StartTransition()
    {
        if (isTransitioningBtwCanvases) return;
        isTransitioningBtwCanvases = true;

        // Анимация отодвигания первого канваса влево
        _firstCanvas.transform
            .DOLocalMoveX(_firstCanvas.transform.position.x - 4000f, 0.7f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                // Когда первый канвас отодвинулся, появляем второй канвас с постепенным увеличением прозрачности
                _secondCanvas.transform.parent.gameObject.SetActive(true);
                _secondCanvas.gameObject.SetActive(true);
                _secondCanvas.alpha = 0f;
                //_secondCanvas.transform.DOMoveX(0f, 1f);
                _secondCanvas.DOFade(1f, 1f).OnComplete(() =>
                {
                    // Когда второй канвас появился, скрываем первый канвас и сбрасываем его позицию
                    _firstCanvas.gameObject.SetActive(false);
                    _firstCanvas.transform.localPosition = new Vector3(0f, 0f, 0f);
                    isTransitioningBtwCanvases = false;
                });
            });
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
        if(_currentOpenedPanel != _panels.LocationsPanel && isTransitioningBtwLeftPanels == false)
        {
            StartCoroutine(ShowContentOfLocations());
            StartCoroutine(SetTransitionFalse());
        }
            
    }

    public void CallTasksPanel() 
    {
        if (_currentOpenedPanel != _panels.TasksPanel && isTransitioningBtwLeftPanels == false)
        {
            StartCoroutine(ShowContentOfTasks());
            StartCoroutine(SetTransitionFalse());
        }

    }


    public void CallCarsPanel()
    {
        if (_currentOpenedPanel != _panels.CarsPanel && isTransitioningBtwLeftPanels == false)
        {
            StartCoroutine(ShowContentOfCars());
            StartCoroutine(SetTransitionFalse());
            //print("WTF");
        }
    }


    private IEnumerator ShowContentOfLocations()
    {
        isTransitioningBtwLeftPanels = true;
        if (_currentOpenedPanel == _panels.CarsPanel)
            DownScaleObjects(_contentOfCars, _carsPanel);
        else
            DownScaleObjects(_contentOfCars, _tasksPanel);
        DownScaleObjects(_contentOfCars, _carsPanel);
        yield return new WaitForSeconds(0.4f);
        _locationsPanel.SetActive(true);
        UPScaleObjects(_contentOfLocations);
        _currentOpenedPanel = _panels.LocationsPanel;
    }


    private IEnumerator ShowContentOfTasks()
    {
        isTransitioningBtwLeftPanels = true;
        if(_currentOpenedPanel == _panels.CarsPanel)
            DownScaleObjects(_contentOfCars, _carsPanel);
        else
            DownScaleObjects(_contentOfLocations, _locationsPanel);
        yield return new WaitForSeconds(0.4f);
        _tasksPanel.SetActive(true);
        UPScaleObjects(_contentOfTasks);
        _currentOpenedPanel = _panels.TasksPanel;
    }


    private IEnumerator ShowContentOfCars()
    {
        isTransitioningBtwLeftPanels = true;
        if (_currentOpenedPanel == _panels.TasksPanel)
            DownScaleObjects(_contentOfTasks, _tasksPanel);
        else
            DownScaleObjects(_contentOfLocations, _locationsPanel);
        DownScaleObjects(_contentOfLocations,_locationsPanel);
        yield return new WaitForSeconds(0.4f);
        _carsPanel.SetActive(true);
        UPScaleObjects(_contentOfCars);
        _currentOpenedPanel = _panels.CarsPanel;
    }

    private IEnumerator SetTransitionFalse()
    {
        isTransitioningBtwLeftPanels = true;
        yield return new WaitForSeconds(1f);
        isTransitioningBtwLeftPanels = false;
    }

    private void PanelsStartAnim()
    {
        _firstCanvas.gameObject.SetActive(true);
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


    private void DownScaleObjects(List<Transform> objects, GameObject panelToTurnOFF)
    {
        foreach (Transform obj in objects)
        {
            obj.gameObject.SetActive(true);

            Vector3 startScale = obj.localScale;
            Vector3 endScale = new Vector3(0f, 0f, 0f);

            obj.DOScale(endScale, _downScaleDuration).OnComplete(() =>
            {
                panelToTurnOFF.SetActive(false);
            }); ;
        }
    }


    private void SetCurrentCarByPrefs()
    {
        if (PlayerPrefs.GetString("Car") == "WhitePoliceCar" && PlayerPrefs.GetString("WhitePoliceCar") == "Unlocked")
        {
            CheckCar(1);
        }
        else if (PlayerPrefs.GetString("Car") == "SportCar" && PlayerPrefs.GetString("SportCar") == "Unlocked")
        {
            CheckCar(2);
        }
        else if (PlayerPrefs.GetString("Car") == "SciFiCar" && PlayerPrefs.GetString("SciFiCar") == "Unlocked")
        {
            CheckCar(3);
        }
        else
        {
            CheckCar(0);
            PlayerPrefs.SetString("Car", "PoliceCar");
        }

    }


    private void SetCarPlayerPrefs()
    {
        if (_currentCarIndex == 0)
        {
            PlayerPrefs.SetString("Car", "PoliceCar");
        }

        if (_currentCarIndex == 1 && PlayerPrefs.GetString("WhitePoliceCar") == "Unlocked")
        {
            PlayerPrefs.SetString("Car", "WhitePoliceCar");
        }

        if (_currentCarIndex == 2 && PlayerPrefs.GetString("SportCar") == "Unlocked")
        {
            PlayerPrefs.SetString("Car", "SportCar");
        }

        if (_currentCarIndex == 3 && PlayerPrefs.GetString("SciFiCar") == "Unlocked")
        {
            PlayerPrefs.SetString("Car", "SciFiCar");
        }
    }


    public IEnumerator MoveCarToGarage(Transform tr)
    {
        tr.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        tr.position = _garageStartPosition;
        tr.DOMove(_garageEndPosition, _animationDuration).SetAutoKill(false).OnComplete(() =>
        {
            //Debug.Log("Машина прибыла в гараж");
            isTransitioningBtwCars = false;
        });
    }


    public void MoveCarToStreet(Transform tr)
    {
        tr.position = _garageEndPosition;
        tr.DOMove(_streetEndPosition, _animationDuration).SetAutoKill(false).OnComplete(() =>
        {
            //Debug.Log("Машина прибыла на улицу");
            tr.gameObject.SetActive(false);
        });
    }


    public void CheckCar(int indexInCarsList)
    {

        if (_currentCarIndex != indexInCarsList && isTransitioningBtwCars == false)
        {
            _canvasWithSliderCont.GetComponent<SliderVolumeController>().ChangeCarSound();
            isTransitioningBtwCars = true;
            MoveCarToStreet(carsInGarage[_currentCarIndex]);
            StartCoroutine(MoveCarToGarage(carsInGarage[indexInCarsList]));
            _currentCarIndex = indexInCarsList;
            SetCarPlayerPrefs();
        }
    }

    public bool GetIsTransitioningBtwCars()
    {
        return isTransitioningBtwCars;
    }

}
