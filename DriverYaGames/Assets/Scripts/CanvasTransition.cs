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
        // �������� ������ ������ ��� �������
        secondCanvas.alpha = 0f;

        // ��� ������� �� ������ Play ��������� �������� ��������
        playButton.onClick.AddListener(StartTransition);
    }

    private void StartTransition()
    {
        if (isTransitioning) return;
        isTransitioning = true;

        // �������� ����������� ������� ������� �����
        firstCanvas.transform
            .DOMoveX(firstCanvas.transform.position.x - 1000f, 1f)
            .SetEase(Ease.OutQuart)
            .OnComplete(() =>
            {
                // ����� ������ ������ �����������, �������� ������ ������ � ����������� ����������� ������������
                secondCanvas.gameObject.SetActive(true);
                secondCanvas.alpha = 0f;
                secondCanvas.DOFade(1f, 1f).OnComplete(() =>
                {
                    // ����� ������ ������ ��������, �������� ������ ������ � ���������� ��� �������
                    firstCanvas.gameObject.SetActive(false);
                    firstCanvas.transform.position = new Vector3(0f, firstCanvas.transform.position.y, firstCanvas.transform.position.z);
                    isTransitioning = false;
                });
            });
    }
}
