using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Furin : MonoBehaviour
{
    public float hp = 500;
	public GameObject dieEffect;


	private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.CompareTag("Player_bullet")) {
            float att = collision.gameObject.GetComponent<Bullet_info>().att;
            GetDamage(att);
            //Destroy(collision.gameObject);
        }
    }

    private void GetDamage(float att)
    {
        hp -= att;
        if (hp <= 0)
        {
			GameObject a = Instantiate(dieEffect, transform.position, Quaternion.identity);
			Destroy(a, 0.6f);
			KYS_GameManager.isFurin_die = true;
            ITEM_MANAGER.instance.GetItem(transform.position, Quaternion.identity);
            //Debug.Log("furin die !!!!!!!!!!! " + KYS_GameManager.isFurin_die);
            Destroy(gameObject);
        }
    }

}
