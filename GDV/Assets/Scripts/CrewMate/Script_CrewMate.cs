using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Script_CrewMate : MonoBehaviour
{
    public enum MATESTATE
    {
        UNASSIGNED = 0,

        IDLE,
        ONROUTE,
        DOING
    }
    public enum MOOD
    {
        UNASSIGNED = 0,

        INTOXICATED,
        HAPPY,
        CONTENT,
        SAD,
        DEPRESSED
    }

    public string m_Name = "John Doe";
    public float m_Health = 100.0f;
    public float m_Hunger = 100.0f;
    public bool m_IsSelected = false;
    public MATESTATE m_MateState = MATESTATE.UNASSIGNED;
    public MOOD m_Mood = MOOD.UNASSIGNED;

    [SerializeField] protected float m_Speed;
    [SerializeField] private Transform TargetPosition;

    [SerializeField] protected float m_NextWaypointDistance;
    [SerializeField] protected Path m_Path;
    [SerializeField] protected int m_CurrentWaypoint = 0;
    protected Seeker m_Seeker;

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

        m_Mood = MOOD.CONTENT;
        m_MateState = MATESTATE.IDLE;

        m_Seeker = GetComponent<Seeker>();
        TargetPosition.parent = null;
        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    void UpdatePath()
    {
        if(m_Seeker.IsDone() && TargetPosition)
        {
            m_Seeker.StartPath(m_RigidBody.position, TargetPosition.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path _Path)
    {
        if (!_Path.error)
        {
            m_Path = _Path;
            m_CurrentWaypoint = 0;
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

        //Move character to TargetPosition
        if (m_MateState == MATESTATE.ONROUTE && m_Path != null && TargetPosition)
        {
            Vector2 MovementDirection = ((Vector2)m_Path.vectorPath[m_CurrentWaypoint] - m_RigidBody.position).normalized;

            //Horizontal Movement
            Vector2 MovementVector = Vector2.zero;
            MovementVector.x = Mathf.Sign(MovementDirection.x) * m_Speed;
            
            
            m_RigidBody.velocity = MovementVector;
            
            float Distance = Vector2.Distance(transform.position, m_Path.vectorPath[m_CurrentWaypoint]);

            if (Distance < m_NextWaypointDistance)
            {
                m_CurrentWaypoint++;
            }
        }
        //Check if there is a wall on the player's side
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);

    }
}
