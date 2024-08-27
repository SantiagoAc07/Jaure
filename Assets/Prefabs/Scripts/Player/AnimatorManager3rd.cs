using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager3rd : MonoBehaviour
{
    Animator animator; //Variable to store the animator componet
    int horizontal; //Variable to store the id version of the parameters on the animator
    int vertical;

    private void Awake() //Get the animator componet
    {
        animator = GetComponent<Animator>(); //Get the animator componet
        horizontal = Animator.StringToHash("Horizontal"); //Get the id version of the parameters on the animator
        vertical = Animator.StringToHash("Vertical"); //Get the id version of the parameters on the animator
    }

    //function to update the animator parameters

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        animator.SetFloat(horizontal, horizontalMovement, 0.1f, Time.deltaTime); //Update the horizontal parameter on the animator
        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime); //Update the vertical parameter on the animator
    }
}
