using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : StateMachineBehaviour
{
    Wolf wolf;
    float timer;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (wolf == null)
        {
            wolf = animator.GetComponent<Wolf>();
        }
        wolf.agent.isStopped=true;
        if (wolf.idleControl==false)
        {
            wolf.setAnim("isPatroll");
        }

    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (wolf.idleControl==true)
        {
            timer += Time.deltaTime;
            if (timer >3)
            {
                wolf.setAnim("isPatroll");
                timer = 0;
                wolf.idleControl = false;
            }
        }
        /*else
        {
            wolf.setAnim("isPatroll");
        }
        */
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        wolf.agent.isStopped = false;
    }

    
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
