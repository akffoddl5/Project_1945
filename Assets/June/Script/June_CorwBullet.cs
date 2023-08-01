using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class June_CrowBullet : MonoBehaviour
{
    public GameObject target = null;
    Vector3 dir;
    Vector3 dirNo;
    public float Speed = 4.0f;
   
    void Start()
    {

    }

    void Update()
    {
        target = GameObject.FindGameObjectWithTag("ENEMY");
       
        if (target != null)
        {
            dir = target.transform.position - transform.position; //a-b == a�� �ٶ󺸴�  b�� ����
            dirNo = dir.normalized;//���⸸ ����ֱ�
        }
        transform.Translate(dirNo * Speed * Time.deltaTime);
        //.Translate(Vector3.right * Speed * Time.deltaTime);
    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

}