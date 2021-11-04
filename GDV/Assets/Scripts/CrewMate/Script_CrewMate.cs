using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Script_CrewMate : MonoBehaviour
{
    protected enum MATESTATE
    {
        IDLE,
        ONROUTE,
        DOING
    }
    protected enum MOOD
    {
        INTOXICATED,
        HAPPY,
        CONTENT,
        SAD,
        DEPRESSED
    }

    [SerializeField] protected GameManager m_GameManager;

    [SerializeField] protected string m_Name = "Default";
    [SerializeField] protected float m_Health = 100.0f;
    [SerializeField] protected float m_Hunger = 100.0f;
    [SerializeField] protected bool m_IsSelected = false;
    [SerializeField] protected MATESTATE m_MateState;
    [SerializeField] protected MOOD m_Mood;

    [SerializeField] protected LayerMask m_GroundLayer;
    [SerializeField] protected float m_Speed;

    protected Rigidbody2D m_RigidBody;
    protected Animator m_Animator;

    void Start()
    {
        if (GetComponent<Rigidbody2D>())
        {
            m_RigidBody = GetComponent<Rigidbody2D>();
        }
        if (GetComponent<Animator>())
        {
            m_Animator = GetComponent<Animator>();
        }
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
            transform.position.y > m_GameManager.m_WorldBoundaries[3]
        )
        {
            transform.position = m_GameManager.m_CrewmateSpawnPostion;
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.3f, 0), 0.25f, m_GroundLayer);
    }
}