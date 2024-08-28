using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBool : StateMachineBehaviour
{

    public string isInteractingBooll;
    public bool isInteractingStatus;

    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
{
    animator.SetBool(isInteractingBooll, isInteractingStatus); // Asegúrate de que el parámetro esté definido en el Animator
}

    
}
