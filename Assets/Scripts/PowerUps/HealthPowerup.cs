using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPowerup : Powerup
{
    private Pawn pawn;
    private float healthAmount;

    public HealthPowerup(Pawn pawn, float healthAmount, float duration) : base(duration)
    {
        this.pawn = pawn;
        this.healthAmount = healthAmount;
        powerupName = "HealthPowerup";
    }

    public override void Apply()
    {
        Health pawnHealth = pawn.GetHealth();
        float currentHealth = pawnHealth.GetCurrentHealthValue();
        float maxHealth = pawnHealth.GetMaxHealthValue();
        float restoredHealth = Mathf.Min(maxHealth - currentHealth, healthAmount);
        pawnHealth.RestoreHealth(restoredHealth);
    }

    public override void Remove()
    {
        // No action needed for this powerup when it expires
    }
}
