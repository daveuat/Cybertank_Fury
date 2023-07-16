using UnityEngine;

public class TankMover : Mover
{
    private Rigidbody rb;

    public float rotationSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing");
        }
    }


    public override void Start() // Changed this line
    {
        base.Start(); // Call base implementation, if needed
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing");
        }
    }

    public override void Move(Vector3 direction, float speed)
    {
        Vector3 moveVector = direction.normalized * speed * Time.deltaTime;
        rb.MovePosition(rb.position + moveVector);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, toRotation, rotationSpeed));
        }
    }

    public float GetCurrentSpeed()
    {
        return rb.velocity.magnitude;
    }
}

