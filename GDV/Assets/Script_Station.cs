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

    [SerializeField] TMPro.TextMeshProUGUI m_ProgressText;

    bool m_IsOccupied = false;
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
        m_IsOccupied = m_OccupantNumber > 0 ? true : false;

        if (m_IsOccupied && m_StationType != STATIONTYPE.UNASSIGNED)
        {
            if (m_Progress < m_MaxProgress)
            {
                switch (m_StationType)
                {
                    case STATIONTYPE.BROKEN:
                        {
                            m_MaxProgress = 10;
                            m_Progress += Time.deltaTime * m_OccupantNumber;
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
                            //m_Sprites[1].SetActive(true);
                            m_MaxProgress = 30;
                            if (m_ResourcesUI.m_Food < 100)
                            {
                                m_Progress += Time.deltaTime * m_OccupantNumber;
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
                            int food = 25;
                            while (food > 0)
                            {
                                if (m_ResourcesUI.m_Food < 100)
                                {
                                    m_ResourcesUI.m_Food++;
                                }
                                food--;
                            }
                            break;
                        }
                    default:
                        break;
                }

                // Task Finishe
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
            m_OccupantNumber++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag is "CrewMate")
        {
            m_OccupantNumber--;
        }
    }
}
