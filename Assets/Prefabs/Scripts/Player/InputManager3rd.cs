using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager3rd : MonoBehaviour
{
    PlayerControls3rd playerControls;
    AnimatorManager3rd animatorManager;
    PlayerMovement3rd playerMovement;//reference to PlayerMovement3rd speed

    public Vector2 movementInput;
    public float moveAmount; //change from private to public
    public float verticalInput; 
    public float horizontalInput;

    public bool shiftInput; //to receive the input after pressing SHIFT

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager3rd>();
        playerMovement = GetComponent<PlayerMovement3rd>(); //get the script component
    }

    private void OnEnable() 
    {
        if (playerControls == null) 
        {
            playerControls  = new PlayerControls3rd(); 
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

            playerControls.PlayerActions.Shift.performed += i => shiftInput = true;//when pressing the SHIFT key set shiftInput true
            playerControls.PlayerActions.Shift.canceled += i => shiftInput = false;//when releasing the SHIFT key set shiftInput false
        }
        playerControls.Enable(); 
    }

    private void OnDisable()
    {
        playerControls.Disable(); 
    }

    private void HandleMovementInput() 
    {
        verticalInput = movementInput.y; 
        horizontalInput = movementInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount, playerMovement.isRunning); //add "playerMovement.isRunning"(doesn’t exist yet) 
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleRunningInput();//call
    }

    private void HandleRunningInput()//create
    {
        if (shiftInput && moveAmount > 0.5f)//
        {
            playerMovement.isRunning = true;//
        }
        else
        {
            playerMovement.isRunning = false;//
        }
    }
}