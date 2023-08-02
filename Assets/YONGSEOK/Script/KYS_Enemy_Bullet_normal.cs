using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Enemy_Bullet_normal : MonoBehaviour
{
    //노말은 바라보는 방향으로만 나아가게
    [SerializeField]
    float speed = 20f;
    Vector2 dir;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        //Debug.Log(transform.up + " " + transform.forward + " " + transform.right);
        //transform.Translate(transform.up * speed * Time.deltaTime);
        //transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    public void Shoot(Vector2 dir, float speed)
    {
        this.dir = dir;
        this.speed = speed;
        InvokeRepeating("Move", 0.01f, 0.01f);
        InvokeRepeating("Move", 0.01f, 0.01f);
        InvokeRepeating("Move", 0.01f, 0.01f);

    }

    public void Move()
    {
        transform.Translate(dir * speed * Time.deltaTime);

    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			collision.gameObject.SetActive(false);
			//Destroy(collision.gameObject);
		}
	}
}