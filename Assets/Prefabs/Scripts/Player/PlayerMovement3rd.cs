using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3rd : MonoBehaviour
{
    PlayerManager3rd playerManager; //Variable to store the PlayerManager3rd script

    AnimatorManager3rd animatorManager; //Variable to store the AnimatorManager3rd script


    
    InputManager3rd inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;


    public float inAirTimer; //Variable to store the inAirTimer
    public float leapingVelocity; //Variable to store the leapingVelocity
    public float fallingVelocity; //Variable to store the fallingVelocity
    public float rayCastHeightOffSet = 0.5f; //Variable to store the rayCastHeightOffSet
    public LayerMask groundLayer; //Variable to store the groundLayer
    public float maxDistance = 1;


    public float walkingSpeed = 2.5f;
    public float runningSpeed = 7;
    public float rotationSpeed = 15;

    public bool isRunning;
    public bool isGrounded;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager3rd>(); //Get the PlayerManager3rd script
        animatorManager = GetComponent<AnimatorManager3rd>(); //Get the AnimatorManager3rd script
        isGrounded = true;
        inputManager = GetComponent<InputManager3rd>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    void Update()
    {
        HandleAllMovement();

        GroundCheck();
    }

    bool GroundCheck()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        if (Physics.SphereCast(rayCastOrigin, 0.2f, Vector3.down, out hit, maxDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.y = 0;
        moveDirection.Normalize();
        if (isRunning)
        {
            moveDirection = moveDirection * runningSpeed; //Set the player to run
        }
        else
        {
            moveDirection = moveDirection * walkingSpeed; //Set the player to walk
        }
        //moveDirection *= movementSpeed;
        Vector3 movementVelocity = moveDirection;
        playerRigidbody.velocity = movementVelocity;
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = cameraObject.forward * inputManager.verticalInput + cameraObject.right * inputManager.horizontalInput;
        targetDirection.y = 0;
        targetDirection.Normalize();

        
        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = playerRotation;
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting) //If the player is interacting, return
            return; //Return
        Debug.Log("HandleAllMovement called");
        HandleMovement();
        HandleRotation();
    }

    private void HandleFallingAndLanding()
{
    RaycastHit hit;
    Vector3 rayCastOrigin = transform.position;
    rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

    if (!isGrounded)
    {
        if (!playerManager.isInteracting)
        {
            animatorManager.PlayerTargetAnimation("IsGrounded", false);
        }

        inAirTimer += Time.deltaTime;
        playerRigidbody.AddForce(transform.forward * leapingVelocity);
        playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer);
    }
    else
    {
        animatorManager.PlayerTargetAnimation("IsGrounded", true);
    }

    if (Physics.SphereCast(rayCastOrigin, 0.1f, Vector3.down, out hit, maxDistance, groundLayer))
    {
        if (!isGrounded && playerManager.isInteracting)
        {
            animatorManager.PlayerTargetAnimation("IsGrounded", true);
        }

        inAirTimer = 0;
        isGrounded = true;
        playerManager.isInteracting = false;
    }
    else
    {
        isGrounded = false;
    }
}

   

}
