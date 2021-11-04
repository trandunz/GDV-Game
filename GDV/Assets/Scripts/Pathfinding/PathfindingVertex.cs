using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingVertex : MonoBehaviour
{
    public float m_VertexTriggerRadius;
    public CEdge[] AdjacencyList;
    //private bool IsSelectedInScene;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;

        //Draw Edges
        if (transform.parent.GetComponent<PathfindingController>().m_ShowGraph)
        {
            float arrowLength = 0.8f;
            float arrowHeadAngle = 13.0f;

            foreach (CEdge Edge in AdjacencyList)
            {
                Vector3 right = Quaternion.Euler(0, 0, arrowHeadAngle) * (transform.position - Edge.m_ConnectedVertex.transform.position).normalized;
                Vector3 left = Quaternion.Euler(0, 0, -arrowHeadAngle) * (transform.position - Edge.m_ConnectedVertex.transform.position).normalized;
                Gizmos.DrawRay(Edge.m_ConnectedVertex.transform.position, right * arrowLength);
                Gizmos.DrawRay(Edge.m_ConnectedVertex.transform.position, left * arrowLength);
                
                Gizmos.DrawLine(transform.position, Edge.m_ConnectedVertex.transform.position);
            }
        }
    }
}

[System.Serializable]
public class CEdge
{
    public string m_Property;
    public GameObject m_ConnectedVertex;

    public CEdge(string _Property, GameObject _ConnectedVertex)
    {
        m_Property = _Property;
        m_ConnectedVertex = _ConnectedVertex;
    }
}