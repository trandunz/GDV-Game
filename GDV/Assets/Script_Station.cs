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
        MEDBAY,
        BUNKROOM,
        KITCHEN
    };

    float m_Progress = 0.0f;
    [SerializeField] float m_MaxProgress = 100.0f;
    static float m_SpecialistBonusProgress = 2.0f; 

    [SerializeField] TMPro.TextMeshProUGUI m_ProgressText;

    [SerializeField] List<Script_CrewMate>m_CurrentlyOccupying;
    int m_OccupantNumber = 0;

    [SerializeField] STATIONTYPE m_StationType = STATIONTYPE.UNASSIGNED;
    [SerializeField] STATIONTYPE m_PostRepairType = STATIONTYPE.UNASSIGNED;

    Script_ResourcesUI m_ResourcesUI;

    [SerializeField] GameObject[] m_Sprites;

    private void Start()
    {
        m_ResourcesUI = GameObject.FindGameObjectWithTag("ResourcePanel").GetComponent<Script_ResourcesUI>();
    }

    void Update()
    {
        if (m_OccupantNumber > 0 && m_StationType != STATIONTYPE.UNASSIGNED)
        {
            if (m_Progress < m_MaxProgress)
            {
                switch (m_StationType)
                {
                    case STATIONTYPE.BROKEN:
                        {
                            uint SpecialistNumber = 0;
                            foreach (Script_CrewMate CrewMate in m_CurrentlyOccupying)
                            {
                                if (CrewMate.m_Class == Script_CrewMate.CLASS.ENGINEER)
                                {
                                    SpecialistNumber++;
                                }
                            }

                            m_MaxProgress = 10;
                            m_Progress += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                            m_Progress += Time.deltaTime * SpecialistNumber * m_SpecialistBonusProgress;
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
                            break;
                        }
                    case STATIONTYPE.BUNKROOM:
                        {
                            break;
                        }
                    case STATIONTYPE.KITCHEN:
                        {
                            uint SpecialistNumber = 0;
                            foreach (Script_CrewMate CrewMate in m_CurrentlyOccupying)
                            {
                                if (CrewMate.m_Class == Script_CrewMate.CLASS.CHEF)
                                {
                                    SpecialistNumber++;
                                }
                            }

                            m_MaxProgress = 30;
                            if (m_ResourcesUI.m_Food < 100)
                            {
                                m_Progress += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                                m_Progress += Time.deltaTime * SpecialistNumber * m_SpecialistBonusProgress;
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
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
                    case STATIONTYPE.MEDBAY:
                        {
                            break;
                        }
                    case STATIONTYPE.BUNKROOM:
                        {
                            break;
                        }
                    case STATIONTYPE.KITCHEN:
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

        m_ProgressText.text = "Task Progress : " + (int) m_Progress + " / " + (int) m_MaxProgress;

        if (m_Progress == 0)
        {
            m_ProgressText.text = "";
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag is "CrewMate")
        {
            m_CurrentlyOccupying.Add(collision.GetComponent<Script_CrewMate>());
            m_OccupantNumber++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag is "CrewMate")
        {
            m_CurrentlyOccupying.Remove(collision.GetComponent<Script_CrewMate>());
            m_OccupantNumber--;
        }
    }
}
