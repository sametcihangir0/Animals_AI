using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wolf : Animal
{
    private RaycastHit hit;

    public AudioClip HowlClip;

    public Vector3 GetPoint()
    {
        Vector3 x = transform.forward * Random.Range(1f, 8f);
        Vector3 z = transform.right * Random.Range(1f, 8f);
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

    private bool OneTimeSetTargetRotation;
    private bool IsRotate;
    private Quaternion target;
    // Belirlediðimiz çapýn içerisindeki colliderlarý sana döndürür
    public void Check(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius, LayerMask.GetMask("Animals"));

        foreach (var item in hitColliders)
        {
            // colliderýn tagýný kontrol ediyoruz
            if (item.gameObject.tag == "Bear")
            {
                LockTarget = item.gameObject.transform;

                Vector3 dir = (LockTarget.transform.position - transform.position);
                float angle = Vector3.Dot(transform.forward, dir);

                if (angle > 0)
                {
                    CurrentTarget = LockTarget;
                    IsTriggered = true;
                }
            }
        }

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
                transform.rotation = Quaternion.RotateTowards(transform.rotation, target, 165 * Time.deltaTime);
            }

            if (CheckRotation(target) && IsRotate)
            {
                IsRotate = false;
                setAnim("isRoar");
            }
        }
    }
    public void Howl()
    {
        AudioSource.PlayOneShot(HowlClip);
    }

    public void SetRotate()
    {
        target = Quaternion.LookRotation((CurrentTarget.position - transform.position), Vector3.up);
        transform.rotation = target;
    }
    public void RoarBegin()
    {
        if (CurrentTarget != null)
        {
            Animal animal = CurrentTarget.gameObject.GetComponent<Animal>();
            animal.LockTarget = transform;
            animal.CurrentTarget = transform;
            animal.IsTriggered = true;
        }
    }

    public void RoarFinished()
    {
       
    }

    public bool CheckRotation(Quaternion target)
    {
        return Mathf.Approximately(Mathf.Abs(Quaternion.Dot(transform.rotation, target)), 1f);
    }
    public void Hit()
    {
        CurrentTarget.GetComponent<Live>().DecreaseHealth(25, Die);
    }

    public void CheckTargetDistance()
    {
        if (IsDie) return;

        float distance = (CurrentTarget.transform.position - transform.position).magnitude;
        if (distance > (agent.stoppingDistance + 2f))
        {
            setAnim("isChase");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.2f);
        Gizmos.DrawSphere(transform.position, 15);
    }


    // Karþý tarafýn ölüm fonksiyonu
    public void Die()
    {
        CurrentTarget.GetComponent<Animator>().SetTrigger("Deneme");
        setAnim("");
    }

    void Start()
    {
        animator.SetInteger("isIdleIndex", 1);
        setAnim("isPatroll");
    }
}
