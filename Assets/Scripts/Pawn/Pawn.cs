<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public Mover mover;
    public float shellForce;
    public float damageDone;
    public float shellLife;
    public float rateFire;

    public Shooter shooter;

    public GameObject tankShell1;

    private Health health;

    public Health GetHealth()
    {
        return health;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
        health = GetComponent<Health>();
    }

    public virtual void Update()
    {
        float deltaTime = Time.deltaTime;
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (mover != null)
        {
            mover.Move(moveVector, moveSpeed * deltaTime);
            mover.Rotate(turnSpeed * deltaTime);
        }
        else
        {
            Debug.LogError("Mover component is missing!");
        }
    }


    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void StrafeLeft();
    public abstract void StrafeRight();

    public void RestoreHealth(float amount)
    {
        health.RestoreHealth(amount);
    }

    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(tankShell1, shooter.transform.position, shooter.transform.rotation);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
        bullet.GetComponent<Rigidbody>().AddForce(shooter.transform.forward * shellForce);
        Destroy(bullet, shellLife);
    }

    public bool IsAtLocation(Vector3 targetPosition, float threshold = 0.1f)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        return distance <= threshold;
    }

    public void MoveToward(Vector3 targetPosition, float speed)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        mover.Move(direction, speed);
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public float moveSpeed;
    public float turnSpeed;
    public Mover mover;
    public float shellForce;
    public float damageDone;
    public float shellLife;
    public float rateFire;

    public Shooter shooter;

    public GameObject tankShell1;

    private Health health;

    public Health GetHealth()
    {
        return health;
    }

    public float GetSpeed()
    {
        return moveSpeed;
    }

    public void SetSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        shooter = GetComponent<Shooter>();
        health = GetComponent<Health>();
    }

    public virtual void Update()
    {
        float deltaTime = Time.deltaTime;
        Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        mover.Move(moveVector, moveSpeed * deltaTime);
        mover.Rotate(turnSpeed * deltaTime);
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void StrafeLeft();
    public abstract void StrafeRight();

    public void RestoreHealth(float amount)
    {
        health.RestoreHealth(amount);
    }

    public virtual void Shoot()
    {
        GameObject bullet = Instantiate(tankShell1, shooter.transform.position, shooter.transform.rotation);
        Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
        bullet.GetComponent<Rigidbody>().AddForce(shooter.transform.forward * shellForce);
        Destroy(bullet, shellLife);
    }

    public bool IsAtLocation(Vector3 targetPosition, float threshold = 0.1f)
    {
        float distance = Vector3.Distance(transform.position, targetPosition);
        return distance <= threshold;
    }

    public void MoveToward(Vector3 targetPosition, float speed)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        mover.Move(direction, speed);
    }
}
>>>>>>> main
