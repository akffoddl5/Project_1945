using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class June_EnemyMiddleBulletSpawn : MonoBehaviour
{
    GameObject Obj;



    public float Hp;
    public float speed = 1;
    public GameObject Effect;

    public float Delay = 3f;
    public Transform ms;
    public GameObject Bullet;


    void Start()
    {
        Invoke("CreateBullte", Delay);

    }
    void CreateBullte() //�� �Ѿ� ����
    {
        for (int j = 0; j < 360; j += 30)
            Instantiate(Bullet, ms.position, Quaternion.Euler(0, 0, j));
        Invoke("CreateBullte", Delay);



    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); //speed ��ŭ ������ �����̱�

    }

    private void OnBecameInvisible() //ȭ�鿡 �Ⱥ��̸� ����
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hp -= collision.gameObject.GetComponent<Bullet_info>().att;
        Debug.Log(Hp);
        if (Hp <= 0)
        {
            Instantiate(Effect, ms.position, Quaternion.identity);
            Destroy(gameObject);

        }

    }

}
