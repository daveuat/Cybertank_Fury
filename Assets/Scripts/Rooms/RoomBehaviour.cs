using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    public GameObject[] walls; // 0 = top, 1 = right, 2 = bottom, 3 = left
    public GameObject[] doors;

    // Update is called once per frame
    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}