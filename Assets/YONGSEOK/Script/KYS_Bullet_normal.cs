using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Bullet_normal : MonoBehaviour
{
    //노말은 바라보는 방향으로만 나아가게
    float speed = 15f;
    
    private void Update()
    {
         transform.Translate(transform.up * speed * Time.deltaTime);
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log(collision.name);
		if (collision.CompareTag("ENEMY"))
		{
			Destroy(gameObject,0.02f);
		}
	}
}
