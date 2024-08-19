using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _waterMask;
    [SerializeField] private GameObject _splashParticles;

    private EdgeCollider2D _edgeColl;
    private InteractableWater _water;

    private void Awake()
    {
        _edgeColl = GetComponent<EdgeCollider2D>();
        _water = GetComponent<InteractableWater>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_waterMask.value & (1 << collision.gameObject.layer)) > 0)
        {
            Debug.Log("Object entered water trigger: " + collision.name); // Log when an object enters the trigger

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 localPos = gameObject.transform.localPosition;
                Vector2 hitObjectPos = collision.transform.position;
                Bounds hitObjectBounds = collision.bounds;

                //Vector3 spawnPos = Vector3.zero;
                //if (collision.transform.position.y >= _edgeColl.points[1].y + _edgeColl.offset.y + localPos.y)
                //{
                //    spawnPos = hitObjectPos - new Vector2(0f, hitObjectBounds.extents.y);
                //}
                //else
                //{
                //    spawnPos = hitObjectPos + new Vector2(0f, hitObjectBounds.extents.y);
                //}

                //Debug.Log("Splash spawn position: " + spawnPos); // Log the splash spawn position

                //if (_splashParticles != null)
                //{
                //    Instantiate(_splashParticles, spawnPos, Quaternion.identity);
                //}
                //else
                //{
                //    Debug.LogWarning("Splash particles are not assigned!"); // Warn if splash particles are not set
                //}

                int multiplier = 1;
                if (rb.velocity.y < 0)
                {
                    multiplier = -1;
                }

                float vel = rb.velocity.y * _water.ForceMultiplier;
                vel = Mathf.Clamp(Mathf.Abs(vel), 0f, _water.MaxForce);
                vel *= multiplier;

                Debug.Log("Calculated splash force: " + vel); // Log the calculated splash force

                _water.Splash(collision, vel);
            }
            else
            {
                Debug.LogWarning("Object does not have a Rigidbody2D: " + collision.name); // Warn if no Rigidbody2D is found
            }
        }
    }
}