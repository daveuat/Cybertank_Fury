using UnityEngine;

public class AIType1 : AIController
{
    private void Start()
    {
        currentState = AIState.Patrol;
        pawn = GetComponent<Pawn>();
    }

    private void Update()
    {
        UpdateFSM();
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
                MoveTowardsTarget();
                break;
            case AIState.Attack:
                // Implement attack behavior
                AttackTarget();
                break;
            default:
                Debug.LogError("Invalid AI state.");
                break;
        }
    }

    private void MoveTowardsTarget()
    {
        if (target != null)
        {
            Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
            pawn.MoveForward();
        }
    }

    private void AttackTarget()
    {
        if (target != null)
        {
            // Implement attack logic here
            // For example, call the Shoot method on the Shooter component
            Shooter shooter = GetComponent<Shooter>();
            if (shooter != null)
            {
                shooter.Shoot(pawn.tankShell1, pawn.shellForce, pawn.damageDone, pawn.shellLife);
            }
        }
    }
}
