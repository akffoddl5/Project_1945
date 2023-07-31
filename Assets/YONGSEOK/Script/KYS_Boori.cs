using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Boori : MonoBehaviour
{
    public float speed = 30f;
    Rigidbody2D rb;
    public float hp = 50;
    Vector2[] p1 = new Vector2[4] { new Vector2(-1, -1), new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1) };

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        //int r = Random.Range(0, 4);
        //rb.velocity = p1[r] * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.CompareTag("Player_bullet"))
        {
            float att = collision.gameObject.GetComponent<Bullet_info>().att;
            GetDamage(att);
            Destroy(collision.gameObject);
        }
    }

    private void GetDamage(float att)
    {
        hp -= att;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }


}
