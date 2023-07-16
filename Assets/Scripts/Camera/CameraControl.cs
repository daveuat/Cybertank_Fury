using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform playerTank;                    // Reference to the player tank's transform
    public Vector3 offset;                          // Offset from the player tank's position
    public float smoothSpeed = 0.125f;              // Speed of camera movement

    private Vector3 desiredPosition;                // The desired position for the camera


    private void LateUpdate()
    {
        // Calculate the desired position based on the player tank's position and the offset
        desiredPosition = playerTank.position + offset;

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
