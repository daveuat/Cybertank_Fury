using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance { get; private set; }

    private ThirdPersonCamera thirdPersonCamera;

    private void Awake()
    {
        // Implementing the singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        thirdPersonCamera = GetComponent<ThirdPersonCamera>();
    }

    // This method can be called when a new scene is loaded or a new player is spawned
    public void UpdateCameraTarget(Transform newTarget)
    {
        if (thirdPersonCamera != null)
        {
            thirdPersonCamera.target = newTarget;
        }
    }
}
