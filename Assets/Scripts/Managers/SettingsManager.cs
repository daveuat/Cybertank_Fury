using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public bool useMapOfTheDay = false;
    public int seed = 0;

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("MapOfTheDay", useMapOfTheDay ? 1 : 0);
        PlayerPrefs.SetInt("Seed", seed);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        useMapOfTheDay = PlayerPrefs.GetInt("MapOfTheDay", 0) == 1;
        seed = PlayerPrefs.GetInt("Seed", 0);
    }
}
