using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAndAngle3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(150 * Time.deltaTime, 150 * Time.deltaTime, 150 * Time.deltaTime, Space.Self);
    }
}
