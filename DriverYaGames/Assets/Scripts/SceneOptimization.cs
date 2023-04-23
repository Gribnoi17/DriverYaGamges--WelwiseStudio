using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class SceneOptimization : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToDestroyOnPhone;
    [SerializeField] private SceneType _sceneType = SceneType.Location;
    [SerializeField] private UniversalRenderPipelineAsset pipelineAsset;
    public Camera _mainCamera;

    [SerializeField] private Skybox[] _skyboxes;

    [Header("Fog settings")]
    [SerializeField] private float _fogStartDistance = 0f;
    [SerializeField] private float _fogEndDistance = 15f;
    [SerializeField] private float _fogDensity = 0.1f;


    [DllImport("__Internal")]
    private static extern int detectDeviceUser();

    private int _deviceType;

    public enum SceneType
    {
        Menu,
        Location
    }


    private void Awake()
    {
        //PlayerPrefs.DeleteAll();
        foreach(Skybox sky in _skyboxes)
        {
            //sky.material.
        }
        
        if (_sceneType == SceneType.Menu)
            try
            {
                GetDevice();
            }
            catch
            {
                PlayerPrefs.SetInt("IsMobile", 1);
            }

        if (PlayerPrefs.GetInt("IsMobile") == 0)
        {
            foreach (GameObject obj in objectsToDestroyOnPhone)
            {
                obj.SetActive(true);
            }
            SetPCGraphicsSettings();

        }
        else
        {
            foreach (GameObject obj in objectsToDestroyOnPhone)
            {
                Destroy(obj);
            }
            SetMobileGraphicsSettings();
            //SetLinearFog();
        }
    }

    private void SetLinearFog()
    {
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = _fogStartDistance;
        RenderSettings.fogEndDistance = _fogEndDistance;
    }

    private void SetExponentialFog()
    {
        RenderSettings.fogMode = FogMode.Exponential;
        RenderSettings.fogDensity = _fogDensity;
    }

    private void GetDevice()
    {
        _deviceType = detectDeviceUser();

        if (_deviceType == 1)
        {
            PlayerPrefs.SetInt("IsMobile", 1);
            Debug.Log("-------------------------------------------“≈À≈‘ŒÕ-------------------");
        }
        else if (_deviceType == 3)
        {
            PlayerPrefs.SetInt("IsMobile", 0);
            Debug.Log("-------------------------------------------ÔÍÍÍÍÍÍ-------------------");
        }
    }


    private void SetMobileGraphicsSettings()
    {
        //_mainCamera.targetTexture = new RenderTexture(1280, 720, 16, RenderTextureFormat.ARGB32);
        // ”ÒÚ‡ÌÓ‚Í‡ Render Scale
        QualitySettings.antiAliasing = (int)AntialiasingMode.None;

        pipelineAsset.renderScale = 0.7f;

        pipelineAsset.msaaSampleCount = 1;
        QualitySettings.shadows = UnityEngine.ShadowQuality.Disable;
        QualitySettings.realtimeReflectionProbes = false;
    }

    private void SetPCGraphicsSettings()
    {
        //_mainCamera.targetTexture = new RenderTexture(1920, 1080, 20, RenderTextureFormat.ARGB32);

        // ”ÒÚ‡ÌÓ‚Í‡ Render Scale
        QualitySettings.antiAliasing = (int)AntialiasingMode.FastApproximateAntialiasing;

        pipelineAsset.renderScale = 1.2f;

        pipelineAsset.msaaSampleCount = 4;

        QualitySettings.shadows = UnityEngine.ShadowQuality.HardOnly;
        QualitySettings.realtimeReflectionProbes = false;
    }

}
