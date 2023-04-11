using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeController : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;
    private void Awake()
    {
        _musicSlider.value = PlayerPrefs.GetFloat("BG_MUSIC");
        _soundsSlider.value = PlayerPrefs.GetFloat("BG_SOUNDS");
    }
}
