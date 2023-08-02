using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_Jiwon : MonoBehaviour
{
	Rigidbody2D rb;

	public GameObject player_bullet;
	Transform shootTr;

	public Vector3 bulletRotate = new Vector3(0, 0, 0);

	float moveSpeed = 7.0f;

	float axisH, axisV;
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		shootTr = transform.Find("msPos");
	}

	// 입력은 Update()에서 받고, 이동은 FixedUpdate()에서 받는 게 좋음
	private void Update()
	{
		axisH = Input.GetAxis("Horizontal");
		axisV = Input.GetAxis("Vertical");

		if (Input.GetKeyDown(KeyCode.Z))
		{
			var b = Instantiate(player_bullet, shootTr.position, shootTr.rotation);
			b.GetComponent<PlayerBullet_Jiwon>().SetDir(bulletRotate);
			Destroy(b, 1f);
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			bulletRotate.z += 90;
			// 360도가 넘어가면 360 빼주기
			if (bulletRotate.z >= 360) bulletRotate.z -= 360;
			shootTr.rotation = Quaternion.Euler(bulletRotate);
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			if (bulletRotate.z <= 0) bulletRotate.z += 360;
			bulletRotate.z -= 90;
			// 0보다 작아지면 360더하기
			shootTr.rotation = Quaternion.Euler(bulletRotate);
		}
	}
	private void FixedUpdate()
	{
		rb.velocity = new Vector2 (axisH* moveSpeed, axisV* moveSpeed);

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("ENEMY"))
		{
			Destroy(gameObject);
		}
	}

}
