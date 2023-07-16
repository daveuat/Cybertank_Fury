<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public virtual void Start() // Changed this line
    {
    }

    public abstract void Move(Vector3 direction, float speed);

    public void Rotate(float turnSpeed)
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
    }
}

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour
{
    public abstract void Start();
    public abstract void Move(Vector3 direction, float speed);
    public void Rotate(float turnSpeed)
    {
        transform.Rotate(Vector3.up * turnSpeed * Time.deltaTime);
    }
}
>>>>>>> main
