<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    // Instantiating my shell prefab
    public GameObject tankShellPrefab;

    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode strafeLeftKey;
    public KeyCode strafeRightKey;
    public KeyCode shootKey;
    public KeyCode pauseKey;

    // Reference to the TankPawn instance
    public TankPawn tankPawn;

    // Start is called before the first frame update
    public override void Start()
    {
        // Run the Start() function from the parent (base) class
        base.Start();
        // Check if the TankPawn reference is not null
        if (tankPawn == null)
        {
            Debug.LogError("TankPawn reference is missing in the PlayerController!");
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        // Run the Update() function from the parent (base) class
        base.Update();
        // Process our Keyboard Inputs
        ProcessInputs();
    }

    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            tankPawn.MoveForward();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            tankPawn.MoveBackward();
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            tankPawn.RotateClockwise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            tankPawn.RotateCounterClockwise();
        }

        if (Input.GetKey(strafeLeftKey))
        {
            tankPawn.StrafeLeft();
        }

        if (Input.GetKey(strafeRightKey))
        {
            tankPawn.StrafeRight();
        }

        if (Input.GetKeyDown(shootKey))
        {
            tankPawn.Shoot();
        }

        if (Input.GetKeyDown(pauseKey))
        {
            // Find the PauseMenu script in the scene
            PauseMenu pauseMenu = FindObjectOfType<PauseMenu>();

            // If the PauseMenu script is found, toggle the pause state
            if (pauseMenu != null)
            {
                pauseMenu.TogglePause();
            }
        }
    }
}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    // Instantiating my shell prefab
    public GameObject tankShellPrefab;

    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode strafeLeftKey;
    public KeyCode strafeRightKey;
    public KeyCode shootKey;

    // Reference to the TankPawn instance
    public TankPawn tankPawn;

    // Start is called before the first frame update
    public override void Start()
    {
        // Run the Start() function from the parent (base) class
        base.Start();
        // Check if the TankPawn reference is not null
        if (tankPawn == null)
        {
            Debug.LogError("TankPawn reference is missing in the PlayerController!");
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        // Run the Update() function from the parent (base) class
        base.Update();
        // Process our Keyboard Inputs
        ProcessInputs();
    }

    public override void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            tankPawn.MoveForward();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            tankPawn.MoveBackward();
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            tankPawn.RotateClockwise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            tankPawn.RotateCounterClockwise();
        }

        if (Input.GetKey(strafeLeftKey))
        {
            tankPawn.StrafeLeft();
        }

        if (Input.GetKey(strafeRightKey))
        {
            tankPawn.StrafeRight();
        }

        if (Input.GetKeyDown(shootKey))
        {
            tankPawn.Shoot();
        }
    }
}
>>>>>>> main
