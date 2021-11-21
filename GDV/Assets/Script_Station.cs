using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Station : MonoBehaviour
{
    enum STATIONTYPE
    {
        UNASSIGNED = 0,

        BROKEN,
        COCKPIT,
        ENGINEERINGBAY, //Unfinished
        MEDBAY,
        BUNKROOM,
        KITCHEN, //Unfinished
        FARM
    };

    [SerializeField] float m_Progress = 0.0f;
    [SerializeField] float m_MaxProgress = 100.0f;

    [SerializeField] GameObject m_ProgressTextGameObject;
    [SerializeField] TMPro.TextMeshProUGUI m_ProgressText;

    [SerializeField] List<GameObject>m_CurrentlyOccupying;
    int m_OccupantNumber = 0;

    [SerializeField] STATIONTYPE m_StationType = STATIONTYPE.UNASSIGNED;
    [SerializeField] STATIONTYPE m_PostRepairType = STATIONTYPE.UNASSIGNED;

    Script_ResourcesUI m_ResourcesUI;

    [SerializeField] GameObject[] m_Sprites;

    private void Start()
    {
        m_ResourcesUI = GameObject.FindGameObjectWithTag("ResourcePanel").GetComponent<Script_ResourcesUI>();
        m_ProgressText = m_ProgressTextGameObject.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        if (m_OccupantNumber > 0 && m_StationType != STATIONTYPE.UNASSIGNED)
        {
            //When a task is in progress
            if (m_Progress < m_MaxProgress)
            {
                switch (m_StationType)
                {
                    case STATIONTYPE.BROKEN:
                        {
                            uint SpecialistNumber = 0;
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                if (CrewMate.GetComponent<Script_CrewMate>().m_CrewClass == Script_CrewMate.CREWCLASS.ENGINEER)
                                {
                                    SpecialistNumber++;
                                }
                            }

                            m_MaxProgress = 10;
                            m_Progress += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                            m_Progress += Time.deltaTime * SpecialistNumber * 2.0f;
                            break;
                        }
                    case STATIONTYPE.COCKPIT:
                        {
                            m_MaxProgress = 20;
                            if (m_ResourcesUI.m_Food >= 25)
                            {
                                m_Progress += Time.deltaTime;
                            }
                            break;
                        }
                    case STATIONTYPE.MEDBAY:
                        {
                            //Count how many doctors are at the bay
                            uint SpecialistNumber = 0;
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                if (CrewMate.GetComponent<Script_CrewMate>().m_CrewClass == Script_CrewMate.CREWCLASS.DOCTOR)
                                {
                                    SpecialistNumber++;
                                }
                            }

                            //Heal crewmates
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                CrewMate.GetComponent<Script_CrewMate>().m_Health += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                                CrewMate.GetComponent<Script_CrewMate>().m_Health += Time.deltaTime * SpecialistNumber * 3.0f;
                            }

                            break;
                        }
                    case STATIONTYPE.ENGINEERINGBAY:
                        {
                            break;
                        }
                    case STATIONTYPE.BUNKROOM:
                        {
                            m_MaxProgress = 50;
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                Script_CrewMate CrewMateScript = CrewMate.GetComponent<Script_CrewMate>();
                                
                                if (CrewMateScript.m_RestProgression > m_MaxProgress)
                                {
                                    if (CrewMateScript.m_Rest != 100) { CrewMateScript.m_Rest = 100.0f; }
                                }
                                else
                                {
                                    CrewMateScript.m_RestProgression += Time.deltaTime;
                                }
                            }

                            break;
                        }
                    case STATIONTYPE.KITCHEN:
                        {
                            break;
                        }
                    case STATIONTYPE.FARM:
                        {
                            uint SpecialistNumber = 0;
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                if (CrewMate.GetComponent<Script_CrewMate>().m_CrewClass == Script_CrewMate.CREWCLASS.FARMER)
                                {
                                    SpecialistNumber++;
                                }
                            }

                            m_MaxProgress = 30;
                            if (m_ResourcesUI.m_Food < 100)
                            {
                                m_Progress += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                                m_Progress += Time.deltaTime * SpecialistNumber * 2.0f;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            //When a task from the station has been finished
            else if (m_Progress > m_MaxProgress)
            {
                switch (m_StationType)
                {
                    case STATIONTYPE.BROKEN:
                        {
                            m_Sprites[0].SetActive(false);
                            m_Sprites[1].SetActive(true);
                            m_StationType = m_PostRepairType;
                            m_ResourcesUI.IncreaseShipCondition(10);
                            break;
                        }
                    case STATIONTYPE.COCKPIT:
                        {
                            GameObject.FindGameObjectWithTag("SpaceBeast").GetComponent<Script_SpaceBeast>().Feed();
                            m_ResourcesUI.m_Food -= 25;
                            break;
                        }
                    case STATIONTYPE.KITCHEN:
                        {
                            break;
                        }
                    case STATIONTYPE.FARM:
                        {
                            int Food = 25;
                            while (Food > 0)
                            {
                                if (m_ResourcesUI.m_Food < 100)
                                {
                                    m_ResourcesUI.m_Food++;
                                }
                                Food--;
                            }
                            break;
                        }
                    default:
                        break;
                }

                // Task Finishes
                m_Progress = 0;
            }

            Debug.Log("Task Progress : " + m_Progress + " / " + m_MaxProgress);
        }
        else
        {
            // Set Progress To 0 If Station Not Assigned
            m_Progress = 0;
        }

        if (m_OccupantNumber < 0)
        {
            m_OccupantNumber = 0;
        }

        if (m_Progress == 0)
        {
            if (m_ProgressTextGameObject.activeSelf == true) { m_ProgressTextGameObject.SetActive(false); }
        }
        else
        {
            if (m_ProgressTextGameObject.activeSelf == false) { m_ProgressTextGameObject.SetActive(true); }

            m_ProgressText.text = "Task Progress : " + (int)m_Progress + " / " + (int)m_MaxProgress;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag is "CrewMate")
        {
            m_CurrentlyOccupying.Add(collision.gameObject);
            m_OccupantNumber++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag is "CrewMate")
        {
            collision.GetComponent<Script_CrewMate>().m_RestProgression = 0.0f;
            m_CurrentlyOccupying.Remove(collision.gameObject);
            m_OccupantNumber--;
        }
    }
}
