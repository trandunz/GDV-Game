using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
