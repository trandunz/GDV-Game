using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_WorldTimer : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI m_WorldTimerText;
    [SerializeField] TMPro.TextMeshProUGUI m_DayText;
    int m_Hours = 0;
    int m_Minutes = 0;
    float m_Timer = 0;
    string m_Text;
    public int m_Day = 0;
    [SerializeField] float m_DebugSpeedup = 1.0f;
    public bool m_UpdateHours = false;
    void LateUpdate()
    {
        m_Text = "";
        m_Timer += Time.deltaTime * m_DebugSpeedup;
        if ((int)m_Timer % 1 == 0)
        {
            m_Minutes = (int)m_Timer % 60;
        }
        if (m_Minutes % 60 == 0)
        {
            m_Minutes = 0;
            if (m_UpdateHours)
            {
                m_Hours++;
                m_UpdateHours = false;
            }
        }
        if (m_Minutes % 57 == 0)
        {
            StopAllCoroutines();
            StartCoroutine(Delay());
        }
        if (m_Hours == 24)
        {
            m_Day++;
            m_Hours = 0;
        }
        if (m_Hours == 0)
        {
            m_Text = "00:";
        }
        else if (m_Hours < 10)
        {
            m_Text = "0" + m_Hours + ":";
        }
        else
        {
            m_Text = m_Hours + ":";
        }
        if (m_Minutes == 0)
        {
            m_Text = m_Text + "00";
        }
        else if (m_Minutes < 10)
        {
            m_Text = m_Text + "0" + m_Minutes;
        }
        else
        {
            m_Text = m_Text + m_Minutes;
        }
        m_WorldTimerText.SetText("");
        m_WorldTimerText.SetText(m_Text);

        m_DayText.SetText("Day:" + m_Day.ToString());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        if (m_Minutes % 60 == 0) { m_UpdateHours = true; }
    }
}
