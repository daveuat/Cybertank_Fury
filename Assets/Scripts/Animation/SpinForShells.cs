<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinForShells : MonoBehaviour
{
    public float rotationSpeed = 150f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinForShells : MonoBehaviour
{
    public float rotationSpeed = 150f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime, Space.Self);
    }
>>>>>>> main
}