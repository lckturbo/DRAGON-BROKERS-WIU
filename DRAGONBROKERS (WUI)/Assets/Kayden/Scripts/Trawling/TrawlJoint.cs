using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrawlJoint : MonoBehaviour
{
    public TrawlJoint m_child;

    public TrawlJoint GetChild()
    {
        return m_child;
    }

    public void Rotate(float _angle)
    {
        // Rotate around the z-axis (2D rotation)
        transform.Rotate(Vector3.forward * _angle);
    }
}
