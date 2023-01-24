using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearPatrollState : StateMachineBehaviour
{
    private Bear m_Bear;
    private Vector3 m_Point;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_Bear == null)
        {
            m_Bear = animator.GetComponent<Bear>();
        }

        m_Point = m_Bear.GetPoint();

        m_Bear.agent.speed = 1.5f;
        m_Bear.agent.angularSpeed = 175f;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Bear.agent.SetDestination(m_Point);

        if (m_Bear.agent.remainingDistance == 0)
        {
            m_Point = m_Bear.GetPoint();
        }

        m_Bear.Check(m_Bear.transform.position, 5);
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
