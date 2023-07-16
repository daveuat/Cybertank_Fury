using UnityEngine;
using UnityEngine.UI;

public class ResetScoreButton : MonoBehaviour
{
    public void ResetScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        PlayerPrefs.DeleteKey("CurrentScore");
        PlayerPrefs.DeleteKey("Score");
        PlayerPrefs.DeleteKey("Lives");
        PlayerPrefs.Save();

        if (ScorePlayerHUD.instance != null)
        {
            ScorePlayerHUD.instance.UpdateScore(-ScorePlayerHUD.instance.score);
        }
    }
}
