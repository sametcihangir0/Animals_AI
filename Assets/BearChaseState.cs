using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearChaseState : StateMachineBehaviour
{
    Bear bear;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (bear == null)
        {
            bear = animator.GetComponent<Bear>();
        }

        bear.agent.speed = 2f;
        bear.agent.angularSpeed = 175f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bear.agent.stoppingDistance = 2.25f;
        bear.agent.SetDestination(bear.CurrentTarget.position);

        if (bear.agent.remainingDistance <= bear.agent.stoppingDistance)
        {
            bear.setAnim("isAttack");
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
