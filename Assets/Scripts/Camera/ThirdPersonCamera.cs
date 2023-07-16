<<<<<<< HEAD
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;             // The target object to follow (the tank)
    public float distance = .9f;         // The distance between the camera and the target
    public float height = .8f;            // The height of the camera above the target
    public float rotationDamping = 10f;  // The damping applied to camera rotation
    public float tiltAngle = 15f;        // The angle at which the camera tilts

    private Vector3 offset;              // The offset from the target's position

    private void Start()
    {
        // Calculate and store the initial offset from the target's position
        offset = new Vector3(0f, height, -distance);
    }

    private void LateUpdate()
    {
        // Check if the target is null
        if (target == null)
        {
            return;
        }

        // Calculate the desired camera position based on the target's position and rotation
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Set the camera's position
        transform.position = desiredPosition;

        // Calculate the desired camera rotation based on the target's rotation and tilt angle
        Quaternion desiredRotation = Quaternion.LookRotation(target.forward) * Quaternion.Euler(tiltAngle, 0f, 0f);

        // Smoothly rotate the camera towards the desired rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationDamping * Time.deltaTime);
    }

    public static ThirdPersonCamera instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        RoomGenerator.OnPlayerTankSpawned += AssignTarget;  // Subscribe to the event
    }

    private void OnDisable()
    {
        RoomGenerator.OnPlayerTankSpawned -= AssignTarget;  // Unsubscribe from the event
    }

    private void AssignTarget(Transform target)
    {
        this.target = target;
    }



}
=======
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;             // The target object to follow (the tank)
    public float distance = .9f;         // The distance between the camera and the target
    public float height = .8f;            // The height of the camera above the target
    public float rotationDamping = 10f;  // The damping applied to camera rotation
    public float tiltAngle = 15f;        // The angle at which the camera tilts

    private Vector3 offset;              // The offset from the target's position

    private void Start()
    {
        // Calculate and store the initial offset from the target's position
        offset = new Vector3(0f, height, -distance);
    }

    private void LateUpdate()
    {
        // Calculate the desired camera position based on the target's position and rotation
        Vector3 desiredPosition = target.position + target.rotation * offset;

        // Set the camera's position
        transform.position = desiredPosition;

        // Calculate the desired camera rotation based on the target's rotation and tilt angle
        Quaternion desiredRotation = Quaternion.LookRotation(target.forward) * Quaternion.Euler(tiltAngle, 0f, 0f);

        // Smoothly rotate the camera towards the desired rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationDamping * Time.deltaTime);
    }
}
>>>>>>> main
