using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HRazer_collision_Jiwon : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Destroy(collision.gameObject);
		}
	}
}
