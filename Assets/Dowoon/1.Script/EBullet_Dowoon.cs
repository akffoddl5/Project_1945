using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EBullet_Dowoon : MonoBehaviour
{
    public Vector3 dir;
    public float bulletSpeed = 14.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * 14.0f *  Time.deltaTime);
    }
}
