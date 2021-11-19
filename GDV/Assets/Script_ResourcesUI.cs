using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ResourcesUI : MonoBehaviour
{
    bool m_bShowUI = true;

    public int m_Metal = 0;
    public int m_Food = 0;
    public int m_ShipCondition = 20;
    public int m_BeastHunger = 0;

    [SerializeField] TMPro.TextMeshProUGUI m_MetalText;
    [SerializeField] TMPro.TextMeshProUGUI m_FoodText;
    [SerializeField] TMPro.TextMeshProUGUI m_ShipConditionText;
    [SerializeField] TMPro.TextMeshProUGUI m_BeastHungerText;

    [SerializeField] GameObject[] m_ShipStatusImages;

    Script_SpaceBeast m_SpaceBeast;

    void Start()
    {
        m_SpaceBeast = GameObject.FindGameObjectWithTag("SpaceBeast").GetComponent<Script_SpaceBeast>();
    }

    void Update()
    {
        m_BeastHunger = (int)m_SpaceBeast.GetHunger();

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

        m_MetalText.text = "Metal: " + m_Metal;
        m_FoodText.text = "Food: " + m_Food;
        if (m_ShipCondition >= 100)
        {
            SetStatusImagesDisable();
            m_ShipStatusImages[0].SetActive(true);
        }
        else if (m_ShipCondition >= 75)
        {
            SetStatusImagesDisable();
            m_ShipStatusImages[1].SetActive(true);
        }
        else if (m_ShipCondition >= 50)
        {
            SetStatusImagesDisable();
            m_ShipStatusImages[2].SetActive(true);
        }
        else if (m_ShipCondition >= 25)
        {
            SetStatusImagesDisable();
            m_ShipStatusImages[3].SetActive(true);
        }
        else
        {
            SetStatusImagesDisable();
            m_ShipStatusImages[3].SetActive(true);
        }

        m_BeastHungerText.text = "Hunger: " + m_BeastHunger;
    }
    public void IncreaseShipCondition(int _amount)
    {
        int i = _amount;
        while (m_ShipCondition < 100 && i > 0)
        {
            m_ShipCondition++;
            i--;
        }
    }
    public void DecreaseShipCondition(int _amount)
    {
        int i = _amount;
        while (m_ShipCondition < 100 && i > 0)
        {
            m_ShipCondition--;
            i--;
        }
    }

    void SetStatusImagesDisable()
    {
        foreach (GameObject item in m_ShipStatusImages)
        {
            item.SetActive(false);
        }
    }
}
