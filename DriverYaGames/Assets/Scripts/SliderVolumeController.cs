using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeController : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;

    private MusicController _musCont;

    private void Start()
    {
        try
        {
            _musicSlider.value = PlayerPrefs.GetFloat("BG_MUSIC");
            _soundsSlider.value = PlayerPrefs.GetFloat("BG_SOUNDS");
        }
        catch
        {
            print("catch error");
        }
  
        _musCont = FindObjectOfType<MusicController>();
    }

    public void changeMusicSliderEvent()
    {
        _musCont.ChangeVolumeMusic();
    }

    public void changeSoundsSliderEvent()
    {
        _musCont.ChangeVolumeSounds();
    }
}
