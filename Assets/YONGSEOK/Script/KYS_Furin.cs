using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Furin : MonoBehaviour
{
    public float hp = 500;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.CompareTag("Player_bullet")) {
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
            GameManager.isFurin_die = true;
            Destroy(gameObject);
        }
    }

}
