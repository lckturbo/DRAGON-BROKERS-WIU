using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public LineRenderer[] lineRenderers;
    public Transform[] stripPositions;
    public Transform center;
    public Transform idlePosition;

    public Vector3 currentPosition;

    public float maxLength;

    public float bottomBoundary;

    bool isMouseDown;

    public GameObject potionPrefab;

    public float potionPositionOffset;

    Rigidbody2D potion;
    Collider2D potionCollider;

    public float force;


    void Start()
    {
        lineRenderers[0].positionCount = 2;
        lineRenderers[1].positionCount = 2;
        lineRenderers[0].SetPosition(0, stripPositions[0].position);
        lineRenderers[1].SetPosition(0, stripPositions[1].position);

        CreatePotion();
    }

    void CreatePotion()
    {
        potion = Instantiate(potionPrefab).GetComponent<Rigidbody2D>();
        potionCollider = potion.GetComponent<Collider2D>();
        potionCollider.enabled = false;

        potion.isKinematic = true;

        ResetStrips();
    }

    void Update()
    {
        if (isMouseDown)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 10;

            currentPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            currentPosition = center.position + Vector3.ClampMagnitude(currentPosition - center.position, maxLength);

            currentPosition = ClampBoundary(currentPosition);

            SetStrips(currentPosition);

            if (potionCollider)
            {
                potionCollider.enabled = true;
            }
        }
        else
        {
            ResetStrips();
        }
    }

    private void OnMouseDown()
    {
        isMouseDown = true;
    }

    private void OnMouseUp()
    {
        isMouseDown = false;
        Shoot();
    }

    void Shoot()
    {
        potion.isKinematic = false;
        Vector3 potionForce = (currentPosition - center.position) * force * -1;
        potion.velocity = potionForce;

        potion = null;
        potionCollider = null;
        // invoke the "CreatePotion" function after a 2-second delay
        Invoke("CreatePotion", 2);
    }

    void ResetStrips()
    {
        currentPosition = idlePosition.position;
        SetStrips(currentPosition);
    }

    void SetStrips(Vector3 position)
    {
        lineRenderers[0].SetPosition(1, position);
        lineRenderers[1].SetPosition(1, position);

        if (potion)
        {
            Vector3 dir = position - center.position;
            potion.transform.position = position + dir.normalized * potionPositionOffset;
            potion.transform.right = -dir.normalized;
        }
    }

    Vector3 ClampBoundary(Vector3 vector)
    {
        vector.y = Mathf.Clamp(vector.y, bottomBoundary, 1000);
        return vector;
    }
}
