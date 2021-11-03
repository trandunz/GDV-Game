using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CrewMateUI : MonoBehaviour
{
    Script_CrewMateManager m_CrewMateManager;
    [SerializeField] List<GameObject> m_CrewMatePanels;
    [SerializeField] List<Script_CrewMatePanel> m_CrewMatePanelScripts;

    private void Start()
    {
        m_CrewMateManager = GameObject.FindGameObjectWithTag("CrewMateManager").GetComponent<Script_CrewMateManager>();
        foreach (GameObject _Gameobject in GameObject.FindGameObjectsWithTag("CrewMatePanel"))
        {
            m_CrewMatePanels.Add(_Gameobject);
        }
        foreach (GameObject _Gameobject in m_CrewMatePanels)
        {
            m_CrewMatePanelScripts.Add(_Gameobject.GetComponent<Script_CrewMatePanel>());
        }
    }

    public void UpdateCrewMateStats()
    {
        for (int i = 0; i < m_CrewMateManager.m_CrewMates.Length; i++)
        {
            Script_CrewMate mateScript = m_CrewMateManager.m_CrewMates[i].GetComponent<Script_CrewMate>();
            m_CrewMatePanelScripts[i].m_Name.text = mateScript.m_Name;
            m_CrewMatePanelScripts[i].m_Mood.text = mateScript.m_Mood.ToString();
        }
    }

    void CheckForEmptyPanel()
    {
        foreach (GameObject _Gameobject in m_CrewMatePanels)
        {
            if (_Gameobject.GetComponent<Script_CrewMatePanel>().m_Name.text == "John Doe")
            {
                _Gameobject.SetActive(false);
            }
            else
            {
                _Gameobject.SetActive(true);
            }
        }
    }

    void Update()
    {
        UpdateCrewMateStats();
        CheckForEmptyPanel();
    }
}
