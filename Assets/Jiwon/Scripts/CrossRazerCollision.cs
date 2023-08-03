using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossRazerCollision : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Destroy(collision.gameObject);
		}
	}
}
