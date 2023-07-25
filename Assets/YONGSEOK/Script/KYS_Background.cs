using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Background : MonoBehaviour
{
    Material mt = null;
    float map_speed = 1f;

    private void Awake()
    {
        mt = GetComponent<MeshRenderer>().material;
        
    }

    // Update is called once per frame
    void Update()
    {
       mt.mainTextureOffset += new Vector2 (map_speed * Time.deltaTime, 0);
    }
}
