using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIType4 : AIController
{
    // Additional adjustable parameters for attack behavior
    public float attackRange;
    public float attackCooldown;

    private float nextAttackTime;

    protected override void Start()
    {
        base.Start();

        // Set the initial cooldown for the first attack
        nextAttackTime = Time.time;
    }

    protected override void UpdateFSM()
    {
        switch (currentState)
        {
            case AIState.Patrol:
                // Implement patrol behavior
                break;
            case AIState.Chase:
                // Implement chase behavior
                break;
            case AIState.Flee:
                // Implement flee behavior
                break;
            case AIState.Attack:
                // Implement attack behavior
                Attack();
                break;
            default:
                // Implement default behavior or handle unrecognized state
                break;
        }
    }

    private void Attack()
    {
        // Check if the target is within attack range
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= attackRange)
        {
            // Check if the attack cooldown has passed
            if (Time.time >= nextAttackTime)
            {
                // Perform the attack action
                Debug.Log("Attacking!");

                // Set the next attack time based on the cooldown
                nextAttackTime = Time.time + attackCooldown;
            }
        }
    }
}
