using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : MonoBehaviour
{
    // IdleState
    private string[] Anims = new string[]
    {
        "isIdle" ,
        "isPatroll" ,
        "isAttack" ,
        "isRoar" ,
        "isChase" ,
        "isDie"
    };

    public List<Transform> Points= new List<Transform>();

    public NavMeshAgent agent;
    RaycastHit hit;

    public bool idleControl;

    public Vector3 GetPoint()
    {
        Vector3 x=transform.forward*Random.Range(5,20);
        Vector3 z = transform.right * Random.Range(5, 20);
        Vector3 randomOrigin = (transform.position + (Vector3.up * 5))+ x+z;

        if (Physics.Raycast(randomOrigin,Vector3.down,out hit,LayerMask.GetMask("Ground")))
        {
            
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }

        
    }

    [SerializeField]
    private Animator animator;

    public void setAnim(string anim)
    {
        for (int i = 0; i < Anims.Length; i++)
        {
            if (Anims[i]==anim)
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
        setAnim("isIdle");
        idleControl = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
