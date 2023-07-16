using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public int enemiesToKill = 1;
    public int scorePerEnemy = 0;
    public ScorePlayerHUD scorePlayerHUD;

    private void Start()
    {
        scorePlayerHUD = FindObjectOfType<ScorePlayerHUD>();
    }

    public void EnemyKilled()
    {
        enemiesToKill--;
        if (scorePlayerHUD != null)
        {
            scorePlayerHUD.UpdateScore(scorePerEnemy);
        }

        if (enemiesToKill <= 0)
        {
            TransitionToNextScene();
        }
    }

    private void TransitionToNextScene()
    {
        // Save the current lives value before transitioning to the next scene
        PlayerPrefs.SetInt("Lives", GameManager.instance.currentLives);
        PlayerPrefs.Save();

        // Load the next scene (assuming the scene build index is 1)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // It loads the next scene in the list
    }
}
