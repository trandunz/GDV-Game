using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_MissionUI : MonoBehaviour
{
    bool m_bShowUI = true;

    public bool m_MissionInProgress = false;
    string m_ActiveCrewmate = "William";

    GameObject m_CrewMateManager;

    private void Awake()
    {

        m_CrewMateManager = GameObject.Find("CrewMateManager");
    }
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

    public void ToggleBool(string _button)
    {
        if (!m_MissionInProgress)
        {
            switch (_button)
            {
                case "William":
                    {
                        m_ActiveCrewmate = _button;
                        break;
                    }
                case "Michael":
                    {
                        m_ActiveCrewmate = _button;
                        break;

                    }
                case "Jerome":
                    {
                        m_ActiveCrewmate = _button;
                        break;
                    }
                case "Bradan":
                    {
                        m_ActiveCrewmate = _button;
                        break;
                    }
                default:
                    break;
            }
        }
    }

    bool IsEngineeringBayReady()
    {
        foreach (GameObject item in GameObject.FindObjectOfType<GameManager>().m_Stations)
        {
            Script_Station[] m_Stations = item.GetComponentsInChildren<Script_Station>();
            foreach(Script_Station station in m_Stations)
            {
                if (station.m_PostRepairType == Script_Station.STATIONTYPE.ENGINEERINGBAY)
                {
                    if (station.m_Sprites[2].activeSelf)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    void DeployShip()
    {
        foreach (GameObject item in GameObject.FindObjectOfType<GameManager>().m_Stations)
        {
            Script_Station[] m_Stations = item.GetComponentsInChildren<Script_Station>();
            foreach (Script_Station station in m_Stations)
            {
                if (station.m_PostRepairType == Script_Station.STATIONTYPE.ENGINEERINGBAY)
                {
                    station.m_Sprites[2].SetActive(false);
                    return;
                }
            }
        }
    }

    void ReturnShip()
    {
        foreach (GameObject item in GameObject.FindObjectOfType<GameManager>().m_Stations)
        {
            Script_Station[] m_Stations = item.GetComponentsInChildren<Script_Station>();
            foreach (Script_Station station in m_Stations)
            {
                if (station.m_PostRepairType == Script_Station.STATIONTYPE.ENGINEERINGBAY)
                {
                    if (station.m_Sprites.Length >= 3)
                    {
                        if (!station.m_Sprites[2].activeSelf)
                        {
                            station.m_Sprites[2].SetActive(true);
                            return;
                        }
                    }
                }
            }
        }
    }

    public void GoMission()
    {
        if (m_MissionInProgress == false && IsEngineeringBayReady())
        {
            m_MissionInProgress = true;
            StartCoroutine(Mission());
        }
    }

    IEnumerator Mission()
    {
        switch(m_ActiveCrewmate)
        {
            case "William":
                {
                    foreach (GameObject iteem in m_CrewMateManager.GetComponent<Script_CrewMateManager>().m_CrewMates)
                    {
                        if (iteem.GetComponent<Script_CrewMate>().m_CrewmateName == "William")
                        {
                            iteem.SetActive(false);
                        }
                    }
                    

                    break;
                }
            case "Michael":
                {
                    foreach (GameObject iteem in m_CrewMateManager.GetComponent<Script_CrewMateManager>().m_CrewMates)
                    {
                        if (iteem.GetComponent<Script_CrewMate>().m_CrewmateName == "Michael")
                        {
                            iteem.SetActive(false);
                        }
                    }
                    break;

                }
            case "Jerome":
                {
                    foreach (GameObject iteem in m_CrewMateManager.GetComponent<Script_CrewMateManager>().m_CrewMates)
                    {
                        if (iteem.GetComponent<Script_CrewMate>().m_CrewmateName == "Jerome")
                        {
                            iteem.SetActive(false);
                        }
                    }
                    break;
                }
            case "Bradan":
                {
                    foreach (GameObject iteem in m_CrewMateManager.GetComponent<Script_CrewMateManager>().m_CrewMates)
                    {
                        if (iteem.GetComponent<Script_CrewMate>().m_CrewmateName == "Bradan")
                        {
                            iteem.SetActive(false);
                        }
                    }
                    break;
                }
            default:
                break;
        }

        DeployShip();

        yield return new WaitForSeconds(Random.Range(20, 40));

        ReturnShip();

        GameObject.FindObjectOfType<Script_ResourcesUI>().m_Metal += 50;
        Random.InitState((int)Time.deltaTime);
        int rand = Random.Range(0, 6);
        switch(rand)
        {
            case 2:
                {
                    int Food = 25;
                    while (Food > 0)
                    {
                        if (GameObject.FindObjectOfType<Script_ResourcesUI>().m_Food < 100)
                        {
                            GameObject.FindObjectOfType<Script_ResourcesUI>().m_Food++;
                        }
                        Food--;
                    }

                    break;
                }
            case 3:
                {
                    HandleRandomDamage();

                    break;
                }
            case 4:
                {
                    HandleRandomNewCrewMate();

                    break;
                }
            default:
                break;
        }

        m_MissionInProgress = false;

        switch (m_ActiveCrewmate)
        {
            case "William":
                {
                    foreach (GameObject iteem in m_CrewMateManager.GetComponent<Script_CrewMateManager>().m_CrewMates)
                    {
                        if (iteem.GetComponent<Script_CrewMate>().m_CrewmateName == "William")
                        {
                            iteem.SetActive(true);
                        }
                    }

                    break;
                }
            case "Michael":
                {
                    foreach (GameObject iteem in m_CrewMateManager.GetComponent<Script_CrewMateManager>().m_CrewMates)
                    {
                        if (iteem.GetComponent<Script_CrewMate>().m_CrewmateName == "Michael")
                        {
                            iteem.SetActive(true);
                        }
                    }
                    break;

                }
            case "Jerome":
                {
                    foreach (GameObject iteem in m_CrewMateManager.GetComponent<Script_CrewMateManager>().m_CrewMates)
                    {
                        if (iteem.GetComponent<Script_CrewMate>().m_CrewmateName == "Jerome")
                        {
                            iteem.SetActive(true);
                        }
                    }
                    break;
                }
            case "Bradan":
                {
                    foreach(GameObject iteem in m_CrewMateManager.GetComponent<Script_CrewMateManager>().m_CrewMates)
                    {
                        if (iteem.GetComponent<Script_CrewMate>().m_CrewmateName == "Bradan")
                        {
                            iteem.SetActive(true);
                        }
                    }
                    break;
                }
            default:
                break;
        }
    }

    void HandleRandomNewCrewMate()
    {
        int rand = Random.Range(0, 3);
        switch(rand)
        {
            case 0:
                {
                    m_CrewMateManager.GetComponent<Script_CrewMateManager>().AddCrewMate("William");
                    break;
                }
            case 1:
                {
                    m_CrewMateManager.GetComponent<Script_CrewMateManager>().AddCrewMate("Michael");
                    break;
                }
            case 2:
                {
                    m_CrewMateManager.GetComponent<Script_CrewMateManager>().AddCrewMate("Jerome");
                    break;
                }
            case 3:
                {
                    m_CrewMateManager.GetComponent<Script_CrewMateManager>().AddCrewMate("Bradan");
                    break;
                }
            default:
                break;
        }
        
    }

    void HandleRandomDamage()
    {
        switch (m_ActiveCrewmate)
        {
            case "William":
                {
                    if (GameObject.Find("CrewMate"))
                    {
                        GameObject.Find("CrewMate").GetComponent<Script_CrewMate>().m_Health = 0.25f;
                    }
                    break;
                }
            case "Michael":
                {
                    if (GameObject.Find("Chef"))
                    {
                        GameObject.Find("Chef").GetComponent<Script_CrewMate>().m_Health = 0.25f;
                    }
                    break;
                }
            case "Jerome":
                {
                    if (GameObject.Find("Doctor"))
                    {
                        GameObject.Find("Doctor").GetComponent<Script_CrewMate>().m_Health = 0.25f;
                    }
                    break;
                }
            case "Bradan":
                {
                    if (GameObject.Find("Engineer"))
                    {
                        GameObject.Find("Engineer").GetComponent<Script_CrewMate>().m_Health = 0.25f;
                    }

                    break;
                }
            default:
                break;
        }
    }
}
