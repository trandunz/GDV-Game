﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Camera m_MainCamera;
    public GameObject m_SelectedCrewmate;
    [SerializeField] private Vector2 m_DraggedCrewmateOffset;
    public Vector2 m_CrewmateSpawnPostion;
    public float[] m_WorldBoundaries;

    void Start()
    {
        
    }

    void Update()
    {
        //Select Crewmate
        if (Input.GetMouseButtonDown(0))
        {
            Ray CursorRay = m_MainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] CursorHits = Physics.RaycastAll(CursorRay);
            bool FoundCrewmate = false;
            uint CursorHitIndex = 0;
            foreach (RaycastHit CursorHit in CursorHits)
            {
                if (CursorHit.transform.parent.GetComponent<Script_CrewMate>())
                {
                    FoundCrewmate = true; break;
                }
                CursorHitIndex++;
            }

            if (FoundCrewmate) { m_SelectedCrewmate = CursorHits[CursorHitIndex].transform.parent.gameObject; }
            else { m_SelectedCrewmate = null; }
        }

        //Drag Crewmate
        if (Input.GetMouseButton(0) && m_SelectedCrewmate != null)
        {
            Vector3 CursorWorldPosition = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
            CursorWorldPosition.z = 0;
            m_SelectedCrewmate.transform.position = CursorWorldPosition + new Vector3(m_DraggedCrewmateOffset.x, m_DraggedCrewmateOffset.y, m_SelectedCrewmate.transform.position.z);
        }
    }

    void OnDrawGizmosSelected()
    {
        //Draw World Boundaries
        if (m_WorldBoundaries.Length == 4)
        {
            Gizmos.DrawLine(new Vector3(m_WorldBoundaries[0], m_WorldBoundaries[1]), new Vector3(m_WorldBoundaries[2], m_WorldBoundaries[1]));
            Gizmos.DrawLine(new Vector3(m_WorldBoundaries[0], m_WorldBoundaries[1]), new Vector3(m_WorldBoundaries[0], m_WorldBoundaries[3]));
            Gizmos.DrawLine(new Vector3(m_WorldBoundaries[2], m_WorldBoundaries[3]), new Vector3(m_WorldBoundaries[0], m_WorldBoundaries[3]));
            Gizmos.DrawLine(new Vector3(m_WorldBoundaries[2], m_WorldBoundaries[3]), new Vector3(m_WorldBoundaries[2], m_WorldBoundaries[1]));
        }
    }
}
