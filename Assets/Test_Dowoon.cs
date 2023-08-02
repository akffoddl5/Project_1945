using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Dowoon : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var local = transform.localEulerAngles;
        local.z = 45;
        transform.localEulerAngles = local;
    }
}
