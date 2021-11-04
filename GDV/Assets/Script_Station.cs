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
        BUNKROOM
    };

    float m_Progress = 0.0f;
    [SerializeField] float m_MaxProgress = 100.0f;

    [SerializeField] TMPro.TextMeshProUGUI m_ProgressText;

    bool m_IsOccupied = false;
    int m_OccupantNumber = 0;

    [SerializeField] STATIONTYPE m_StationType = STATIONTYPE.UNASSIGNED;

    Script_ResourcesUI m_ResourcesUI;

    private void Start()
    {
        m_ResourcesUI = GameObject.FindGameObjectWithTag("ResourcePanel").GetComponent<Script_ResourcesUI>();
    }

    void Update()
    {
        if (m_IsOccupied && m_StationType != STATIONTYPE.UNASSIGNED)
        {
            if (m_Progress < m_MaxProgress)
            {
                m_Progress += Time.deltaTime * m_OccupantNumber;
            }
            else if (m_Progress > m_MaxProgress)
            {
                // Task Finished
                m_IsOccupied = false;
                m_OccupantNumber = 0;
                m_Progress = 0;

                switch (m_StationType)
                {
                    case STATIONTYPE.BROKEN:
                        {
                            m_StationType = STATIONTYPE.UNASSIGNED;
                            m_ResourcesUI.IncreaseShipCondition(10);
                            break;
                        }
                    case STATIONTYPE.COCKPIT:
                        {
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
                    default:
                        break;
                }
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
            m_IsOccupied = true;
            m_OccupantNumber++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag is "CrewMate")
        {
            m_IsOccupied = false;
            m_OccupantNumber--;
        }
    }
}
