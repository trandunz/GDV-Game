using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CrewMateManager : MonoBehaviour
{
    public GameObject[] m_CrewMates;

    public Script_CrewMate m_activeCrewmate;

    void Start()
    {
        m_CrewMates = GameObject.FindGameObjectsWithTag("CrewMate");
    }
}
