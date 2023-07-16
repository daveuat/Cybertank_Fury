<<<<<<< HEAD
using UnityEngine;

public class TankPawn : Pawn
{
    // float for fire cooldown
    private float nextFire;

    public Transform firePointTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.Move(-transform.forward, moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    public override void StrafeLeft()
    {
        mover.Move(-transform.right, moveSpeed);
    }

    public override void StrafeRight()
    {
        mover.Move(transform.right, moveSpeed);
    }

    public override void Shoot()
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + rateFire;

            // Call the base implementation of Shoot to include the existing functionality
            base.Shoot();

            // Add additional logic specific to the TankPawn
            GameObject tankShell = Instantiate(tankShell1, transform.position + (transform.forward * 2), transform.rotation);
            shooter.Shoot(tankShell1, shellForce, damageDone, shellLife);
        }
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    // float for fire cooldown
    private float nextFire;

    public Transform firePointTransform;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.Move(-transform.forward, moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    public override void StrafeLeft()
    {
        mover.Move(-transform.right, moveSpeed);
    }

    public override void StrafeRight()
    {
        mover.Move(transform.right, moveSpeed);
    }

    public override void Shoot()
    {
        if (Time.time >= nextFire)
        {
            nextFire = Time.time + rateFire;
            GameObject tankShell = Instantiate(tankShell1, transform.position + (transform.forward * 2), transform.rotation);
            shooter.Shoot(tankShell1, shellForce, damageDone, shellLife);

            // Call the base implementation of Shoot to include the existing functionality
            base.Shoot();
        }
    }
}
>>>>>>> main
