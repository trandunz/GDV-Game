using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_CrewMate : MonoBehaviour
{
    protected enum MATESTATE
    {
        UNASSIGNED = 0,

        IDLE,
        ONROUTE,
        DOING
    }
    protected enum MOOD
    {
        UNASSIGNED = 0,

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
    [SerializeField] protected MATESTATE m_MateState = MATESTATE.UNASSIGNED;
    [SerializeField] protected MOOD m_Mood = MOOD.UNASSIGNED;

    protected Animator m_Animator;

    void Start()
    {
        if (gameObject.GetComponent<Animator>())
        {
            m_Animator = gameObject.GetComponent<Animator>();
        }

        m_Mood = MOOD.CONTENT;
        m_MateState = MATESTATE.IDLE;
    }

    void Update()
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


    }
}
