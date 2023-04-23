using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeController : MonoBehaviour
{
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _soundsSlider;
    [SerializeField] private AudioClip _uiClick;
    [SerializeField] private AudioClip _playClick;
    [SerializeField] private AudioClip _changeCar;
    [SerializeField] private AudioClip _buyFire;
    [SerializeField] private AudioClip _winSound;

    private MusicController _musCont;
    [SerializeField]private AudioSource _source;
    private void Start()
    {
        _musCont = FindObjectOfType<MusicController>();
        try
        {
            _musicSlider.value = PlayerPrefs.GetFloat("BG_MUSIC");
        }
        catch
        {
            
        }
        try
        {
            _soundsSlider.value = PlayerPrefs.GetFloat("BG_SOUNDS");
        }
        catch
        {

        }
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

    public void ChangeCarSound()
    {
        _source.PlayOneShot(_changeCar);
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

    public void BuyButtonSound()
    {
        _source.clip = _uiClick;
        _source.PlayOneShot(_buyFire);
    }

    public void WinSoundsStart()
    {
        StartCoroutine(WinSounds());
    }

    private IEnumerator WinSounds()
    {
        _source.PlayOneShot(_winSound);
        while (true)
        {
            _source.PlayOneShot(_buyFire);
            yield return new WaitForSeconds(1f);
        }
    }
}
