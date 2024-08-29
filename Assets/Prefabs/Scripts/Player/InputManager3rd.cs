using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager3rd : MonoBehaviour
{
    PlayerControls3rd playerControls;  //Variable to store the player controls
    AnimatorManager3rd animatorManager;  //Variable to store the animator manager componet
    PlayerMovement3rd playerMovement;  //Variable to store the player movement componet
    public Vector2 movementInput;  //Variable to store the movement input
    private float moveAmount;  //Variable to store the move amount
    public float verticalInput;  //Variable to store the vertical input
    public float horizontalInput;  //Variable to store the horizontal input
    public bool shiftInput;  //Variable to store the shift input

    private void Awake()  //Get the animator manager componet
    {
        animatorManager = GetComponent<AnimatorManager3rd>();  //Get the animator manager componet
        playerMovement = GetComponent<PlayerMovement3rd>();  //Get the player movement componet
        playerControls = new PlayerControls3rd();  //Create a new player controls
    }

    void Update()
{
    HandleAllInputs();  //Handle all the inputs
}

    private void OnEnable()
    {
        if (playerControls == null) //Check if the player controls are null
        {
            playerControls = new PlayerControls3rd();  //Create a new player controls
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();  //Get the movement input

            playerControls.PlayerActions.Shift.performed += i => shiftInput = true;  //Get the shift input
            playerControls.PlayerActions.Shift.canceled += i => shiftInput = false;  //Get the shift input
        }
        playerControls.Enable();  //Enable the player controls
    }

    private void OnDisable()
    {
        playerControls.Disable();  //Disable the player controls
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;  //Get the vertical input
        horizontalInput = movementInput.x;   //Get the horizontal input

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));  //Clamp the move amount
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerMovement.isRunning);  //Update the animator values
    }

    public void HandleAllInputs()  //Handle all the inputs
    {
        verticalInput = playerControls.PlayerMovement.Movement.ReadValue<Vector2>().y;
        horizontalInput = playerControls.PlayerMovement.Movement.ReadValue<Vector2>().x;
        HandleMovementInput();  //Handle the movement input
        HandleRunningInput();  //Handle the running input
    }

    private void HandleRunningInput()
    {
        if (shiftInput && moveAmount > 0.5f)
        {
            playerMovement.isRunning = true;  //Set the player to run
        }
        else
        {
            playerMovement.isRunning = false;  //Set the player to walk
        }
    }
}
