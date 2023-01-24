using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : MonoBehaviour
{
    // IdleState
    private string[] Anims = new string[]
    {
        "isPatroll" ,
        "isAttack" ,
        "isRoar" ,
        "isChase" ,
        "isDie"
    };

    public NavMeshAgent agent;

    [SerializeField]
    private Animator animator;
    private RaycastHit hit;
    public Vector3 GetPoint()
    {
        Vector3 x = transform.forward * Random.Range(5, 20);
        Vector3 z = transform.right * Random.Range(5, 20);
        Vector3 randomOrigin = (transform.position + (Vector3.up * 5)) + x + z;

        if (Physics.Raycast(randomOrigin, Vector3.down, out hit, LayerMask.GetMask("Ground")))
        {
            return GetNearPoint(hit.point);
        }
        else
        {
            Debug.LogError("Boþluk!");
            return Vector3.zero;
        }
    }


    private Vector3 GetNearPoint(Vector3 point)
    {
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(point, out myNavHit, 1000, -1))
        {
            return myNavHit.position;
        }
        else
        {
            Debug.LogError("Point ayarlanmadý!");
            return Vector3.zero;
        }
    }


    public void setAnim(string anim)
    {
        for (int i = 0; i < Anims.Length; i++)
        {
            if (Anims[i] == anim)
            {
                animator.SetBool(Anims[i], true);
            }
            else
            {
                animator.SetBool(Anims[i], false);
            }
        }
    }

    void Start()
    {
        animator.SetInteger("isIdleIndex", 1);
        setAnim("isPatroll");
    }
}
