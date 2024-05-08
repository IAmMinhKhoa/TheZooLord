using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomacticDanceBehaviour : StateMachineBehaviour
{
    readonly float danceMinTime = 1;
    readonly float danceMaxTime = 5;

    float danceTimer = 0;

    string[] danceTriggers = { "Dance 1", "Dance 2", "Dance 3", "Dance 4", "Dance 5"};
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(danceTimer <= 0) {
            danceRandomIdle(animator);
            danceTimer = Random.Range(danceMinTime, danceMaxTime);
        }
        else
        {
            danceTimer -= Time.deltaTime;
        }
    }

    void danceRandomIdle(Animator animator)
    {
        System.Random rand = new System.Random();
        int dancePos = rand.Next(danceTriggers.Length);
        string danceTrigger = danceTriggers[dancePos];
        animator.SetTrigger(danceTrigger);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
