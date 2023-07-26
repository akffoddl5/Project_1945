using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kjh_BackGround : MonoBehaviour
{
    Material myMaterial;
    public float scrollSpeed=5.0f;
    void Start()
    {
        myMaterial = GetComponent<Renderer>().material;

    }
        void Update()
    {
       float offsety = myMaterial.mainTextureOffset.y;
      Vector2 offset = new Vector2(0, offsety +scrollSpeed*Time.deltaTime);
        myMaterial.mainTextureOffset = offset;
        
    }
}
