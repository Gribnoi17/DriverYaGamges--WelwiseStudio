using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MusicController : MonoBehaviour
{
    [Header("�omponents")]
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
        if (PlayerPrefs.HasKey(_saveMusicVolumeKey))
            _musicVolume = PlayerPrefs.GetFloat(_saveMusicVolumeKey);
        else
            PlayerPrefs.SetFloat(_saveMusicVolumeKey, _musicVolume);

        _mixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, _musicVolume));

        if (PlayerPrefs.HasKey(_saveMusicVolumeKey))
            _musicVolume = PlayerPrefs.GetFloat(_saveMusicVolumeKey);
        else
            PlayerPrefs.SetFloat(_saveMusicVolumeKey, _musicVolume);

        
        _mixerGroup.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, _soundsVolume));
        FindSliders();
    }
    private void LateUpdate()
    {
        CheckCurrentScene();
    }

    public void ChangeVolumeMusic()
    {
        if (_sliderMusic != null)
        {
            print(_sliderMusic.value);
            _musicVolume = _sliderMusic.value;
            PlayerPrefs.SetFloat(_saveMusicVolumeKey, _musicVolume);
            _mixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, _musicVolume));
            _musicSource.volume = _musicVolume;
        }
    }
    public void ChangeVolumeSounds()
    {
        if (_sliderSounds != null)
        {         
            _soundsVolume = _sliderSounds.value;
            PlayerPrefs.SetFloat(_saveSoundsVolumeKey, _soundsVolume);
            _mixerGroup.audioMixer.SetFloat("SoundsVolume", Mathf.Lerp(-80, 0, _soundsVolume));
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

    private void CheckCurrentScene()
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

    public void SetUpSlidersStats()
    {
        if(_sliderMusic != null)
            _sliderMusic.value = PlayerPrefs.GetFloat(_saveMusicVolumeKey);
        if (_sliderSounds != null)
            _sliderSounds.value = PlayerPrefs.GetFloat(_saveSoundsVolumeKey);
    }

    public void FindSliders()
    {
        try
        {
            _sliderMusic = GameObject.FindGameObjectWithTag(_sliderMusicTag).GetComponent<Slider>();
            //_sliderMusic.onValueChanged.AddListener(ChangeVolumeMusic);
            _sliderMusic = GameObject.FindGameObjectWithTag(_sliderSoundsTag).GetComponent<Slider>();
        }
        catch
        {
            Debug.LogWarning("�������� ����������� ��������� �� �������, ������ ����� ��� ���������");
        }
    }
}
