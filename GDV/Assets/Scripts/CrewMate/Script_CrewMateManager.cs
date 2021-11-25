using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_CrewMateManager : MonoBehaviour
{
    public GameManager m_GameManager;

    public GameObject[] m_CrewMates;
    public GameObject m_SelectedCrewmate;
    private Vector2 m_DraggedCrewmateOffset;

    void Start()
    {
        m_CrewMates = GameObject.FindGameObjectsWithTag("CrewMate");
    }

    private void Update()
    {
        //Get m_DraggedCrewmateOffset
        m_DraggedCrewmateOffset = m_GameManager.m_Levels[m_GameManager.m_LevelIndex].GetComponent<LevelController>().GetDraggedCrewmateOffset();

        //Select Crewmate
        if (Input.GetMouseButtonDown(0))
        {
            Ray CursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] CursorHits = Physics.RaycastAll(CursorRay);
            bool FoundCrewmate = false;
            uint CursorHitIndex = 0;
            foreach (RaycastHit CursorHit in CursorHits)
            {
                if (CursorHit.transform.parent.GetComponent<Script_CrewMate>())
                {
                    FoundCrewmate = true; break;
                }
                CursorHitIndex++;
            }

            if (FoundCrewmate) { m_SelectedCrewmate = CursorHits[CursorHitIndex].transform.parent.gameObject; }
            else { m_SelectedCrewmate = null; }
        }

        //Drag Crewmate
        if (Input.GetMouseButton(0) && m_SelectedCrewmate != null)
        {
            Vector3 CursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            CursorWorldPosition.z = 0;
            m_SelectedCrewmate.GetComponent<Rigidbody2D>().velocity = new Vector2(m_SelectedCrewmate.GetComponent<Rigidbody2D>().velocity.x, 0.0f);
            m_SelectedCrewmate.transform.position = CursorWorldPosition + new Vector3(m_DraggedCrewmateOffset.x, m_DraggedCrewmateOffset.y, m_SelectedCrewmate.transform.position.z);
            m_SelectedCrewmate.transform.position = new Vector3(m_SelectedCrewmate.transform.position.x, m_SelectedCrewmate.transform.position.y, -1.0f);
        }
    }

    public void LossCondition()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
