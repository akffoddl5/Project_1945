using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public GameObject RotateTarget;
    public GameObject LazerTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(RotateTarget.transform.position, new Vector3(0, 1, 1), 45 * Time.deltaTime);
    }
}
