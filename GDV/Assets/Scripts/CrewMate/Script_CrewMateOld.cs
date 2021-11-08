using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CrewMateOld : MonoBehaviour
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

    [SerializeField] protected string m_Name = "Default";
    [SerializeField] protected float m_Health = 100.0f;
    [SerializeField] protected float m_Hunger = 100.0f;
    [SerializeField] protected bool m_IsSelected = false;
    [SerializeField] protected MATESTATE m_MateState;
    [SerializeField] protected MOOD m_Mood;

    [SerializeField] protected LayerMask m_GroundLayer;
    [SerializeField] protected float m_Speed;
    [SerializeField] private Transform TargetPosition;
    [SerializeField] protected float m_JumpPower;

    //[SerializeField] protected float m_NextWaypointDistance;
    //[SerializeField] protected float m_TargetTriggerDistance;
    //protected Path m_Path;
    //protected int m_CurrentWaypoint;
    //protected Seeker m_Seeker;

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

        //m_Seeker = GetComponent<Seeker>();
        TargetPosition.parent = null;
        //InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    //void UpdatePath()
    //{
    //    if (m_Seeker.IsDone() && TargetPosition)
    //    {
    //        m_Seeker.StartPath(m_RigidBody.position, TargetPosition.position, OnPathComplete);
    //    }
    //}

    //void OnPathComplete(Path _Path)
    //{
    //    if (!_Path.error)
    //    {
    //        m_Path = _Path;
    //        m_CurrentWaypoint = 0;
    //    }
    //}

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

        //switch (m_MateState)
        //{
        //    case MATESTATE.ONROUTE:
        //        //Move character to TargetPosition
        //        
        //        
        //        //if (m_Path != null && TargetPosition)
        //        //{
        //        //    Vector2 MovementDirection = ((Vector2)m_Path.vectorPath[m_CurrentWaypoint] - m_RigidBody.position).normalized;
        //        //
        //        //    //Horizontal Movement
        //        //    Vector2 MovementVector = Vector2.zero;
        //        //    MovementVector.x = Mathf.Sign(MovementDirection.x) * m_Speed;
        //        //
        //        //    //Vertical Movement
        //        //    MovementVector.y = m_RigidBody.velocity.y;
        //        //
        //        //    //Move the character
        //        //    m_RigidBody.velocity = MovementVector;
        //        //
        //        //    //If the character is at the target
        //        //    if (Vector2.Distance(transform.position, TargetPosition.position) < m_TargetTriggerDistance)
        //        //    {
        //        //        m_MateState = MATESTATE.DOING;
        //        //    }
        //        //
        //        //    float Distance = Vector2.Distance(transform.position, m_Path.vectorPath[m_CurrentWaypoint]);
        //        //    if (Distance < m_NextWaypointDistance) { m_CurrentWaypoint++; }
        //        //}
        //        break;
        //    //case MATESTATE.DOING:
        //    //
        //    //    break;
        //}
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(transform.position + new Vector3(0, -0.3f, 0), 0.25f, m_GroundLayer);
    }
}