using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int m_LevelIndex;
    public GameObject[] m_Levels;
    public GameObject[] m_Stations;

    public Script_CrewMateManager m_CrewMateManager;

    void Start()
    {
        foreach (GameObject Level in m_Levels) { Level.SetActive(false); }
        m_Levels[m_LevelIndex].SetActive(true);

        foreach (GameObject Level in m_Stations) { Level.SetActive(false); }
        m_Stations[m_LevelIndex].SetActive(true);
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
        if (GameObject.FindObjectOfType<Script_ResourcesUI>().m_Metal >= 200)
        {
            if (m_LevelIndex + 1 == m_Levels.Length)
            {
                WinCondition();
            }
            else if (m_LevelIndex + 1 < m_Levels.Length)
            {
                GameObject.FindObjectOfType<Script_ResourcesUI>().m_Metal -= 200;
                m_Levels[m_LevelIndex].SetActive(false);
                m_LevelIndex++;
                m_Levels[m_LevelIndex].SetActive(true);
                m_Stations[m_LevelIndex].SetActive(true);
            }
        }
    }

    void WinCondition()
    {

        StartCoroutine(WaitForTime(2)); // Wait Delay For Animation

    }

    IEnumerator WaitForTime(int _seconds)
    {
        // Some Animation
        yield return new WaitForSeconds(_seconds);
        // Some UI
        SceneManager.LoadSceneAsync(0); // Main Menu
    }
}
