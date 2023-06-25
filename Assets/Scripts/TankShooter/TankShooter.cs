using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankShooter : Shooter
{
    public Transform firepointTransform;
    public GameObject tankShell1;
    public float shellForce;
    public float damageDone;
    public float shellLife;
    public float targetRange;
    public float rateFire; // Add this line to declare rateFire

    private float nextFire;

    private void Start()
    {
        nextFire = Time.time;
    }

    public override void Shoot(GameObject tankShell1, float shellForce, float damageDone, float lifeSpan)
    {
        if (Time.time >= nextFire)
        {
            // Instantiate the projectile
            GameObject newShell = Instantiate(tankShell1, firepointTransform.position, firepointTransform.rotation);

            // Set projectile properties
            DamageOnHit doh = newShell.GetComponent<DamageOnHit>();
            if (doh != null)
            {
                doh.damageDone = damageDone;
                doh.owner = GetComponent<Pawn>();
            }

            // Apply force to the projectile
            Rigidbody rb = newShell.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(firepointTransform.forward * shellForce);
            }

            // Destroy the projectile after a certain lifespan
            Destroy(newShell, lifeSpan);

            nextFire = Time.time + rateFire;
        }
    }

    public float TargetRange
    {
        get { return targetRange; }
    }
}
