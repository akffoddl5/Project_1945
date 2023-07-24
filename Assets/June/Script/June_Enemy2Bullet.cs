using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class June_Enemy2Bullet : MonoBehaviour
{
    public GameObject Effect;

    public GameObject target;

    public float Speed = 3f;
    Vector3 dir;
    Vector3 dirNo;


    void Start()
    {

        FindPlayer();

    }
    public void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        //플레이어 찾기
        //플레이어 찾기
        dir = target.transform.position - transform.position; //a-b == a를 바라보는  b의 방향
        dirNo = dir.normalized;//방향만 잡아주기
    }

    void Update()
    {

        transform.Translate(dirNo * Speed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Instantiate(Effect, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}