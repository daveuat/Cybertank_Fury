using UnityEngine;
using TMPro;

public class Lives : MonoBehaviour
{
    public int startingLives = 3; // Number of starting lives

    private int currentLives = 3; // Current number of lives

    private int maxLives = 3; // Maximum number of lives
    private TextMeshProUGUI livesCounter; // Reference to the TextMeshProUGUI component for displaying lives counter

    private void Start()
    {
        currentLives = startingLives; // Initialize the current lives

        GameObject hud = GameObject.FindGameObjectWithTag("Lives"); // Find the HUD by tag
        //livesCounter = hud.GetComponentInChildren<TextMeshProUGUI>(); // Find the "LivesNum" TextMeshProUGUI component in the HUD

        if (livesCounter != null && livesCounter.gameObject.name == "LivesNum")
        {
            UpdateLivesCounter(); // Update the initial lives counter display
        }
    }

    public void TakeDamage(int damage)
    {
        currentLives -= damage; // Decrease the current lives by the damage amount

        if (currentLives <= 0)
        {
            ShowGameOverScreen(); // If lives reach zero, show the game over screen
        }
        else
        {
            RespawnPlayerTank(); // Respawn the player tank
        }

        if (livesCounter != null && livesCounter.gameObject.name == "LivesNum")
        {
            UpdateLivesCounter(); // Update the lives counter display
        }
    }

    private void ShowGameOverScreen()
    {
        // Show the game over screen logic
    }

    private void RespawnPlayerTank()
    {
        // Respawn the player tank logic
    }

    private void UpdateLivesCounter()
    {
        livesCounter.text = currentLives.ToString(); // Update the lives counter text
    }
}
