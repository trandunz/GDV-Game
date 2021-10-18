using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_OneWayPlatform : MonoBehaviour
{
    float m_WaitTime;
    PlatformEffector2D m_Effector;

    void Start()
    {
        m_Effector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.S))
        {
            m_WaitTime = 0.5f;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(FlipEffector());
        }
        if (Input.GetKey(KeyCode.S))
        {
            if (m_WaitTime <= 0)
            {
                m_WaitTime = 0.5f;
            }
            else
            {
                m_WaitTime -= Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            m_Effector.rotationalOffset = 0;
        }
    }

    IEnumerator FlipEffector()
    {
        m_Effector.rotationalOffset = 180.0f;
        yield return new WaitForSeconds(0.2f);
        m_Effector.rotationalOffset = 0;
    }
}
