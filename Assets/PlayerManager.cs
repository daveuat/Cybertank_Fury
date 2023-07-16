using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LootLocker.Requests;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoginRoutine());
    }

    IEnumerator LoginRoutine()
    {
        bool done = false;
        yield return new WaitForSeconds(1f);
        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (response.success)
            {
                Debug.Log("Login Success");
                PlayerPrefs.SetString("PlayerId", response.player_id.ToString());
                done = true;
            }
            else
            {
                Debug.Log("Login Failed");
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
