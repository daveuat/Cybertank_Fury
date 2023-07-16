using UnityEngine;

public class TankSoundController : MonoBehaviour
{
    public AudioSource startSound;
    public AudioSource stopSound;
    public AudioSource turbineSound;
    public AudioSource maxSpeedSound;
    public AudioSource movementSound;
    public AudioSource idleSound;
    public AudioSource accelerationSound;

    public float speedMax = 5f; // Maximum speed of the tank

    private TankMover tankMover; // Reference to the TankMover script
    private bool isAccelerating; // Flag to track if the tank is accelerating

    private void Awake()
    {
        tankMover = GetComponent<TankMover>();
    }

    public void StartMoving()
    {
        startSound.Play();
        Debug.Log("Start sound played");
    }

    public void StopMoving()
    {
        stopSound.Play();
        Debug.Log("Stop sound played");
    }

    public void SetTurbinePitch(float pitch)
    {
        turbineSound.pitch = pitch;
        Debug.Log("Turbine pitch set: " + pitch);
    }

    public void SetMaxSpeedPitch(float pitch)
    {
        maxSpeedSound.pitch = pitch;
        Debug.Log("Max speed pitch set: " + pitch);
    }

    public void SetMovementVolume(float volume)
    {
        movementSound.volume = volume;
        Debug.Log("Movement volume set: " + volume);
    }

    public void SetIdleVolume(float volume)
    {
        idleSound.volume = volume;
        Debug.Log("Idle volume set: " + volume);
    }

    public void SetAccelerationVolume(float volume)
    {
        accelerationSound.volume = volume;
        Debug.Log("Acceleration volume set: " + volume);
    }

    public void SetAcceleration(bool isAccelerating)
    {
        this.isAccelerating = isAccelerating;
        Debug.Log("Acceleration flag set: " + isAccelerating);
    }

    private void Update()
    {
        // Get the current speed of the tank using TankMover
        float speedCurr = tankMover.GetCurrentSpeed();

        // Normalize the speed between 0 and 1
        float speed = speedCurr / speedMax;

        // Calculate gain and pitch parameters for the idle sound
        float idleVolume = -0.2f * speed + 0.5f;
        float idlePitch = 0.65f * speed + 0.85f;

        // Set volume and pitch for the idle sound
        SetIdleVolume(idleVolume);
        idleSound.pitch = idlePitch;

        // Calculate gain and pitch parameters for the acceleration sound
        float accelerationVolume = isAccelerating ? 2.5f : 1f;

        // Set volume for the acceleration sound
        SetAccelerationVolume(accelerationVolume);

        Debug.Log("Speed: " + speed + ", Idle volume: " + idleVolume + ", Idle pitch: " + idlePitch);
        Debug.Log("Acceleration volume: " + accelerationVolume);
    }
}
