using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private Vector2 m_DraggedCrewmateOffset = new Vector2(0.0f, -0.4f);
    public Vector2 m_CrewmateSpawnPostion;
    public float[] m_WorldBoundaries;
    public BoxCollider2D[] m_CollisionBoundaries;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

        //Draw Crewmate at origin
        Gizmos.DrawWireCube(new Vector3(m_CrewmateSpawnPostion.x, m_CrewmateSpawnPostion.y + 0.4f), new Vector3(0.4f, 0.8f));

        //Draw Dragging Point
        Gizmos.DrawWireSphere(m_CrewmateSpawnPostion - m_DraggedCrewmateOffset, 0.1f);
    }

    public Vector2 GetDraggedCrewmateOffset()
    {
        return m_DraggedCrewmateOffset;
    }
}
