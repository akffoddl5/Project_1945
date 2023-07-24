using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Enemy_Bullet_normal : MonoBehaviour
{
    //�븻�� �ٶ󺸴� �������θ� ���ư���
    [SerializeField]
    float speed = 20f;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        //Debug.Log(transform.up + " " + transform.forward + " " + transform.right);
        //transform.Translate(transform.up * speed * Time.deltaTime);
    }

    public void Shoot(Vector2 dir)
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
