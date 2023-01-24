using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    private int m_Health;

    public int Health
    {
        get { return m_Health; }
        set { m_Health = value; }
    }
}
