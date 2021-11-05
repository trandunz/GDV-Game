using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_2DDragCamera : MonoBehaviour
{
    [SerializeField] float m_ZoomStep = 1, m_MinCamSize = 1, m_MaxCamSize = 6;
    [SerializeField] SpriteRenderer m_BackgroundRenderer;

    #region Private Member Variables

    Vector3 m_DragOrigin;
    Camera m_MainCamera;
    float m_BackgroundMinX, m_BackgroundMaxX, m_BackgroundMinY, m_BackgroundMaxY;

    #endregion

    #region Private Member Functions

    void Start()
    {
        m_MainCamera = GetComponent<Camera>();

        m_MainCamera.orthographicSize = m_MaxCamSize;
    }

    void Awake()
    {
        m_BackgroundMinX = m_BackgroundRenderer.transform.position.x - m_BackgroundRenderer.bounds.size.x / 2;
        m_BackgroundMinY = m_BackgroundRenderer.transform.position.y - m_BackgroundRenderer.bounds.size.y / 2;

        m_BackgroundMaxX = m_BackgroundRenderer.transform.position.x + m_BackgroundRenderer.bounds.size.x / 2;
        m_BackgroundMaxY = m_BackgroundRenderer.transform.position.y + m_BackgroundRenderer.bounds.size.y / 2;
    }

    void LateUpdate()
    {
        DragCamera();
        if (Input.mouseScrollDelta.y > 0)
        {
            ZoomIn();
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            ZoomOut();
        }
    }

    void DragCamera()
    {
        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            m_DragOrigin = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {
            Vector3 Difference = m_DragOrigin - m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
            m_MainCamera.transform.position = ClampCamera(m_MainCamera.transform.position + Difference);
        }
    }

    void ZoomIn()
    {
        float newSize = m_MainCamera.orthographicSize - m_ZoomStep;
        m_MainCamera.orthographicSize = Mathf.Clamp(newSize, m_MinCamSize, m_MaxCamSize);

        m_MainCamera.transform.position = ClampCamera(m_MainCamera.transform.position);
    }

    void ZoomOut()
    {
        float newSize = m_MainCamera.orthographicSize + m_ZoomStep;
        m_MainCamera.orthographicSize = Mathf.Clamp(newSize, m_MinCamSize, m_MaxCamSize);

        m_MainCamera.transform.position = ClampCamera(m_MainCamera.transform.position);
    }

    Vector3 ClampCamera(Vector3 _targetPos)
    {
        float cameraHeight = m_MainCamera.orthographicSize;
        float cameraWidth = m_MainCamera.orthographicSize * m_MainCamera.aspect;

        float minX = m_BackgroundMinX + cameraWidth;
        float minY = m_BackgroundMinY + cameraHeight;

        float maxX = m_BackgroundMaxX - cameraWidth;
        float maxY = m_BackgroundMaxY - cameraHeight;

        float newX = Mathf.Clamp(_targetPos.x, minX, maxX);
        float newY = Mathf.Clamp(_targetPos.y, minY, maxY);

        return new Vector3(newX, newY, _targetPos.z);
    }

    #endregion
}
