using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CrewMate : MonoBehaviour
{
    public enum MATESTATE
    {
        IDLE,
        ONROUTE,
        DOING
    }
    public enum MOOD
    {
        INTOXICATED,
        HAPPY,
        CONTENT,
        SAD,
        DEPRESSED
    }

    protected GameManager m_GameManager;

    [SerializeField] public string m_CrewmateName = "John Doe";
    [SerializeField] public float m_Health = 100.0f;
    [SerializeField] public float m_Hunger = 100.0f;
    [SerializeField] public bool m_IsSelected = false;
    [SerializeField] public MATESTATE m_MateState;
    [SerializeField] public MOOD m_Mood;

    [SerializeField] protected LayerMask m_GroundLayer;
    [SerializeField] protected float m_Speed;

    protected Animator m_Animator;

    [SerializeField] ContactFilter2D m_BoundsContactFilter;

    void Start()
    {
        if (GetComponent<Animator>())
        {
            m_Animator = GetComponent<Animator>();
        }

        m_GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if (m_Animator)
        {
            if (m_MateState == MATESTATE.IDLE && !m_Animator.GetBool("Idle"))
            {
                m_Animator.SetBool("Idle", true);
            }
            else if (m_MateState == MATESTATE.ONROUTE && !m_Animator.GetBool("Run"))
            {
                m_Animator.SetBool("Run", true);
            }
            else if (m_MateState == MATESTATE.DOING && !m_Animator.GetBool("Interact"))
            {
                m_Animator.SetBool("Interact", true);
            }
        }

        //Push back into world boundaries
        if
        (
            transform.position.x < m_GameManager.m_WorldBoundaries[0] ||
            transform.position.y < m_GameManager.m_WorldBoundaries[1] ||
            transform.position.x > m_GameManager.m_WorldBoundaries[2] ||
            transform.position.y > m_GameManager.m_WorldBoundaries[3] ||
            CollidingWithBoundary()
        )
        {
            transform.position = m_GameManager.m_CrewmateSpawnPostion;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.3f, 0), 0.25f, m_GroundLayer);
    }

    bool CollidingWithBoundary()
    {
        List<Collider2D>CollidedObjects = new List<Collider2D>();
        transform.GetComponent<BoxCollider2D>().OverlapCollider(m_BoundsContactFilter, CollidedObjects);

        foreach (Collider2D Object in CollidedObjects)
        {
            if (Object.transform.GetComponent<GameManager>()) { return true; }
        }

        return false;
    }
}