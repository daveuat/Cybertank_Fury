using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public Toggle mapOfTheDayToggle; // Reference to the "map of the day" checkbox in the options menu
    public TMP_InputField seedInputField; // Reference to the seed input field in the options menu

    public bool useMapOfTheDay = false;
    public int seed = 0;

    private RoomGenerator roomGenerator; // Reference to the RoomGenerator script

    private void Awake()
    {
        roomGenerator = FindObjectOfType<RoomGenerator>(); // Find the RoomGenerator script in the scene

        if (roomGenerator != null)
        {
            // Load settings from PlayerPrefs
            LoadSettings();

            // Set UI elements based on loaded settings
            if (mapOfTheDayToggle != null)
                mapOfTheDayToggle.isOn = useMapOfTheDay;

            if (seedInputField != null)
                seedInputField.text = seed.ToString();
        }
    }

    private void OnDestroy()
    {
        // Save settings before destroying the script
        SaveSettings();
    }

    public void OnMapOfTheDayToggleChanged(bool value)
    {
        useMapOfTheDay = value; // Set the useMapOfTheDay bool based on the toggle value
    }

    public void OnSeedValueChanged(string value)
    {
        int parsedSeed;
        if (int.TryParse(value, out parsedSeed))
        {
            seed = parsedSeed; // Set the seed based on the input field value
        }
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetInt("MapOfTheDay", useMapOfTheDay ? 1 : 0);
        PlayerPrefs.SetInt("Seed", seed);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        useMapOfTheDay = PlayerPrefs.GetInt("MapOfTheDay", 0) == 1;
        seed = PlayerPrefs.GetInt("Seed", 0);
    }

    public void OnPlayButtonClicked()
    {
        if (roomGenerator != null)
        {
            roomGenerator.useMapOfTheDay = useMapOfTheDay; // Set the useMapOfTheDay in RoomGenerator based on the options menu value
            roomGenerator.seed = seed; // Set the seed in RoomGenerator based on the options menu value
        }

        SaveSettings(); // Save the settings
        //ScorePlayerHUD.ResetScore(); // Reset the score
        SceneManager.LoadScene("Room1"); // Load the "Room1" scene by its name
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
