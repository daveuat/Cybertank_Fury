using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootSpeedPickup : MonoBehaviour
{
    private TankShooter tankShooter;
    private float originalRateOfFire;
    [SerializeField] private float rateOfFireMultiplier = 2f;
    [SerializeField] private float duration = 10f;

    private void OnTriggerEnter(Collider other)
    {
        TankShooter shooter = other.GetComponent<TankShooter>();
        if (shooter != null)
        {
            tankShooter = shooter;
            Apply();
            StartCoroutine(RemoveAfterDuration());
            Destroy(gameObject);
        }
    }

    private void Apply()
    {
        originalRateOfFire = tankShooter.rateFire;
        tankShooter.rateFire /= rateOfFireMultiplier; // Subtract the rateOfFireMultiplier from rateFire
    }




    private IEnumerator RemoveAfterDuration()
    {
        yield return new WaitForSeconds(duration);

        // Revert the rate of fire by setting it back to the original value
        tankShooter.rateFire = originalRateOfFire;
    }
}
