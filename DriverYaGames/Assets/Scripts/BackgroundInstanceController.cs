using UnityEngine;

public class BackgroundInstanceController : MonoBehaviour
{
    [SerializeField] private string createdTag;
    private void Awake()
    {
        GameObject obj = GameObject.FindWithTag(createdTag);
        if (obj != null)
            Destroy(gameObject);
        else
        {
            tag = createdTag;
            DontDestroyOnLoad(this);
        }
    }
}
