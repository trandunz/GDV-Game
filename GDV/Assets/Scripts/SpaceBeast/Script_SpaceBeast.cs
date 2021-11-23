using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SpaceBeast : MonoBehaviour
{
    float m_Hunger = 0;
    bool m_Hungry = true;

    [SerializeField] float m_DigestionRate_s = 0.5f;

    Script_CrewMateManager m_CrewmateManager;
    private void Start()
    {
        m_CrewmateManager = GameObject.FindObjectOfType<Script_CrewMateManager>();
    }

    void Update()
    {
        if (m_Hunger < 100 && m_Hungry)
        {
            m_Hunger += Time.deltaTime * m_DigestionRate_s;
        }
        if (m_Hunger >= 100)
        {
            StartCoroutine(EatShip());
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

    IEnumerator EatShip()
    {
        foreach (GameObject crewmate in m_CrewmateManager.m_CrewMates)
        {
            crewmate.GetComponent<Script_CrewMate>().m_Health = 1;
        }
        yield return new WaitForSeconds(2);
        foreach (GameObject crewmate in m_CrewmateManager.m_CrewMates)
        {
            crewmate.GetComponent<Script_CrewMate>().m_Health = 0;
        }
        m_CrewmateManager.LossCondition();
    }
}
