using UnityEngine;

public class PointsGiver : MonoBehaviour
{
    // The score value given to the player when they collect a gold bar
    public int scoreValue = 500;

    // Detects the collision with the player
    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object is the player
        if (other.gameObject.tag == "Player")
        {
            // Get the player's HUD script and update the score
            ScorePlayerHUD scorePlayerHUD = FindObjectOfType<ScorePlayerHUD>();
            if (scorePlayerHUD != null)
            {
                scorePlayerHUD.UpdateScore(scoreValue);
            }

            // Destroys the gold bar after the player collected it
            Destroy(gameObject);
        }
    }
}