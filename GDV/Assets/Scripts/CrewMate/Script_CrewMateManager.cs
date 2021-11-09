using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CrewMateManager : MonoBehaviour
{
    public GameObject[] m_CrewMates;
    private GameObject m_GameManager;

    public Script_CrewMate m_activeCrewmate;

    void Start()
    {
        m_CrewMates = GameObject.FindGameObjectsWithTag("CrewMate");
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //Called when a new level loads (Not tested)
        if (m_GameManager == null)
        {
            m_GameManager = GameObject.FindGameObjectWithTag("GameManager");
            
            //Place all its crewmates at spawn position
            foreach (GameObject CrewMate in m_CrewMates)
            {
                CrewMate.transform.position = m_GameManager.GetComponent<GameManager>().m_CrewmateSpawnPostion;
            }
        }
    }
}
