using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager3rd : MonoBehaviour
{
    Animator animator;//to store the animator component
    InputManager3rd inputManager3rd; 
    PlayerMovement3rd playerMovement3rd;

    public bool isInteracting; //bool to get the status from the animator

    private void Awake()
    {
        animator = GetComponent<Animator>();//get the component
        inputManager3rd = GetComponent<InputManager3rd>(); 
        playerMovement3rd = GetComponent<PlayerMovement3rd>(); 
    }

    private void Update()
    {
        inputManager3rd.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerMovement3rd.HandleAllMovement();
    }

    private void LateUpdate()//code here is executed after the update()
    {
        //check every frame in the animator the status of "isInteracting" and update isInteracting bool here
        isInteracting = animator.GetBool("isInteracting");
    }
}