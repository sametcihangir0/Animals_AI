using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bear : MonoBehaviour
{
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

    public Transform Target;
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
            Debug.LogError("Bo�luk!");
            return Vector3.zero;
        }
    }
    // E�er ���n navmesh �zerine denk gelmez ise ���n�n d�nd�rd��� noktaya bakarak navmesh �zerinde en yak�n  noktay� geri d�nd�r�r
    private Vector3 GetNearPoint(Vector3 point)
    {
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(point, out myNavHit, 1000, -1))
        {
            return myNavHit.position;
        }
        else
        {
            Debug.LogError("Point ayarlanmad�!");
            return Vector3.zero;
        }


    }
    // Belirledi�imiz �ap�n i�erisindeki colliderlar� sana d�nd�r�r
    public void Check(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, LayerMask.GetMask("Animals"));

        foreach (var item in hitColliders)
        {
            // collider�n tag�n� kontrol ediyoruz
            if (item.gameObject.tag == "Wolf")
            {
                Target = item.gameObject.transform;


                Debug.LogError("K�kre");
            }
        }
    }
    // animasyonu ayarl�yoruz
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
        setAnim("isPatroll");
    }
}
