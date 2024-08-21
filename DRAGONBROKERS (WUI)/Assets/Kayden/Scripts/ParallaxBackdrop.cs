using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackdrop : MonoBehaviour
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
}
