using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;

    public static LevelManager instance;

    public GameObject levelManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(levelManager);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        if (soundManager != null)
        {
            PlayMusicBasedOnScene();
        }

        SceneManager.sceneLoaded += OnSceneChange;
    }

    private void PlayMusicBasedOnScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        int trackIndex = GetTrackIndexBySceneName(sceneName);

        if (soundManager != null)
        {
            soundManager.PlayMusicByTrackIndex(trackIndex);
        }
    }

    private void OnSceneChange(Scene scene, LoadSceneMode mode)
    {
        PlayMusicBasedOnScene();
    }

    private int GetTrackIndexBySceneName(string sceneName)
    {
        switch (sceneName)
        {
            case "Menu":
                return 0;
            case "Room1":
                return 1;
            case "Room2":
                return 2;
            // Add more cases for other scenes and their respective track indices
            default:
                return 0;
        }
    }
}
