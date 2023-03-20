using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    private const float UpdateInterval = 0.5f; // Интервал обновления
    private int framesCount; // Количество кадров за интервал
    private float framesTime; // Время кадров за интервал
    private TextMeshProUGUI fpsText; // Компонент TMP_Text для отображения FPS

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
