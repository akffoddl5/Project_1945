using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chrome_Parts_Dowoon : MonoBehaviour
{

    public GameObject rotateTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(rotateTarget.transform.position,new Vector3(0, 0, 1), 0.5f);
        
    }
}
