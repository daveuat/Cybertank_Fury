<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpeedPowerup : Powerup
{
    private Pawn pawn;
    private float speedBoost;
    private float originalSpeed;

    public TankSpeedPowerup(Pawn pawn, float speedBoost, float duration) : base(duration)
    {
        this.pawn = pawn;
        this.speedBoost = speedBoost;
        powerupName = "TankSpeedPowerup";
    }

    public override void Apply()
    {
        if (pawn != null)
        {
            originalSpeed = pawn.GetSpeed();
            pawn.SetSpeed(originalSpeed + speedBoost);
        }
    }

    public override void Remove()
    {
        if (pawn != null)
        {
            pawn.SetSpeed(originalSpeed);
        }
    }
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpeedPowerup : Powerup
{
    private Pawn pawn;
    private float speedBoost;
    private float originalSpeed;

    public TankSpeedPowerup(Pawn pawn, float speedBoost, float duration) : base(duration)
    {
        this.pawn = pawn;
        this.speedBoost = speedBoost;
        powerupName = "TankSpeedPowerup";
    }

    public override void Apply()
    {
        if (pawn != null)
        {
            originalSpeed = pawn.GetSpeed();
            pawn.SetSpeed(originalSpeed + speedBoost);
        }
    }

    public override void Remove()
    {
        if (pawn != null)
        {
            pawn.SetSpeed(originalSpeed);
        }
    }
>>>>>>> main
}