using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfPatrollState : StateMachineBehaviour
{
    Wolf wolf;
    Vector3 point;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (wolf == null)
        {
            wolf = animator.GetComponent<Wolf>();
        }

        point = wolf.GetPoint();

        wolf.agent.speed = 1.5f;
        wolf.agent.angularSpeed = 175f;
        wolf.agent.stoppingDistance = 0;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolf.agent.SetDestination(point);

        if (wolf.agent.remainingDistance == 0)
        {
            point = wolf.GetPoint();
        }

        wolf.Check(wolf.transform.position, 15f);
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
