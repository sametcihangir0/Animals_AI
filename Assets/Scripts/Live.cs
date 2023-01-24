using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Live : MonoBehaviour
{

    [SerializeField] private float m_Health;

    public float Health
    {
        get { return m_Health; }
        set
        {
            m_Health = Mathf.Clamp(value, 0, float.MaxValue);
        }
    }
}
