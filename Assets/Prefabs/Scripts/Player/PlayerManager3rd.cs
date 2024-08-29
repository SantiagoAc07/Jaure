using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager3rd : MonoBehaviour
{
    Animator animator; //Variable to store the animator componet
    InputManager3rd inputManager; //To reference the InputManager3rd script 
    PlayerMovement3rd playerMovement; //To reference the PlayerMovement3rd script

    public bool isInteracting; //Variable to check if the player is interacting

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>(); //Assign the animator component to the animator variable
        inputManager = GetComponent<InputManager3rd>(); //Assign the InputManager3rd script to inputManager
        playerMovement = GetComponent<PlayerMovement3rd>(); //Assign the PlayerMovement3rd script to playerMovement
    }

    private void LateUpdate()
{
    isInteracting = animator.GetBool("isInteracting"); // Asegúrate de que el parámetro esté definido en el Animator
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
