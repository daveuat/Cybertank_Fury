using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public Toggle mapOfTheDayToggle;
    public TMP_InputField seedInputField;
    public Toggle twoPlayerToggle; 

    public bool useMapOfTheDay = false;
    public int seed = 0;
    public bool useTwoPlayers = false; 

    private RoomGenerator roomGenerator;

    private void Awake()
    {
        roomGenerator = FindObjectOfType<RoomGenerator>();

        if (roomGenerator != null)
        {
            LoadSettings();

            if (mapOfTheDayToggle != null)
                mapOfTheDayToggle.isOn = useMapOfTheDay;

            if (seedInputField != null)
                seedInputField.text = seed.ToString();

            if (twoPlayerToggle != null) 
            {
                twoPlayerToggle.isOn = useTwoPlayers;
                twoPlayerToggle.onValueChanged.AddListener(OnTwoPlayerToggleChanged);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        SaveSettings();
    }

    public void OnMapOfTheDayToggleChanged(bool value)
    {
        useMapOfTheDay = value;
    }

    public void OnSeedValueChanged(string value)
    {
        int.TryParse(value, out seed);
    }

    // Add this method
    public void OnTwoPlayerToggleChanged(bool value)
    {
        useTwoPlayers = value;
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("MapOfTheDay", useMapOfTheDay ? 1 : 0);
        PlayerPrefs.SetInt("Seed", seed);
        PlayerPrefs.SetInt("UseTwoPlayers", useTwoPlayers ? 1 : 0); 
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        useMapOfTheDay = PlayerPrefs.GetInt("MapOfTheDay", 0) == 1;
        seed = PlayerPrefs.GetInt("Seed", 0);
        useTwoPlayers = PlayerPrefs.GetInt("UseTwoPlayers", 0) == 1; 
    }

    public void OnPlayButtonClicked()
    {
        if (ScorePlayerHUD.instance != null)
        {
            ScorePlayerHUD.ResetScore();
        }
        SaveSettings();
        if (roomGenerator != null)
        {
            roomGenerator.useMapOfTheDay = useMapOfTheDay;
            roomGenerator.seed = seed;
            roomGenerator.useTwoPlayers = useTwoPlayers;
        }

        if (GameManager.instance != null)
        {
            GameManager.instance.isNewGame = true;
        }

        SceneManager.LoadScene("Room1");
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
