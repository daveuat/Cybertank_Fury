using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int scoreValue = 100; // The score value given to the player when this enemy tank is destroyed

    public void OnEnemyDeath()
    {
        Debug.Log("Enemy died"); // For testing purposes
        if (ScorePlayerHUD.instance != null)
        {
            ScorePlayerHUD.instance.UpdateScore(scoreValue);
            Debug.Log("Score updated: " + ScorePlayerHUD.instance.score); // Access 'score' directly
        }
        else
        {
            Debug.LogWarning("ScorePlayerHUD instance is null!");
        }
    }
}
