using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeController : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;
    [SerializeField] private AudioClip _uiClick;
    [SerializeField] private AudioClip _playClick;

    private MusicController _musCont;
    [SerializeField]private AudioSource _source;
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

    public void PauseClick()
    {
        _musCont.OffEngine();
    }
    public void ContineClick()
    {
        _musCont.OnEngine();
    }

    public void ClickForButtonSound()
    {
        _source.clip = _uiClick;
        _source.Play();
    }
    public void ClickForPlaySound()
    {
        _source.clip = _playClick;
        _source.Play();
    }
}
