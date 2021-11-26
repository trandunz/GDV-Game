using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Station : MonoBehaviour
{
    public enum STATIONTYPE
    {
        UNASSIGNED = 0,

        BROKEN,
        COCKPIT,
        ENGINEERINGBAY,
        MEDBAY,
        BUNKROOM,
        KITCHEN,
        FARM,
        ENDGAME
    };

    [SerializeField] float m_Progress = 0.0f;
    [SerializeField] float m_MaxProgress = 100.0f;

    [SerializeField] GameObject m_ProgressTextGameObject;
    TMPro.TextMeshProUGUI m_ProgressText;

    [SerializeField] List<GameObject>m_CurrentlyOccupying;
    int m_OccupantNumber = 0;

    [SerializeField] STATIONTYPE m_StationType = STATIONTYPE.UNASSIGNED;
    public STATIONTYPE m_PostRepairType = STATIONTYPE.UNASSIGNED;

    Script_ResourcesUI m_ResourcesUI;

    public GameObject[] m_Sprites;

    [SerializeField] GameObject m_SoundPrefab;
    [SerializeField] AudioClip m_FixStation;
    [SerializeField] AudioClip m_ReadyToSearch;
    [SerializeField] AudioClip m_Sparks;

    GameObject m_Audio;

    private void Start()
    {
        m_ResourcesUI = GameObject.FindGameObjectWithTag("ResourcePanel").GetComponent<Script_ResourcesUI>();

        m_ProgressText = m_ProgressTextGameObject.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        if (m_OccupantNumber > 0 && m_StationType != STATIONTYPE.UNASSIGNED)
        {
            //When a task is in progress
            if (m_Progress < m_MaxProgress)
            {
                switch (m_StationType)
                {
                    case STATIONTYPE.BROKEN:
                        {
                            uint SpecialistNumber = 0;
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                if (CrewMate.GetComponent<Script_CrewMate>().m_CrewClass == Script_CrewMate.CREWCLASS.ENGINEER)
                                {
                                    SpecialistNumber++;
                                }
                            }

                            m_MaxProgress = 10;
                            m_Progress += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                            m_Progress += Time.deltaTime * SpecialistNumber * 2.0f;

                            if (!m_Audio)
                            {
                                m_Audio = Instantiate(m_SoundPrefab, transform);
                            }
                            if (!m_Audio.GetComponent<AudioSource>().isPlaying)
                            {
                                m_Audio.GetComponent<AudioSource>().clip = m_Sparks;
                                m_Audio.GetComponent<AudioSource>().volume = 0.2f;
                                m_Audio.GetComponent<AudioSource>().Play();
                            }

                            foreach (GameObject item in m_CurrentlyOccupying)
                            {
                                item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.DOING;
                            }
                            break;
                        }
                    case STATIONTYPE.COCKPIT:
                        {
                            m_MaxProgress = 20;
                            if (m_ResourcesUI.m_Food >= 25)
                            {
                                m_Progress += Time.deltaTime;

                                foreach (GameObject item in m_CurrentlyOccupying)
                                {
                                    item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.DOING;
                                }
                            }
                            else
                            {
                                foreach (GameObject item in m_CurrentlyOccupying)
                                {
                                    item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.ONROUTE;
                                }
                            }
                            break;
                        }
                    case STATIONTYPE.MEDBAY:
                        {
                            //Count how many doctors are at the bay
                            uint SpecialistNumber = 0;
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                if (CrewMate.GetComponent<Script_CrewMate>().m_CrewClass == Script_CrewMate.CREWCLASS.DOCTOR)
                                {
                                    SpecialistNumber++;
                                }
                            }

                            //Heal crewmates
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                foreach (GameObject item in m_CurrentlyOccupying)
                                {
                                    item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.DOING;
                                }
                                CrewMate.GetComponent<Script_CrewMate>().m_Health += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                                CrewMate.GetComponent<Script_CrewMate>().m_Health += Time.deltaTime * SpecialistNumber * 3.0f;
                            }

                            break;
                        }
                    case STATIONTYPE.ENGINEERINGBAY:
                        {
                            if (!m_ResourcesUI.m_ReadyToSearch)
                            {
                                uint SpecialistNumber = 0;
                                foreach (GameObject CrewMate in m_CurrentlyOccupying)
                                {
                                    if (CrewMate.GetComponent<Script_CrewMate>().m_CrewClass == Script_CrewMate.CREWCLASS.ENGINEER)
                                    {
                                        SpecialistNumber++;
                                    }
                                }

                                m_MaxProgress = 30;
                                m_Progress += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                                m_Progress += Time.deltaTime * SpecialistNumber * 2.0f;

                                foreach (GameObject item in m_CurrentlyOccupying)
                                {
                                    item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.DOING;
                                }
                            }
                            else
                            {
                                foreach (GameObject item in m_CurrentlyOccupying)
                                {
                                    item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.ONROUTE;

                                    if (GameObject.FindObjectOfType<Script_MissionUI>().m_MissionInProgress)
                                    {
                                        m_Sprites[2].SetActive(false);
                                    }
                                    else if (!GameObject.FindObjectOfType<Script_MissionUI>().m_MissionInProgress)
                                    {
                                        m_Sprites[2].SetActive(true);
                                    }
                                }
                            }

                            break;
                        }
                    case STATIONTYPE.BUNKROOM:
                        {
                            m_MaxProgress = 50;
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                Script_CrewMate CrewMateScript = CrewMate.GetComponent<Script_CrewMate>();
                                
                                if (CrewMateScript.m_RestProgression > m_MaxProgress)
                                {
                                    if (CrewMateScript.m_Rest != 100) { CrewMateScript.m_Rest = 100.0f; }
                                }
                                else
                                {
                                    CrewMateScript.m_RestProgression += Time.deltaTime;
                                }
                            }

                            break;
                        }
                    case STATIONTYPE.KITCHEN:
                        {
                            m_ResourcesUI.m_ChefInKitchen = false;
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                if (CrewMate.GetComponent<Script_CrewMate>().m_CrewClass == Script_CrewMate.CREWCLASS.CHEF)
                                {
                                    m_ResourcesUI.m_ChefInKitchen = true;
                                    break;
                                }
                            }

                            break;
                        }
                    case STATIONTYPE.FARM:
                        {
                            uint SpecialistNumber = 0;
                            foreach (GameObject CrewMate in m_CurrentlyOccupying)
                            {
                                if (CrewMate.GetComponent<Script_CrewMate>().m_CrewClass == Script_CrewMate.CREWCLASS.FARMER)
                                {
                                    SpecialistNumber++;
                                }
                            }

                            m_MaxProgress = 30;
                            if (m_ResourcesUI.m_Food < 100)
                            {
                                m_Progress += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                                m_Progress += Time.deltaTime * SpecialistNumber * 2.0f;
                                foreach (GameObject item in m_CurrentlyOccupying)
                                {
                                    item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.DOING;
                                }
                            }
                            else
                            {
                                foreach (GameObject item in m_CurrentlyOccupying)
                                {
                                    item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.ONROUTE;
                                }
                            }
                            break;
                        }
                    case STATIONTYPE.ENDGAME:
                        {
                            if (!m_ResourcesUI.m_ReadyToSearch)
                            {
                                uint SpecialistNumber = 0;
                                foreach (GameObject CrewMate in m_CurrentlyOccupying)
                                {
                                    if (CrewMate.GetComponent<Script_CrewMate>().m_CrewClass == Script_CrewMate.CREWCLASS.ENGINEER)
                                    {
                                        SpecialistNumber++;
                                    }
                                }

                                m_MaxProgress = 30;
                                m_Progress += Time.deltaTime * (m_OccupantNumber - (int)SpecialistNumber);
                                m_Progress += Time.deltaTime * SpecialistNumber * 2.0f;

                                foreach (GameObject item in m_CurrentlyOccupying)
                                {
                                    item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.DOING;
                                }
                            }
                            else
                            {
                                foreach (GameObject item in m_CurrentlyOccupying)
                                {
                                    item.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.ONROUTE;
                                }
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            //When a task from the station has been finished
            else if (m_Progress >= m_MaxProgress)
            {
                if (!m_Audio)
                {
                    m_Audio = Instantiate(m_SoundPrefab, transform);
                }
                if (m_Audio.GetComponent<AudioSource>().isPlaying)
                {
                    m_Audio.GetComponent<AudioSource>().Stop();
                }
                switch (m_StationType)
                {
                    case STATIONTYPE.BROKEN:
                        {
                            m_Sprites[0].SetActive(false);
                            m_Sprites[1].SetActive(true);
                            m_StationType = m_PostRepairType;
                            m_ResourcesUI.IncreaseShipCondition(10);

                            GameObject SoundGameObject = Instantiate(m_SoundPrefab);
                            SoundGameObject.GetComponent<AudioSource>().clip = m_FixStation;
                            SoundGameObject.GetComponent<AudioSource>().volume = 0.2f;
                            SoundGameObject.GetComponent<AudioSource>().Play();

                            break;
                        }
                    case STATIONTYPE.COCKPIT:
                        {
                            GameObject.FindGameObjectWithTag("SpaceBeast").GetComponent<Script_SpaceBeast>().Feed();
                            m_ResourcesUI.m_Food -= 25;
                            break;
                        }
                    case STATIONTYPE.ENGINEERINGBAY:
                        {
                            m_ResourcesUI.m_ReadyToSearch = true;
                            m_Sprites[0].SetActive(false);
                            m_Sprites[1].SetActive(true);

                            GameObject SoundGameObject = Instantiate(m_SoundPrefab);
                            SoundGameObject.GetComponent<AudioSource>().clip = m_ReadyToSearch;
                            SoundGameObject.GetComponent<AudioSource>().volume = 0.2f;
                            SoundGameObject.GetComponent<AudioSource>().Play();

                            break;
                        }
                    case STATIONTYPE.FARM:
                        {
                            int Food = 25;
                            while (Food > 0)
                            {
                                if (m_ResourcesUI.m_Food < 100)
                                {
                                    m_ResourcesUI.m_Food++;
                                }
                                Food--;
                            }
                            break;
                        }
                    case STATIONTYPE.ENDGAME:
                        {
                            GameObject.FindObjectOfType<GameManager>().UpgradeShip();
                            break;
                        }
                    default:
                        break;
                }

                // Task Finishes
                m_Progress = 0;

            }
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

        if (m_Progress == 0)
        {
            m_ProgressText.text = "";

            if (m_ProgressTextGameObject.activeSelf)
            {
                m_ProgressTextGameObject.SetActive(false);
            }
        }
        else
        {
            m_ProgressText.text = "Task Progress : " + (int)m_Progress + " / " + (int)m_MaxProgress;

            if (!m_ProgressTextGameObject.activeSelf)
            {
                m_ProgressTextGameObject.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag is "CrewMate")
        {
            m_CurrentlyOccupying.Add(collision.gameObject);
            m_OccupantNumber++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag is "CrewMate")
        {
            collision.GetComponent<Script_CrewMate>().m_RestProgression = 0.0f;
            m_CurrentlyOccupying.Remove(collision.gameObject);
            collision.gameObject.GetComponent<Script_CrewMate>().m_MateState = Script_CrewMate.MATESTATE.IDLE;
            m_OccupantNumber--;

            if (m_StationType == STATIONTYPE.KITCHEN && m_OccupantNumber <= 0)
            {
                m_ResourcesUI.m_ChefInKitchen = false;
            }
        }
    }
}
