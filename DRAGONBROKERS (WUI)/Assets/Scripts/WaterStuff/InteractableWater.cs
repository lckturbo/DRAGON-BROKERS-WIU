using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(EdgeCollider2D))]
public class InteractableWater : MonoBehaviour
{
    public float Width = 5f;
    public float Height = 2f;
    public Color GizmoColor = Color.cyan;

    public float ForceMultiplier = 10f;  // Add this field to control the splash force multiplier
    public float MaxForce = 20f;         // Add this field to limit the maximum splash force

    // GenerateMesh method to create a mesh for the water
    public void GenerateMesh()
    {
        // Implement mesh generation logic here
        Debug.Log("Mesh generated");
    }

    // ResetEdgeCollider method to reset the edge collider based on water dimensions
    public void ResetEdgeCollider()
    {
        // Implement edge collider reset logic here
        Debug.Log("Edge collider reset");
    }

    // Splash method to apply the splash effect
    public void Splash(Collider2D collider, float force)
    {
        // Implement your splash logic here
        Debug.Log("Splash effect triggered with force: " + force);

        // Example: Apply an upward force to the object in the water
        Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }

        // Implement particle effects or other visual effects here, if desired
        if (_splashParticles != null)
        {
            Instantiate(_splashParticles, collider.transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmoColor;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0.1f));
    }

    // Serialized field for splash particles
    [SerializeField] private GameObject _splashParticles;
}
