using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : Live
{
    public bool IsTriggered;

    public Transform CurrentTarget;
    public Transform LockTarget;

    public NavMeshAgent agent;
}
