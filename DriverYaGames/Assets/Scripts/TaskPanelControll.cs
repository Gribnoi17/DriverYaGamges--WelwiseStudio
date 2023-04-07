using UnityEngine;
using TMPro;
using System.Collections;

public class TaskPanelControll : MonoBehaviour
{
    [SerializeField] private GameObject _taskPanel;
    [SerializeField] private TextMeshProUGUI _text;

    private Color _color;
    private CanvasRenderer _sprite;

    private Color _textColor;

    private bool _textFlicker;
    private bool _alreadyRunning;
    private void Start()
    {
        _sprite = _taskPanel.GetComponent<CanvasRenderer>();
        _color = _sprite.GetColor(); _color.a = 0f;
        _sprite.SetColor(_color);
        _textColor = _text.color; _textColor.a = 0f;
        _text.color = _textColor;
        _textFlicker = true;
    }

    private void UnVisible()
    {
        _textFlicker = false;
    }

    private IEnumerator Visible()
    {
        _alreadyRunning = true;
        yield return new WaitForSeconds(2);
        for (float f = 0.05f; f <= 1f; f += 0.05f)
        {
            _color.a = f;
            _sprite.SetColor(_color);
            yield return new WaitForSeconds(0.05f);
        }
        StartCoroutine(TextFlicker());
    }

    private IEnumerator TextFlicker()
    {
        while(_textFlicker)
        {
            for (float f = 0.05f; f <= 1f; f += 0.05f)
            {
                _textColor.a = f;
                _text.color = _textColor;
                yield return new WaitForSeconds(0.05f);
            }
            for (float f = 1f; f >= -0.05f; f -= 0.05f)
            {
                _textColor.a = f;
                _text.color = _textColor;
                yield return new WaitForSeconds(0.05f);
            }
        }
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            _textColor.a = f;
            _color.a = f;
            _text.color = _textColor;
            _sprite.SetColor(_color);
            yield return new WaitForSeconds(0.05f);
        }
        _alreadyRunning = false;    
        StopCoroutine(TextFlicker());
    }

    public void StartUnVisible()
    {
        UnVisible();
    }

    public void StartVisible()
    {
        if(!_alreadyRunning)
            StartCoroutine(Visible());
    }
}
