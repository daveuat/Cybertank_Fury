using LootLocker.Requests;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using LootLocker.Requests;
using LootLocker;

public class GameManager : MonoBehaviour
{
    public ScorePlayerHUD scorePlayerHUD;
    public static GameManager instance;

    public TMP_InputField playerNameInputField;


    public GameObject playerTankPrefab;
    public GameObject hudUI;
    public GameObject gameOverUI;
    public TMP_Text livesText; // Add this line to reference the Text component for lives display

    public GameObject playerTankInstance;
    public Vector3 spawnPosition = new Vector3(0, 0, 0); // Safe spawn position

    public int maxLives = 3;

    public bool isNewGame = true;
    public bool isGameOver = false;
    public bool isRespawning = false;

    public int currentLives = 3;

    public void Start()
    {
        // Instantiate player tank on game start and store it
        playerTankInstance = Instantiate(playerTankPrefab, spawnPosition, Quaternion.identity);
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(hudUI);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != "Menu")
        {
            currentLives = PlayerPrefs.GetInt("Lives", maxLives);
            UpdateLivesText(); // Update the lives text when a new scene is loaded
            RespawnPlayer();
        }

        if (scene.name == "Room1")
        {
            hudUI.SetActive(true);
        }
        if (scene.name == "Room2")
        {
            currentLives = PlayerPrefs.GetInt("Lives", maxLives);
            hudUI.SetActive(true);
        }
        if (scene.name == "Menu")
        {
            hudUI.SetActive(false);
        }
    }


    public void PlayerDied()
    {
        if (isGameOver || isRespawning)
        {
            return;
        }

        currentLives--;
        UpdateLivesText();

        if (currentLives <= 0)
        {
            GameOver();
            hudUI.SetActive(false);
        }
        else
        {
            RespawnPlayer();
        }
    }



    public void RespawnPlayer()
    {
        if (playerTankInstance != null)
        {
            isRespawning = true;

            playerTankInstance.transform.position = spawnPosition;
            playerTankInstance.SetActive(true); // Enable the player object

            Health playerHealth = playerTankInstance.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.currentHealth = playerHealth.startHealth;
                playerHealth.SetInvulnerability(3f);
                playerHealth.hasArmor = false; // Reset the armor status to false
                playerHealth.SetHealthBarColor(playerHealth.normalHealthColor); // Set the health bar color to green
                playerHealth.SetHealthUI(); // Update the health UI
            }

            isRespawning = false;
        }
    }



    public void GameOver()
    {
        isGameOver = true; // Set the flag to true when game over
        Time.timeScale = 0f;

        // Get the score from ScorePlayerHUD
        int score = scorePlayerHUD.score;

        // Check if the score is less than or equal to zero
        if (score <= 0)
        {
            // If so, set it to 1
            score = 1;
        }

        string playerName = PlayerPrefs.GetString("PlayerID"); // player name from PlayerPrefs

        // Create the payload
        LootLockerSubmitScoreRequest payload = new LootLockerSubmitScoreRequest
        {
            member_id = playerName, // Assign player name to member_id
            score = score // Assign score
        };

        // Submit score
        LootLockerAPIManager.SubmitScore(payload, "16198", (response) =>
        {
            if (response != null && response.success)
            {
                Debug.Log("Score submitted successfully to LootLocker");
            }
            else
            {
                Debug.Log("Failed to submit score to LootLocker");
            }
        });

        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
            ResetGame();
        }
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ResetGame()
    {
        isNewGame = true;
        isGameOver = false;
        ScorePlayerHUD.ResetScore();
        currentLives = maxLives; // reset currentLives
        PlayerPrefs.SetInt("Lives", maxLives); // save maxLives in PlayerPrefs
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 0f;
        hudUI.SetActive(false);
    }


    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Lives", maxLives);
        PlayerPrefs.Save();
    }

    private void UpdateLivesText()
    {
        if (livesText != null)
        {
            livesText.text = $"{currentLives}";
        }
    }

    public void SavePlayerName()
    {
        // Save the player name to PlayerPrefs
        string playerName = playerNameInputField.text;
        PlayerPrefs.SetString("PlayerID", playerName);
        PlayerPrefs.Save();
    }
}