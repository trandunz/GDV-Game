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
    public enum CREWCLASS
    {
        STANDARD,
        CHEF,
        DOCTOR,
        ENGINEER,
        FARMER
    }


    protected GameManager m_GameManager;

    [SerializeField] public string m_CrewmateName = "John Doe";

    [SerializeField] public MATESTATE m_MateState;
    [SerializeField] public MOOD m_Mood;
    [SerializeField] public CREWCLASS m_CrewClass;

    [SerializeField] public float m_Energy;
    [SerializeField] public float m_Rest;
    [SerializeField] public float m_RestProgression = 0;
    [SerializeField] public float m_Health;
    [SerializeField] public float m_Hunger;

    [SerializeField] protected LayerMask m_GroundLayer;
    [SerializeField] ContactFilter2D m_BoundsContactFilter;

    protected Animator m_Animator;


    void Start()
    {
        if (GetComponent<Animator>()) { m_Animator = GetComponent<Animator>(); }
        m_GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        //If the current GameManager does not exist, find a new GameManager and place the cremates at its spawn position
        if (m_GameManager == null)
        {
            m_GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
            transform.position = m_GameManager.m_CrewmateSpawnPostion;
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

        //Adjust stats
        if (m_Rest > 0) { m_Rest -= 0.01f; } else { m_Rest = 0.0f; }
        if (m_Hunger > 0) { m_Hunger -= 0.01f; } else { m_Hunger = 0.0f; }
        if (m_Health > 100.0f) { m_Health = 100.0f; }

        if (m_Rest == 0 || m_Hunger == 0) { m_Health -= 1; }

        m_Energy = 0;
        m_Energy = (m_Health * 0.25f) + (m_Rest * 0.25f) + (m_Hunger * 0.5f);

        //Kill Crewmate
        if (m_Health <= 0) { Destroy(gameObject); }

        //Animate Crewmate
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