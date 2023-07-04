using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{
    public int enemiesToKill = 10;

    public void EnemyKilled()
    {
        enemiesToKill--;

        if (enemiesToKill <= 0)
        {
            TransitionToNextScene();
        }
    }

    private void TransitionToNextScene()
    {
        // Load the next scene (assuming the scene build index is 1)
        SceneManager.LoadScene(1);
    }
}
