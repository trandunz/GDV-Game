using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SpaceBeast : MonoBehaviour
{
    float m_Hunger = 0;
    bool m_Hungry = true;

    [SerializeField] float m_DigestionRate_s = 1f;

    void Update()
    {
        if (m_Hunger < 100 && m_Hungry)
        {
            m_Hunger += Time.deltaTime * m_DigestionRate_s;
        }
    }

    public float GetHunger()
    {
        return m_Hunger;
    }

    public void Feed()
    {
        int food = 25;
        while (m_Hunger > 0 && food > 0)
        {
            food--;
            m_Hunger--;
        }
    }

    public void ResetHunger()
    {
        m_Hunger = 0;
    }
}
