using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        ChaseTarget,
    }

    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float speedFactor = 1f;
    [SerializeField] private float roamingRadius = 20f;
    [SerializeField] private float targetDetectionRadius = 15f;
    [SerializeField] private bool fleeIfPlayerDetected = true;
    [SerializeField] private bool useRoaming = true;
    [SerializeField] private float chaseRange = 30f;
    [SerializeField] private bool shouldReturnToStart = true;
    [SerializeField] private Vector3 startingPosition;

    private State state;
    private Vector3 roamPosition;

    private Rigidbody rb;
    private TankShooter shooter;
    private bool hasReachedStartingPosition = false;

    public float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    public float SpeedFactor
    {
        get { return speedFactor; }
        set { speedFactor = value; }
    }

    public float RoamingRadius
    {
        get { return roamingRadius; }
        set { roamingRadius = value; }
    }

    public float TargetDetectionRadius
    {
        get { return targetDetectionRadius; }
        set { targetDetectionRadius = value; }
    }

    public bool FleeIfPlayerDetected
    {
        get { return fleeIfPlayerDetected; }
        set { fleeIfPlayerDetected = value; }
    }

    public Transform[] waypoints;

    private int currentWaypointIndex = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.constraints = RigidbodyConstraints.FreezePositionY;

        startingPosition = transform.position;
        state = State.Roaming;

        shooter = GetComponent<TankShooter>();

        if (!useRoaming && waypoints != null && waypoints.Length > 0)
        {
            roamPosition = GetNextWaypointPosition();
        }
        else
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Update()
    {
        switch (state)
        {
            case State.Roaming:
                Roam();

                if (Vector3.Distance(transform.position, roamPosition) < 1f)
                {
                    roamPosition = GetRoamingPosition();
                }

                FindTarget();
                break;

            case State.ChaseTarget:
                GameObject target = FindClosestTarget();

                if (target != null)
                {
                    float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

                    if (distanceToTarget <= chaseRange)
                    {
                        ChaseTarget(target);
                        ShootIfTargetInRange(target);
                    }
                    else if (shouldReturnToStart && !hasReachedStartingPosition)
                    {
                        ReturnToStartingPosition();
                    }
                }
                else if (shouldReturnToStart && !hasReachedStartingPosition)
                {
                    ReturnToStartingPosition();
                }
                break;
        }
    }

    private Vector3 GetRoamingPosition()
    {
        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * roamingRadius;
        return startingPosition + new Vector3(randomCirclePoint.x, 0f, randomCirclePoint.y);
    }

    private void FindTarget()
    {
        GameObject target = FindClosestTarget();

        if (target != null && Vector3.Distance(transform.position, target.transform.position) < targetDetectionRadius)
        {
            if (fleeIfPlayerDetected)
            {
                FleeFromTarget(target);
            }
            else
            {
                state = State.ChaseTarget;
            }
        }
        else
        {
            state = State.Roaming;
        }
    }

    private bool isFleeing = false;
    private Vector3 fleeDirection;

    private void FleeFromTarget(GameObject target)
    {
        if (fleeIfPlayerDetected)
        {
            if (!isFleeing)
            {
                isFleeing = true;
                fleeDirection = (transform.position - target.transform.position).normalized;
            }

            Vector3 movement = fleeDirection * movementSpeed * speedFactor * Time.deltaTime;

            rb.MovePosition(transform.position + new Vector3(movement.x, 0f, movement.z));
            rb.rotation = Quaternion.LookRotation(fleeDirection);
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

    private void Roam()
    {
        if (useRoaming || waypoints == null || waypoints.Length == 0)
        {
            Vector3 direction = (roamPosition - transform.position).normalized;
            Vector3 movement = direction * movementSpeed * speedFactor * Time.deltaTime;

            rb.MovePosition(transform.position + new Vector3(movement.x, 0f, movement.z));
            rb.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            MoveToNextWaypoint();
        }
    }

    private void ChaseTarget(GameObject target)
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Vector3 movement = direction * movementSpeed * speedFactor * Time.deltaTime;

        rb.MovePosition(transform.position + new Vector3(movement.x, 0f, movement.z));
        rb.rotation = Quaternion.LookRotation(direction);
    }

    private void ShootIfTargetInRange(GameObject target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance <= shooter.TargetRange)
        {
            shooter.Shoot(shooter.tankShell1, shooter.shellForce, shooter.damageDone, shooter.shellLife);
        }
    }

    private Vector3 GetNextWaypointPosition()
    {
        if (waypoints == null || waypoints.Length == 0)
            return transform.position;

        Transform currentWaypoint = waypoints[currentWaypointIndex];

        if (currentWaypoint == null)
            return transform.position;

        return currentWaypoint.position;
    }

    private void MoveToNextWaypoint()
    {
        Vector3 direction = (waypoints[currentWaypointIndex].position - transform.position).normalized;
        Vector3 movement = direction * movementSpeed * speedFactor * Time.deltaTime;

        rb.MovePosition(transform.position + new Vector3(movement.x, 0f, movement.z));
        rb.rotation = Quaternion.LookRotation(direction);

        // Check if the enemy tank has reached the current waypoint
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < 1f)
        {
            // Move forward if not at the start or end
            if (currentWaypointIndex > 0 && currentWaypointIndex < waypoints.Length - 1)
            {
                currentWaypointIndex++;
            }
            // Reached the last waypoint, start moving in reverse
            else if (currentWaypointIndex == waypoints.Length - 1)
            {
                currentWaypointIndex = 0;
                ReverseWaypoints();
            }
            // Reached the first waypoint, start moving forward
            else if (currentWaypointIndex == 0)
            {
                currentWaypointIndex++;
            }
        }
    }

    private void ReverseWaypoints()
    {
        System.Array.Reverse(waypoints);
    }

    private void ReturnToStartingPosition()
    {
        if (!shouldReturnToStart)
        {
            state = State.Roaming;
            return;
        }

        Vector3 direction = (startingPosition - transform.position).normalized;
        Vector3 movement = direction * movementSpeed * speedFactor * Time.deltaTime;

        rb.MovePosition(transform.position + new Vector3(movement.x, 0f, movement.z));
        rb.rotation = Quaternion.LookRotation(direction);

        // Check if the enemy tank has reached the starting position
        if (Vector3.Distance(transform.position, startingPosition) < 0.1f)
        {
            hasReachedStartingPosition = true;
            state = State.Roaming;
        }
    }
}
