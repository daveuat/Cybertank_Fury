using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    public List<Powerup> activePowerups = new List<Powerup>();

    private void Update()
    {
        for (int i = activePowerups.Count - 1; i >= 0; i--)
        {
            Powerup powerup = activePowerups[i];
            powerup.UpdatePowerup();

            if (powerup.IsExpired())
            {
                powerup.Remove();
                activePowerups.RemoveAt(i);
            }
        }
    }

    public void AddPowerup(Powerup powerup)
    {
        powerup.Apply();
        activePowerups.Add(powerup);
    }


}
