using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollState : StateMachineBehaviour
{

    Wolf wolf;
    float timer;
    bool reach;
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

    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolf.agent.SetDestination(point);

        if (wolf.agent.remainingDistance == 0)
        {
            
            if (Random.Range(0, 1) == 0)
            {
                wolf.idleControl = true;
                wolf.setAnim("isIdle");
               
            }
            else
            {
                point = wolf.GetPoint();
                reach = true;
            }
            
            
        }


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
