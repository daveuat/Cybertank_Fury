using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class Leaderboard : MonoBehaviour
{
    int leaderboardId = 16198;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public IEnumerator SubmitScoreRoutine(int scoreToUpload)
    {
        bool done = false;
        string playerId = PlayerPrefs.GetString("PlayerId");
        LootLockerSDKManager.SubmitScore(playerId, scoreToUpload, leaderboardId, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Score Submitted");
                done = true;
            }
            else
            {
                Debug.Log("Score Submission Failed" + response.Error);
                done = true;
            }
        });
        yield return new WaitUntil(() => done == true); // Wait until operation is done
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
