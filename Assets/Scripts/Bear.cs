using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Bear : Animal
{
    private string[] Anims = new string[]
    {
        "isPatroll" ,
        "isAttack" ,
        "isRoar" ,
        "isChase" ,
        "isDie"
    };

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
            Debug.LogError("Bo�luk!");
            return Vector3.zero;
        }
    }
    private bool OneTimeSetTargetRotation;
    private bool IsRotate;
    private Quaternion target;

    private void Update()
    {
        Debug.Log("Ay� navmeshagent = " + agent.isStopped);
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
                LockTarget = item.gameObject.transform;

                Vector3 dir = (transform.position - LockTarget.transform.position);
                float angle = Vector3.Dot(transform.forward, dir);
                if (angle > 0)
                {

                }

                // Debug.Log("Ay� " + angle);
                //   Debug.Log("Ay� -- K�kre");

                //setAnim("isRoar");
            }
        }

        //Debug.Log(IsTriggered);

        if (IsTriggered)
        {
            if (!OneTimeSetTargetRotation)
            {
                agent.isStopped = true;
                target = Quaternion.LookRotation((CurrentTarget.position - transform.position), Vector3.up);
                target = Quaternion.Euler(0, target.eulerAngles.y, 0);

                IsRotate = true;
                OneTimeSetTargetRotation = true;
            }

            if (IsRotate)
            {
                Debug.Log("E�itleniyor rotasyon");
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 100 * Time.deltaTime);
            }

            if (CheckRotation(target) && IsRotate)
            {
                Debug.Log("K�kre");
                IsRotate = false;
                setAnim("isRoar");
            }
            else
            {
                Debug.Log("e�itlenmedi");
            }
        }
    }

    public void SetRotate()
    {
        target = Quaternion.LookRotation((CurrentTarget.position - transform.position), Vector3.up);
        transform.rotation = target;
    }

    public void RoarFinished()
    {
        if (CurrentTarget != null)
        {
            Animal animal = CurrentTarget.gameObject.GetComponent<Animal>();
            animal.LockTarget = transform;
            animal.CurrentTarget = transform;
            animal.IsTriggered = true;
        }
    }

    public bool CheckRotation(Quaternion target)
    {
        return Mathf.Approximately(Mathf.Abs(Quaternion.Dot(transform.rotation, target)), 1f);
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

    public void Hit()
    {
        CurrentTarget.GetComponent<Live>().Health -= 50;
    }

    public void CheckTargetDistance()
    {
        float distance = (CurrentTarget.transform.position - transform.position).magnitude;
        if (distance > (agent.stoppingDistance + 2f))
        {
            setAnim("isChase");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.2f);
        Gizmos.DrawSphere(transform.position, 5);
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
