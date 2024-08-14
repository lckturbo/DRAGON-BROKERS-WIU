using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEditor;
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

    // Method to calculate the slope (sensitivity) of the end effector's distance to the target
    // helps the arm decide how much to rotate each joint to move the hand in the right direction, getting it closer to the target with each step
    float CalculateSlope(TrawlJoint _joint)
    {
        float deltaTheta = 0.01f;

        // the distance from the end effector to the target before rotating the joint
        float distance1 = GetDistance(m_end.transform.position, GetMouseWorldPosition());

        _joint.Rotate(deltaTheta);

        // Distance from the end to the target/ start after rotating the joint
        float distance2 = GetDistance(m_end.transform.position, GetMouseWorldPosition());

        // Rotate the joint back to its original position
        _joint.Rotate(-deltaTheta);

        // Calculate and return the change in distance per change in angle)
        return (distance2 - distance1) / deltaTheta;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < m_steps; ++i)
        {
            // if the end effector is not close enough to the target
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