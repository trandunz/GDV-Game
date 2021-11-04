using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingController : MonoBehaviour
{
    public bool m_ShowGraph;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //Draw Verticies
        if (m_ShowGraph)
        {
            for (int VertexIndex = 0; VertexIndex < transform.childCount; VertexIndex++)
            {
                Transform ChildGameObject = transform.GetChild(VertexIndex);
                if (ChildGameObject.GetComponent<PathfindingVertex>())
                {
                    Gizmos.DrawWireSphere
                    (
                        ChildGameObject.transform.position,
                        ChildGameObject.GetComponent<PathfindingVertex>().m_VertexTriggerRadius
                    );
                }
            }
        }
    }
}
