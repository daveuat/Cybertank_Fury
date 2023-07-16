using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAndBounce : MonoBehaviour
{
    public float startingHeight = 0f;
    public float bounceSpeed = 1f;
    public float timeOffset = 0f;
    public float bounceAmplitude = 1f;
    public float rotationSpeed = 150f;
    public float x = 0f;
    public float y = 0f;
    public float z = 0f;
    public float speed = 1f;
    public bool useDeltaTime = true;

    private float deltaTimeFactor = 1f;

    // Start is called before the first frame update
    void Start()
    {
        deltaTimeFactor = useDeltaTime ? Time.deltaTime : 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // Update position
        float finalHeight = startingHeight + Mathf.Sin(Time.time * bounceSpeed + timeOffset) * bounceAmplitude;
        var position = transform.localPosition;
        position.y = finalHeight;
        position.z = z;
        transform.localPosition = position;

        // Update rotation
        transform.Rotate(0f, rotationSpeed * deltaTimeFactor, 0f, Space.Self);

        // Update bounce
        finalHeight = y + Mathf.Sin(Time.time * speed * deltaTimeFactor) * x;
        position = transform.localPosition;
        position.y = finalHeight;
        transform.localPosition = position;
    }
}