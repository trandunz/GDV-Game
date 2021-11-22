using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_ResourcesUI : MonoBehaviour
{
    bool m_bShowUI = true;

    public int m_Metal = 0;
    public int m_Food = 0;
    public int m_ShipCondition = 50;
    public int m_BeastHunger = 0;
    public bool m_ReadyToSearch = false;
    public bool m_ChefInKitchen = false;

    [SerializeField] TMPro.TextMeshProUGUI m_MetalText;
    [SerializeField] TMPro.TextMeshProUGUI m_FoodText;
    [SerializeField] TMPro.TextMeshProUGUI m_ShipConditionText;
    [SerializeField] TMPro.TextMeshProUGUI m_BeastHungerText;

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
        m_ShipConditionText.text = "Condition: " + m_ShipCondition;
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
}
