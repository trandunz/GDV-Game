using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_MissionUI : MonoBehaviour
{
    bool m_bShowUI = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            m_bShowUI = !m_bShowUI;

            if (m_bShowUI)
            {
                GetComponent<Animator>().SetTrigger("Show");
            }
            else
            {
                GetComponent<Animator>().SetTrigger("Hide");
            }
        }
    }
}
