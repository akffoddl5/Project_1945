using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_Jiwon : MonoBehaviour
{
	Rigidbody2D rb;
    float speed = 10f;

	Vector2 dir = Vector3.up;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
    {
		rb.velocity = dir * speed;
    }
	public void SetDir(Vector3 rotate)
	{
		if (rotate.z == 0) dir = Vector2.up;
		else if (rotate.z == 90) dir = Vector2.right;
		else if (rotate.z == 180) dir = Vector2.down;
		else dir = Vector2.left;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 보스에 닿으면 총알이 사라지도록
		if (collision.CompareTag("ENEMY"))
		{
			Destroy(gameObject);
		}
	}
}
