using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class MusicController : MonoBehaviour
{
    [Header("Ñomponents")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioClip[] _musicForPlay;
    [SerializeField] private AudioClip[] _musicForLobby;
    [Header("Keys")]
    [SerializeField] private string _saveMusicVolumeKey;
    [SerializeField] private string _saveSoundsVolumeKey;
    [Header("Tags")]
    [SerializeField] private string _sliderMusicTag;
    [SerializeField] private string _sliderSoundsTag;
    [Header("Sliders")]
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSounds;
    [Header("Parametres")]
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _soundsVolume;

    private void Awake()
    {
        if (PlayerPrefs.HasKey(_saveMusicVolumeKey))
        {
            _musicVolume = PlayerPrefs.GetFloat(_saveMusicVolumeKey);
            _musicSource.volume = _musicVolume;
            GameObject sliderMusicObj = GameObject.FindWithTag(_sliderMusicTag);
            if (sliderMusicObj != null)
            {
                _sliderMusic = sliderMusicObj.GetComponent<Slider>();
                _sliderMusic.value = _musicVolume;
            }else
            {
                _musicVolume = 0.5f;
                _musicSource.volume = _musicVolume;
            }
        }
        if (PlayerPrefs.HasKey(_saveSoundsVolumeKey))
        {
            _soundsVolume = PlayerPrefs.GetFloat(_saveSoundsVolumeKey);
            _soundsSource.volume = _soundsVolume;
        }
        GameObject sliderSoundsObj = GameObject.FindWithTag(_sliderSoundsTag);
        if(sliderSoundsObj != null)
        {
            _sliderSounds = sliderSoundsObj.GetComponent<Slider>();
            _sliderSounds.value = _soundsVolume;
        }else
        {
            _soundsVolume = 0.05f;
            _soundsSource.volume = _soundsVolume;
        }
    }
    private void LateUpdate()
    {
        GameObject sliderMusicObj = GameObject.FindWithTag(_sliderMusicTag);
        GameObject sliderSoundsObj = GameObject.FindWithTag(_sliderSoundsTag);
        if(sliderMusicObj != null)
        {
            _sliderMusic = sliderMusicObj.GetComponent<Slider>();
            _musicVolume = _sliderMusic.value;
            if(_musicSource.volume != _musicVolume)
            {
                PlayerPrefs.SetFloat(_saveMusicVolumeKey, _musicVolume);
            }
        }
        if (sliderSoundsObj != null)
        {
            _sliderSounds = sliderSoundsObj.GetComponent<Slider>();
            _soundsVolume = _sliderSounds.value;
            if(_soundsSource.volume != _soundsVolume)
            {
                PlayerPrefs.SetFloat(_saveSoundsVolumeKey, _soundsVolume);
            }
        }
    }
}
