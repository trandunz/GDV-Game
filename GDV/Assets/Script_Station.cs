using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Station : MonoBehaviour
{
    float m_Progress = 0.0f;
    [SerializeField] float m_MaxProgress = 100.0f;

    [SerializeField] TMPro.TextMeshProUGUI m_ProgressText;

    bool m_IsOccupied = false;
    int m_OccupantNumber = 0;

    void Update()
    {
        if (m_IsOccupied)
        {
            if (m_Progress < m_MaxProgress)
            {
                m_Progress += Time.deltaTime * m_OccupantNumber;
            }
            else if (m_Progress > m_MaxProgress)
            {
                m_IsOccupied = false;
                m_OccupantNumber = 0;
                m_Progress = 0;
            }

            Debug.Log("Task Progress : " + m_Progress + " / " + m_MaxProgress);
        }

        if (m_OccupantNumber < 0)
        {
            m_OccupantNumber = 0;
        }

        m_ProgressText.text = "Task Progress : " + (int) m_Progress + " / " + (int) m_MaxProgress;
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
