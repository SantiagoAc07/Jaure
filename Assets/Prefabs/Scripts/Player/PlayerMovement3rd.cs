using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement3rd : MonoBehaviour
{
    InputManager3rd inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;
    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    private void Awake()
    {
        inputManager = GetComponent<InputManager3rd>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    void Update()
    {
        HandleAllMovement();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.y = 0;
        moveDirection.Normalize();
        moveDirection *= movementSpeed;
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
        Debug.Log("HandleAllMovement called");
        HandleMovement();
        HandleRotation();
    }

   

}
