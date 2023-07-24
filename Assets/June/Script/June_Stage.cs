using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class Stage : MonoBehaviour
{
    public float scrollSpeed = 0.01f;
    Material myMaterial;
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material; //마테리얼 가져오기

    }

    void Update()
    {
        float newOffsetY = myMaterial.mainTextureOffset.y + scrollSpeed * Time.deltaTime; //스크롤
        Vector2 newOffset = new Vector2(0, newOffsetY);

        myMaterial.mainTextureOffset = newOffset;
    }
}
