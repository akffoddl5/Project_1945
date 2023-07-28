using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public class Jiwon_player : MonoBehaviour
{
	Rigidbody2D rb;

	public GameObject player_bullet;
	Transform shootTr;

	Vector3 bulletRotate = new Vector3(0, 0, 0);

	float moveSpeed = 3.0f;
	float shootcool = 0.3f;
	bool isShootPossible = false;
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

		// 0.3초마다 쏠 수 있도록 쿨타임 적용
		shootcool -= Time.deltaTime;
		if (shootcool <= 0)
		{
			shootcool = 0.3f; // shootcool 초기화
			isShootPossible = true;
		}

		// z를 눌렀는데 쏠 수 있는 상태가 되면
		if (Input.GetKeyDown(KeyCode.Z) && isShootPossible)
		{
			//Debug.Log("쏘기 전 " + " " + shootcool + " " + isShootPossible);
			var b = Instantiate(player_bullet, shootTr.position, shootTr.rotation);
			b.GetComponent<Jiwon_playerBullet>().SetDir(bulletRotate);

			isShootPossible = false;
			//Debug.Log("쏜 다음 " + " " + shootcool + " " + isShootPossible);

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
	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Monster"))
		{
			Destroy(gameObject);
		}
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 보스나 보스의 총알에 맞으면
		if (collision.CompareTag("Monster_bullet"))
		{
			// 플레이어 삭제
			Destroy(gameObject);
		}
	}

}
