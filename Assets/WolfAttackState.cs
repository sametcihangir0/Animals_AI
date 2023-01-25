using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAttackState : StateMachineBehaviour
{
    Wolf wolf;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (wolf == null)
        {
            wolf = animator.GetComponent<Wolf>();
        }

        wolf.agent.velocity = Vector3.zero;
        wolf.agent.isStopped = true;
        wolf.SetRotate();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (wolf.CurrentTarget.GetComponent<Live>().IsDie)
        {
            wolf.setAnim("isRoar");
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
