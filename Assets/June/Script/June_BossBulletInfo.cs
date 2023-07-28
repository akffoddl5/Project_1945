using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class June_BossBulletInfo : MonoBehaviour
{
    Vector2 bulletDir;
    float bulletSpeed;
   
    public void SetDir(Vector2 dir)
    {
        bulletDir = dir;
    }
    public void SetSpeed(float speed)
    {
        bulletSpeed = speed;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(bulletDir * bulletSpeed * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);

    }
}
