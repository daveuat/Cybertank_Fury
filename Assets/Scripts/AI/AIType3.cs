using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIType3 : AIController
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
                if (patrolPoints.Length > 0)
                {
                    pawn.MoveToward(patrolPoints[0].position, pawn.GetSpeed());
                }
                break;
            case AIState.Chase:
                // Implement chase behavior
                if (target != null)
                {
                    pawn.MoveToward(target.transform.position, pawn.GetSpeed());
                }
                break;
            case AIState.Flee:
                // Implement flee behavior
                if (target != null)
                {
                    Vector3 directionAwayFromTarget = (transform.position - target.transform.position).normalized;
                    pawn.MoveToward(transform.position + directionAwayFromTarget, pawn.GetSpeed());
                }
                break;
            default:
                Debug.LogError("Invalid AI state.");
                break;
        }
    }
}