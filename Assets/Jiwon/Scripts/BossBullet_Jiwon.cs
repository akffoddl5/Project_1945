using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet_Jiwon : MonoBehaviour
{
	float speed = 3f;
	Rigidbody2D rb;
	Vector2 dir;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		Destroy(gameObject, 5f);
	}
	private void FixedUpdate()
	{
		rb.velocity = dir * speed;
	}
	public void SetDir(Vector2 v)
	{
		dir = v;
		//transform.Translate(v * speed * Time.deltaTime);
		//rb.velocity = v;

	}
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 플레이어와 보스 총알이 충돌했을 때
		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(collision.gameObject);
		}
	}
}
