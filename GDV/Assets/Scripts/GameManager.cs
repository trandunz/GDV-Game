using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int m_LevelIndex;
    public GameObject[] m_Levels;

    public Script_CrewMateManager m_CrewMateManager;

    void Start()
    {
        foreach (GameObject Level in m_Levels) { Level.SetActive(false); }
        m_Levels[m_LevelIndex].SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            UpgradeShip();
        }
    }

    public void UpgradeShip()
    {
        if (m_LevelIndex < m_Levels.Length)
        {
            m_Levels[m_LevelIndex].SetActive(false);
            m_LevelIndex++;
            m_Levels[m_LevelIndex].SetActive(true);
        }
    }
}
