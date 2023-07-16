using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance;

    public GameObject hudUI;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
