using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager3rd : MonoBehaviour
{
    InputManager3rd inputManager; //To reference the InputManager3rd script 
    PlayerMovement3rd playerMovement; //To reference the PlayerMovement3rd script

    private void Awake()
    {
        inputManager = GetComponent<InputManager3rd>(); //Assign the InputManager3rd script to inputManager
        playerMovement = GetComponent<PlayerMovement3rd>(); //Assign the PlayerMovement3rd script to playerMovement
    }

    private void Update()
    {
        inputManager.HandleAllInputs(); //Call the HandleAllInputs method from the InputManager3rd script
    }
    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement(); //Call the HandleAllMovement method from the PlayerMovement3rd script
    }
}
