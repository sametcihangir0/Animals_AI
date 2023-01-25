using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfChaseState : StateMachineBehaviour
{
    Wolf wolf;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (wolf == null)
        {
            wolf = animator.GetComponent<Wolf>();
        }

        wolf.agent.speed = 3f;
        wolf.agent.angularSpeed = 200f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (wolf.IsDie) return;

        wolf.agent.stoppingDistance = 2.25f;
        wolf.agent.SetDestination(wolf.CurrentTarget.position);

        if (wolf.agent.remainingDistance <= wolf.agent.stoppingDistance)
        {
            wolf.setAnim("isAttack");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that processes and affects root motion
    }

    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Implement code that sets up animation IK (inverse kinematics)
    }
}
