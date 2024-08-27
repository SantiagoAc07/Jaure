using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager3rd : MonoBehaviour
{
    PlayerControls3rd playerControls;
    public Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;

    private void OnEnable()
    {
        if (playerControls == null) //Check if the player controls are null
        {
            playerControls = new PlayerControls3rd();  //Create a new player controls
            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();  //Get the movement input
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
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();  //Handle the movement input
    }
}
