using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorUpgrade : MonoBehaviour
{
    public float armorAmount = 50f;
    public int maxArmorUpgrades = 2;

    private void OnTriggerEnter(Collider other)
    {
        Pawn pawn = other.GetComponent<Pawn>();
        if (pawn != null)
        {
            Health health = pawn.GetComponent<Health>();
            if (health != null && health.GetArmorUpgradeCount() < maxArmorUpgrades)
            {
                health.ApplyArmorUpgrade(armorAmount);
                Destroy(gameObject);
            }
        }
        else
        {
            EnemySpawn enemySpawn = other.GetComponent<EnemySpawn>();
            if (enemySpawn != null)
            {
                Health health = enemySpawn.GetComponent<Health>();
                if (health != null && health.GetArmorUpgradeCount() < maxArmorUpgrades)
                {
                    health.ApplyArmorUpgrade(armorAmount);
                    Destroy(gameObject);
                }
            }
        }
    }
}
