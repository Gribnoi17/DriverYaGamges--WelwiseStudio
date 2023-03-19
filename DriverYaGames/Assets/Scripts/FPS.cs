using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private const float UpdateInterval = 0.5f; // �������� ����������
    private int framesCount; // ���������� ������ �� ��������
    private float framesTime; // ����� ������ �� ��������
    private TextMeshProUGUI fpsText; // ��������� TMP_Text ��� ����������� FPS

    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        framesCount++;
        framesTime += Time.deltaTime;

        if (framesTime >= UpdateInterval)
        {
            float fps = framesCount / framesTime;
            fpsText.text = $"FPS: {fps:F1}";
            framesCount = 0;
            framesTime = 0;
        }
    }
}
