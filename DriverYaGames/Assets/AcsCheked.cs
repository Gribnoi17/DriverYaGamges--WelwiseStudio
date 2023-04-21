using UnityEngine;
using TMPro;

public class AcsCheked : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _valueAcs;
    [SerializeField] private GameObject _acs;

    void Update()
    {
        if(_acs.activeSelf)
        {
            _valueAcs.text = "true";
        }else
        {
            _valueAcs.text = "false";
        }
    }
}
