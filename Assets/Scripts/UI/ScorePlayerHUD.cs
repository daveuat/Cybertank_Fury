using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScorePlayerHUD : MonoBehaviour
{
    public TextMeshProUGUI scoreTextTMPro;
    public TextMeshProUGUI highscoreTextTMPro;

    public int score = 0;
    private int highscore = 0;

    public static ScorePlayerHUD instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadHighScore();
        UpdateScoreText();
        UpdateHighScoreText();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateScoreText();
        UpdateHighScoreText();
    }

    public void UpdateScore(int value)
    {
        score += value;

        if (score < 0)
            score = 0;

        UpdateScoreText();

        if (score > highscore)
        {
            highscore = score;
            UpdateHighScoreText();
            SaveHighScore();
        }
    }


    private void LoadHighScore()
    {
        highscore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highscore);
        PlayerPrefs.Save();
    }

    private void UpdateScoreText()
    {
        scoreTextTMPro.text = score.ToString();
    }

    private void UpdateHighScoreText()
    {
        highscoreTextTMPro.text = highscore.ToString();
    }

    public static void ResetScore()
    {
        if (instance != null)
        {
            instance.score = 0;
            instance.UpdateScoreText();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SaveHighScore();
    }
}
