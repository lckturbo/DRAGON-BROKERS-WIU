using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKManager : MonoBehaviour
{
    // Root of the armature
    public TrawlJoint m_root;

    // End effector
    public TrawlJoint m_end;

    // Remove the previous target GameObject
    // public GameObject m_target;

    public float m_threshold = 0.05f;

    public float m_rate = 10.0f;

    public int m_steps = 20;

    float CalculateSlope(TrawlJoint _joint)
    {
        float deltaTheta = 0.01f;
        float distance1 = GetDistance(m_end.transform.position, GetMouseWorldPosition());

        _joint.Rotate(deltaTheta);

        float distance2 = GetDistance(m_end.transform.position, GetMouseWorldPosition());

        _joint.Rotate(-deltaTheta);

        return (distance2 - distance1) / deltaTheta;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_steps; ++i)
        {
            if (GetDistance(m_end.transform.position, GetMouseWorldPosition()) > m_threshold)
            {
                TrawlJoint current = m_root;
                while (current != null)
                {
                    float slope = CalculateSlope(current);
                    current.Rotate(-slope * m_rate);
                    current = current.GetChild();
                }
            }
        }
    }

    // Convert mouse position to world position
    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane; // Set to the distance from the camera to the object (usually 0 for 2D)
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    float GetDistance(Vector3 _point1, Vector3 _point2)
    {
        return Vector3.Distance(_point1, _point2);
    }
}