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
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSounds;
    [Header("Keys")]
    [SerializeField] private string _saveMusicVolumeKey;
    [SerializeField] private string _saveSoundsVolumeKey;
    [Header("Tags")]
    [SerializeField] private string _sliderMusicTag;
    [SerializeField] private string _sliderSoundsTag;
    [Header("Parametres")]
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _soundsVolume;

    private string _activSceneName;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().name == "Garage Scene")
        {
            _musicSource.clip = _musicForLobby[Random.Range(0, _musicForLobby.Length - 1)];
            _activSceneName = "Garage Scene";
        }
        _musicSource.volume = PlayerPrefs.GetFloat("BG_MUSIC");
        _musicSource.volume = PlayerPrefs.GetFloat("BG_SOUNDS");
        /*if (PlayerPrefs.HasKey(_saveMusicVolumeKey))
        {
            _musicVolume = PlayerPrefs.GetFloat(_saveMusicVolumeKey);
            _musicSource.volume = _musicVolume;
            GameObject sliderMusicObj = GameObject.FindWithTag(_sliderMusicTag);
            if (sliderMusicObj != null)
            {
                _sliderMusic = sliderMusicObj.GetComponent<Slider>();
                _sliderMusic.value = _musicVolume;
            }
        }/*else
        {
            _musicVolume = 0.5f;
            PlayerPrefs.SetFloat(_saveMusicVolumeKey, _musicVolume);
            _musicSource.volume = _musicVolume;
        }*/

        /*if (PlayerPrefs.HasKey(_saveSoundsVolumeKey))
        {
            _soundsVolume = PlayerPrefs.GetFloat(_saveSoundsVolumeKey);
            _soundsSource.volume = _soundsVolume;

            GameObject sliderSoundsObj = GameObject.FindWithTag(_sliderSoundsTag);
            if (sliderSoundsObj != null)
            {
                _sliderSounds = sliderSoundsObj.GetComponent<Slider>();
                PlayerPrefs.SetFloat(_saveSoundsVolumeKey, _soundsVolume);
                _sliderSounds.value = _soundsVolume;
            }
        }/*else
        {
            _soundsVolume = 0.5f;
            _soundsSource.volume = _soundsVolume;
        }*/

    }
    private void LateUpdate()
    {
        CheckThisScene();
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
            _musicSource.volume = _musicVolume;
        }
        if (sliderSoundsObj != null)
        {
            _sliderSounds = sliderSoundsObj.GetComponent<Slider>();
            _soundsVolume = _sliderSounds.value;
            if(_soundsSource.volume != _soundsVolume)
            {
                PlayerPrefs.SetFloat(_saveSoundsVolumeKey, _soundsVolume);
            }
            _soundsSource.volume = _soundsVolume;
        }
    }

    private void CheckThisScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName != _activSceneName)
        {
            if(sceneName == "Garage Scene")
            {
                _musicSource.clip = _musicForLobby[Random.Range(0, _musicForLobby.Length -1)];
                _musicSource.Play();
                _activSceneName = "Garage Scene";
            }
            else
            {
                _musicSource.clip = _musicForPlay[Random.Range(0, 3)];
                _musicSource.Play();
                _activSceneName = sceneName;
            }
            
        }
    }
}
