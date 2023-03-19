using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CanvasTransition : MonoBehaviour
{
    public CanvasGroup firstCanvas;
    public CanvasGroup secondCanvas;
    public Button playButton;

    private bool isTransitioning = false;

    private void Start()
    {
        // Скрываем второй канвас при запуске
        secondCanvas.alpha = 0f;

        // При нажатии на кнопку Play запускаем анимацию перехода
        playButton.onClick.AddListener(StartTransition);
    }

    private void StartTransition()
    {
        if (isTransitioning) return;
        isTransitioning = true;

        // Анимация отодвигания первого канваса влево
        firstCanvas.transform
            .DOMoveX(firstCanvas.transform.position.x - 1000f, 1f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                // Когда первый канвас отодвинулся, появляем второй канвас с постепенным увеличением прозрачности
                secondCanvas.gameObject.SetActive(true);
                secondCanvas.alpha = 0f;
                secondCanvas.DOFade(1f, 1f).OnComplete(() =>
                {
                    // Когда второй канвас появился, скрываем первый канвас и сбрасываем его позицию
                    firstCanvas.gameObject.SetActive(false);
                    firstCanvas.transform.position = new Vector3(0f, firstCanvas.transform.position.y, firstCanvas.transform.position.z);
                    isTransitioning = false;
                });
            });
    }
}
