using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Live : MonoBehaviour
{
    public float Health;
    public bool IsDie;

    public void DecreaseHealth(float value, Action die)
    {
        if (IsDie) return;

        Health = Health - value;
        Health = Mathf.Clamp(Health, 0, float.MaxValue);

        if (Health == 0)
        {
            IsDie = true;
            die.Invoke();
        }
    }
}
