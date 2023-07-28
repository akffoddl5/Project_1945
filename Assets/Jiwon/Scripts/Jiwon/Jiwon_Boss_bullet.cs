using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiwon_Boss_bullet : MonoBehaviour
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
}
