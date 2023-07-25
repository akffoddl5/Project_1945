using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Bullet_normal : MonoBehaviour
{
    //노말은 바라보는 방향으로만 나아가게
    float speed = 15f;
    
    private void Update()
    {
         transform.Translate(transform.up * speed * Time.deltaTime);
    }
}
