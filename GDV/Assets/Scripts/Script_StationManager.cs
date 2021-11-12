using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_StationManager : MonoBehaviour
{
    int m_ShipLevel = 1;

    Script_ResourcesUI m_Resources;

    [SerializeField] int m_MaxShipLevel = 3;
    [SerializeField] GameObject[] m_ShipLevels;

    void Start()
    {
        m_Resources = GameObject.FindGameObjectWithTag("ResourcePanel").GetComponent<Script_ResourcesUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            UpgradeShip();
        }
    }

    public void UpgradeShip()
    {
        if (m_ShipLevel < m_MaxShipLevel && m_Resources.m_Metal >= 100)
        {
            DisableAllLevelObjects();
            m_ShipLevel++;
            m_ShipLevels[m_ShipLevel - 1].SetActive(true);
            m_Resources.m_Metal -= 100;
            
            
        }
    }

    void DisableAllLevelObjects()
    {
        foreach(GameObject item in m_ShipLevels)
        {
            item.SetActive(false);
        }
    }
}
