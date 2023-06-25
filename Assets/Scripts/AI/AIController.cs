using UnityEngine;

public abstract class AIController : Controller
{
    public float hearingDistance;
    public float fieldOfView;
    public float aimingFOV;
    public Transform[] patrolPoints;

    protected Pawn target;
    protected State currentState;

    protected virtual void Update()
    {
        // Implement the FSM behavior in derived classes
    }

    protected bool CanSeeTarget()
    {
        if (target == null)
            return false;

        Vector3 direction = target.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle <= fieldOfView * 0.5f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }
        }

        return false;
    }

    protected bool CanHearTarget()
    {
        if (target == null)
            return false;

        float distance = Vector3.Distance(transform.position, target.transform.position);
        return distance <= hearingDistance;
    }

    protected void ChaseTarget()
    {
        if (target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            direction.y = 0f;

            pawn.MoveToward(target.transform.position, pawn.moveSpeed);
            pawn.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    protected void FleeFromTarget()
    {
        if (target != null)
        {
            Vector3 direction = transform.position - target.transform.position;
            direction.y = 0f;

            pawn.MoveToward(transform.position + direction, pawn.moveSpeed);
            pawn.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    protected void Patrol()
    {
        // Implement patrol logic using patrolPoints
    }

    protected enum State
    {
        Idle,
        Chase,
        Flee,
        Patrol
    }
}
