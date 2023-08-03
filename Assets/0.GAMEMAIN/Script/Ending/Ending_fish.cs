using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ending_fish : MonoBehaviour
{
	float speedF = 7f;
	Rigidbody2D rb;

	void Start()
	{
		rb = transform.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		rb.velocity = Vector2.down * speedF;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Wall"))
		{
			Destroy(gameObject);
		}
	}
}
