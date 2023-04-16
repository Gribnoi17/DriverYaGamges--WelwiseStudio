using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [Header("Ñomponents")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundsSource;
    [SerializeField] private AudioClip[] _musicForPlay;
    [SerializeField] private AudioClip[] _musicForLobby;
    [SerializeField] private AudioMixerGroup _mixerGroup;
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
        if (SceneManager.GetActiveScene().name == "Garage Scene")
        {
            _musicSource.clip = _musicForLobby[Random.Range(0, _musicForLobby.Length - 1)];
            _activSceneName = "Garage Scene";
        }
    }
    private void Start()
    {
        _musicVolume = PlayerPrefs.GetFloat(_saveMusicVolumeKey);
        _mixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, _musicVolume));

        _soundsVolume = PlayerPrefs.GetFloat(_saveSoundsVolumeKey);
        _mixerGroup.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, _soundsVolume));
    }
    private void LateUpdate()
    {
        CheckThisScene();
    }

    public void ChangeVolumeMusic()
    {
        GameObject sliderMusicObj = GameObject.FindWithTag(_sliderMusicTag);

        if (sliderMusicObj != null)
        {
            _sliderMusic = sliderMusicObj.GetComponent<Slider>();
            _musicVolume = _sliderMusic.value;
            PlayerPrefs.SetFloat(_saveMusicVolumeKey, _musicVolume);
            _mixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, _musicVolume));
            /*if (_musicSource.volume != _musicVolume)
            {
                PlayerPrefs.SetFloat(_saveMusicVolumeKey, _musicVolume);
            }
            */
            _musicSource.volume = _musicVolume;
        }
    }
    public void ChangeVolumeSounds()
    {
        GameObject sliderSoundsObj = GameObject.FindWithTag(_sliderSoundsTag);
        if (sliderSoundsObj != null)
        {
            _sliderSounds = sliderSoundsObj.GetComponent<Slider>();
            _soundsVolume = _sliderSounds.value;
            PlayerPrefs.SetFloat(_saveSoundsVolumeKey, _soundsVolume);
            _mixerGroup.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, _soundsVolume));
            /*if (_soundsSource.volume != _soundsVolume)
            {
                PlayerPrefs.SetFloat(_saveSoundsVolumeKey, _soundsVolume);
            }*/
            _soundsSource.volume = _soundsVolume;
        }
    }

    public void OffEngine()
    {
        _mixerGroup.audioMixer.SetFloat("Engine", Mathf.Lerp(-80, 0, 0));
    }
    public void OnEngine()
    {
        _mixerGroup.audioMixer.SetFloat("Engine", Mathf.Lerp(-80, 0, 1));
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
