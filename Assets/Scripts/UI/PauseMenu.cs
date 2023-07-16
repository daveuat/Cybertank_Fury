using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI object
    public GameObject optionsMenuUI; // Reference to the options menu UI object
    public GameObject hudUI;
    public GameObject gameoverUI;

    private bool isPaused = false; // Flag to track if the game is paused
    private bool isInOptionsMenu = false; // Flag to track if the options menu is active


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f; // Set the time scale to 0 to pause the game
        pauseMenuUI.SetActive(true); // Activate the pause menu UI object
    }

    private void ResumeGame()
    {
        if (isInOptionsMenu)
        {
            ReturnToPauseMenu();
        }

        isPaused = false;
        Time.timeScale = 1f; // Set the time scale back to 1 to resume the game
        pauseMenuUI.SetActive(false); // Deactivate the pause menu UI object
    }

    public void ShowOptionsMenu()
    {
        isInOptionsMenu = true;
        pauseMenuUI.SetActive(false); // Deactivate the pause menu UI object
        optionsMenuUI.SetActive(true); // Activate the options menu UI object
    }

    public void ReturnToPauseMenu()
    {
        isInOptionsMenu = false;
        optionsMenuUI.SetActive(false); // Deactivate the options menu UI object
        pauseMenuUI.SetActive(true); // Activate the pause menu UI object
    }

    public void ReturnToMainMenu()
    {
        hudUI.SetActive(false); // Deactivate the HUD UI object
        pauseMenuUI.SetActive(false); // Deactivate the pause menu UI object
        gameoverUI.SetActive(false);

        PlayerPrefs.SetInt("Lives", 3);
        PlayerPrefs.Save();

        Time.timeScale = 1f; // Set the time scale back to 1 to ensure the game is running normally



        //ScorePlayerHUD.ResetScore(); // Reset the score
        GameManager.instance.isGameOver = false;

        SceneManager.LoadScene("Menu"); // Load the "Menu" scene by its name

    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop the game in the Unity Editor
#else
        Application.Quit(); // Quit the game in a standalone build
#endif
    }
}
