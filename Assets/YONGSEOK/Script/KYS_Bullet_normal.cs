using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Bullet_normal : MonoBehaviour
{
    //�븻�� �ٶ󺸴� �������θ� ���ư���
    float speed = 15f;
    
    private void Update()
    {
         transform.Translate(transform.up * speed * Time.deltaTime);
    }
}
