using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<PlayerController> players;
    public GameObject singletank1; // Reference to the TankPawn prefab

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            players = new List<PlayerController>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterPlayer(PlayerController player)
    {
        players.Add(player);
    }

    public void UnregisterPlayer(PlayerController player)
    {
        players.Remove(player);
    }

    public void SpawnPlayer()
    {
        // Create a new instance of the TankPawn and PlayerController
        GameObject tankObj = Instantiate(singletank1);
        TankPawn tankPawn = tankObj.GetComponent<TankPawn>();
        PlayerController playerController = tankObj.GetComponent<PlayerController>();

        // Assign the TankPawn to the PlayerController's pawn variable
        playerController.pawn = tankPawn;

        // Register the PlayerController with the GameManager
        RegisterPlayer(playerController);
    }
}
