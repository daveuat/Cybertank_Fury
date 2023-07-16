using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LivesManager : MonoBehaviour
{
    public static LivesManager instance;

    public GameObject playerPrefabPlayer1;
    public GameObject playerPrefabPlayer2;
    public TMP_Text livesText;
    public GameObject gameOverScreen; // Reference to the Game Over screen
    public string menuSceneName = "Menu";

    public int startingLives = 3; // Number of starting lives

    private int livesPlayer1;
    private int livesPlayer2;
    private bool isTwoPlayerMode;

    private Vector3 respawnPosition = new Vector3(0f, 0f, 0f); // Respawn position at room (0, 0)

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        livesPlayer1 = startingLives;
        isTwoPlayerMode = (playerPrefabPlayer2 != null);
        //livesPlayer2 = isTwoPlayerMode ? playerPrefabPlayer2.GetComponent<Health>().startingLives : 0;

        UpdateLivesText();
    }

    public void ReduceLife(GameObject player)
    {
        if (player == playerPrefabPlayer1)
        {
            livesPlayer1--;
            if (livesPlayer1 <= 0)
            {
                ShowGameOverScreen();
                return;
            }
            RespawnPlayer(playerPrefabPlayer1);
        }
        else if (player == playerPrefabPlayer2)
        {
            livesPlayer2--;
            if (livesPlayer2 <= 0)
            {
                ShowGameOverScreen();
                return;
            }
            RespawnPlayer(playerPrefabPlayer2);
        }

        UpdateLivesText();
    }

    private void UpdateLivesText()
    {
        string livesTextString = $"{livesPlayer1}";

        livesText.text = livesTextString;
    }

    private void RespawnPlayer(GameObject playerPrefab)
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player); // Destroy the existing player
        }
        Instantiate(playerPrefab, respawnPosition, Quaternion.identity); // Instantiate a new player at the respawn position
    }

    private void ShowGameOverScreen()
    {
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true); // Show the game over screen
        }
    }
}
