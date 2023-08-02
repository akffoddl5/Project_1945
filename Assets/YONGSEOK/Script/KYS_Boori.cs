using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Boori : MonoBehaviour
{
    public float speed = 30f;
    Rigidbody2D rb;
    public float hp = 50;
    Vector2[] p1 = new Vector2[4] { new Vector2(-1, -1), new Vector2(-1, 1), new Vector2(1, 1), new Vector2(1, -1) };
	public GameObject dieEffect;
	public GameObject bullet_razor;
	GameObject player;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}


	private void Start()
    {
		
		//StartCoroutine(Cor_Attack());
		InvokeRepeating("Razor_init", 0.2f, 1.5f);
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log(collision.name);
		if (collision.CompareTag("Player_bullet"))
		{
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
			GameObject a =Instantiate(dieEffect, transform.position, Quaternion.identity);
			Destroy(a, 0.6f);
			Destroy(gameObject);
		}
	}

	public void Razor_init()
	{
		
		Instantiate(bullet_razor, transform.position, Quaternion.identity);
			
		
	}

	public IEnumerator Cor_Attack()
	{
		//Instantiate(bullet_razor, transform.position, Quaternion.identity);
		while (true)
		{
			Instantiate(bullet_razor, transform.position, Quaternion.identity);
			yield return new WaitForSeconds(1);
		}
	}



}
