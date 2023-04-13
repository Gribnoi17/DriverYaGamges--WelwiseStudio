using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TextLocalization : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private string _ruText;
    [SerializeField] private string _enText;
    [SerializeField] private string _trText;

    [Header("Fonts")]
    [SerializeField] private TMP_FontAsset _ruFont;
    [SerializeField] private TMP_FontAsset _enFont;
    [SerializeField] private TMP_FontAsset _trFont;


    private TextMeshProUGUI _textM;

    private void Start()
    {
        _textM = gameObject.GetComponent<TextMeshProUGUI>();

        if(PlayerPrefs.GetString("Language") == "ru")
        {
            _textM.text = _ruText;
            _textM.font = _ruFont;
        }
        else if(PlayerPrefs.GetString("Language") == "tr")
        {
            _textM.text = _trText;
            _textM.font = _trFont;
        }
        else
        {
            _textM.text = _enText;
            _textM.font = _enFont;
        }
    }

}
