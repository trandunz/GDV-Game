using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Interactable : MonoBehaviour
{
    [SerializeField] string m_ObjectName;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag is "Player" && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F)))
        {
            Debug.Log("Player Interacted With " + m_ObjectName + "!");
        }
    }
}
