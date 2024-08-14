using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBackdrop : MonoBehaviour
{
    [SerializeField] float backgroundScrollSpeed = 0.2f;
    Material myMaterial;
    Vector2 offSet;

    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(backgroundScrollSpeed, 0f);
    }

    void Update()
    {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
    }
    // DONE BY CHENGKEL
    //public Camera cam;
    //public Transform followTarget;

    //Vector2 startingPosition;

    //float startingZ;

    //Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition;

    //float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    //float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.nearClipPlane : cam.farClipPlane));

    //float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    startingPosition = transform.position;
    //    startingZ = transform.position.z;
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    Vector2 newPosition = startingPosition + camMoveSinceStart * parallaxFactor;

    //    transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    //}
}
