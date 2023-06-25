using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Powerup
{
    public string powerupName;
    public float duration;

    protected float timer;

    public Powerup(float duration)
    {
        this.duration = duration;
        timer = 0;
    }

    public abstract void Apply();
    public abstract void Remove();

    public void UpdatePowerup()
    {
        timer += Time.deltaTime;
    }

    public bool IsExpired()
    {
        return timer >= duration;
    }
}