<<<<<<< HEAD
using UnityEngine;
// Chase Type

public class AIPersonality2 : AIController
{
    protected override void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                if (CanSeeTarget() || CanHearTarget())
                {
                    currentState = State.Chase;
                }
                break;

            case State.Chase:
                if (!CanSeeTarget() && !CanHearTarget())
                {
                    currentState = State.Idle;
                }
                else
                {
                    ChaseTarget();
                }
                break;

            default:
                // Handle other states or implement additional logic as needed
                break;
        }
    }

    private void ChaseTarget()
    {
        GameObject target = FindClosestTarget();

        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Vector3 movement = direction * pawn.moveSpeed * Time.deltaTime;

            pawn.MoveToward(target.transform.position, pawn.moveSpeed);
            pawn.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private GameObject FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    public override void ProcessInputs()
    {
        // Implement the ProcessInputs method from the abstract base class
    }
}
=======
using UnityEngine;
// Chase Type

public class AIPersonality2 : AIController
{
    protected override void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                if (CanSeeTarget() || CanHearTarget())
                {
                    currentState = State.Chase;
                }
                break;

            case State.Chase:
                if (!CanSeeTarget() && !CanHearTarget())
                {
                    currentState = State.Idle;
                }
                else
                {
                    ChaseTarget();
                }
                break;

            default:
                // Handle other states or implement additional logic as needed
                break;
        }
    }

    private void ChaseTarget()
    {
        GameObject target = FindClosestTarget();

        if (target != null)
        {
            Vector3 direction = (target.transform.position - transform.position).normalized;
            Vector3 movement = direction * pawn.moveSpeed * Time.deltaTime;

            pawn.MoveToward(target.transform.position, pawn.moveSpeed);
            pawn.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private GameObject FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestTarget = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = target;
            }
        }

        return closestTarget;
    }

    public override void ProcessInputs()
    {
        // Implement the ProcessInputs method from the abstract base class
    }
}
>>>>>>> main
