<<<<<<< HEAD
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
    public float rateFire;
    public AudioClip[] fireSounds;

    private float nextFire;
    private AudioSource audioSource;

    private void Start()
    {
        nextFire = Time.time;
        audioSource = GetComponent<AudioSource>();
    }

    public override void Shoot(GameObject projectilePrefab, float shellForce, float damageDone, float lifeSpan)
    {
        if (Time.time >= nextFire)
        {
            // Play the fire sound
            PlayFireSound();

            // Instantiate the projectile
            GameObject newShell = Instantiate(projectilePrefab, firepointTransform.position, firepointTransform.rotation);

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

    private void PlayFireSound()
    {
        if (fireSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, fireSounds.Length);
            audioSource.priority = 150; // Set a higher priority for the audio source
            audioSource.PlayOneShot(fireSounds[randomIndex]);
        }
    }

    public void ResetFireRate()
    {
        nextFire = Time.time;
    }

    public float TargetRange
    {
        get { return targetRange; }
    }
}
=======
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
    public float rateFire;

    private float nextFire;

    private void Start()
    {
        nextFire = Time.time;
    }

    public override void Shoot(GameObject projectilePrefab, float shellForce, float damageDone, float lifeSpan)
    {
        if (Time.time >= nextFire)
        {
            // Instantiate the projectile
            GameObject newShell = Instantiate(projectilePrefab, firepointTransform.position, firepointTransform.rotation);

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
>>>>>>> main
