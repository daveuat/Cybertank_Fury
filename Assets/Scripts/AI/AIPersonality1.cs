using UnityEngine;
// Roam Type

public class AIPersonality1 : AIController
{
    private Vector3 roamPosition;

    protected override void Update()
    {
        base.Update();

        switch (currentState)
        {
            case State.Idle:
                if (CanSeeTarget())
                {
                    currentState = State.Chase;
                }
                else if (CanHearTarget())
                {
                    currentState = State.Patrol;
                    roamPosition = GetRoamingPosition();
                }
                break;

            case State.Chase:
                if (!CanSeeTarget())
                {
                    currentState = State.Idle;
                }
                else
                {
                    ChaseTarget();
                }
                break;

            case State.Patrol:
                if (CanSeeTarget())
                {
                    currentState = State.Chase;
                }
                else
                {
                    Roam();
                }
                break;

            default:
                // Handle other states or implement additional logic as needed
                break;
        }
    }

    private void Roam()
    {
        Vector3 direction = (roamPosition - transform.position).normalized;
        Vector3 movement = direction * pawn.moveSpeed * Time.deltaTime;

        pawn.MoveToward(roamPosition, pawn.moveSpeed);
        pawn.transform.rotation = Quaternion.LookRotation(direction);

        if (Vector3.Distance(transform.position, roamPosition) < 1f)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private Vector3 GetRoamingPosition()
    {
        // Implement your own logic to get the roaming position
        // This can be random or predefined waypoints
        return new Vector3(/* Roaming position coordinates */);
    }

    public override void ProcessInputs()
    {
        // Implement the ProcessInputs method from the abstract base class
    }
}
