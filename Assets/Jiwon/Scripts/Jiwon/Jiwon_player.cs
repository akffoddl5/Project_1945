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

	// �Է��� Update()���� �ް�, �̵��� FixedUpdate()���� �޴� �� ����
	private void Update()
	{
		axisH = Input.GetAxis("Horizontal");
		axisV = Input.GetAxis("Vertical");

		// 0.3�ʸ��� �� �� �ֵ��� ��Ÿ�� ����
		shootcool -= Time.deltaTime;
		if (shootcool <= 0)
		{
			shootcool = 0.3f; // shootcool �ʱ�ȭ
			isShootPossible = true;
		}

		// z�� �����µ� �� �� �ִ� ���°� �Ǹ�
		if (Input.GetKeyDown(KeyCode.Z) && isShootPossible)
		{
			//Debug.Log("��� �� " + " " + shootcool + " " + isShootPossible);
			var b = Instantiate(player_bullet, shootTr.position, shootTr.rotation);
			b.GetComponent<Jiwon_playerBullet>().SetDir(bulletRotate);

			isShootPossible = false;
			//Debug.Log("�� ���� " + " " + shootcool + " " + isShootPossible);

			Destroy(b, 1f);
	
		}

		if (Input.GetKeyDown(KeyCode.X))
		{
			bulletRotate.z += 90;
			// 360���� �Ѿ�� 360 ���ֱ�
			if (bulletRotate.z >= 360) bulletRotate.z -= 360;
			shootTr.rotation = Quaternion.Euler(bulletRotate);
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			if (bulletRotate.z <= 0) bulletRotate.z += 360;
			bulletRotate.z -= 90;
			// 0���� �۾����� 360���ϱ�
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
		// ������ ������ �Ѿ˿� ������
		if (collision.CompareTag("Monster_bullet"))
		{
			// �÷��̾� ����
			Destroy(gameObject);
		}
	}

}
