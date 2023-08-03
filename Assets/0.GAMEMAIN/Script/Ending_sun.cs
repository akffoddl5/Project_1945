using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_sun : MonoBehaviour
{
	// Boss Reflect ���� ����
	int randomDir = -1;
	Vector2 shootFirstDir; // �ݻ�� ����
	float reflectPower = 500f; // ForcedMode2D.Impulse�� �ϸ� �̷��� ū ������ �� �൵ ��

	Coroutine co_circle;
	int circleCount = 0;

	public GameObject Pref_bossBullet; // fire

	Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		
		if (randomDir < 0)
		{
			randomDir = Random.Range(0, 2); // 0 <= randomDir < 2
			switch (randomDir)
			{
				case 0:
					// ������ �Ʒ� �밢������ ���ư����� reflectDir �ʱ�ȭ 
					shootFirstDir = (new Vector2(1, -1)).normalized;
					break;
				case 1:
					// ���� �Ʒ� �밢������ ���ư����� reflectDir �ʱ�ȭ
					shootFirstDir = (new Vector2(-1, -1)).normalized;
					break;
			}

		}
		rb.AddForce(shootFirstDir * reflectPower);
		co_circle = StartCoroutine(CircleFire());

	}

	IEnumerator CircleFire()
	{
		float attRate = 1; // ���� �ֱ� 1��
		int count = 20; // �߻��ϴ� ����
		float intervalAngle = 360 / count; // �߻�ü ������ ����
		float weightAngle = 0; // ���ߵǴ� ����(�׻��� ��ġ�� �߻����� �ʵ��� ����)

		// �����·� ����ϴ� �߻�ü ���� (count ������ŭ)
		while (true)
		{
			//Debug.Log("�߻�"+gameObject.name);
			// count�� ������ �ϴϱ� count��ŭ �ݺ�
			for (int i = 0; i < count; ++i)
			{
				// �߻�ü ����
				GameObject clone = Instantiate(Pref_bossBullet, transform.position, Quaternion.identity);

				// �߻�ü �̵� ����(����)
				float angle = weightAngle + intervalAngle * i;
				// �߻�ü �̵� ����(����)
				// Cos(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
				float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
				// Sin(����), ���� ������ ���� ǥ���� ���� PI/180�� ����
				float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
				// �߻�ü �̵� ���� ����
				clone.GetComponent<BossBullet_Jiwon>().SetDir(new Vector2(x, y));
			}
			// �߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
			weightAngle += 10;

			circleCount++;
			if (circleCount >= 3)
			{
				circleCount = 0;
				yield return new WaitForSeconds(3);
			}
			else
			{
				// attackRate �ð� ��ŭ ���
				yield return new WaitForSeconds(attRate);
			}
		}

	}


	// ���� �ε����� ƨ�ܾ� �ϴϱ� OnCollisionEnter2D
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// ������ �� �浹
		if (collision.gameObject.CompareTag("Wall"))
		{
			// Vector2.Reflect(�Ի簢, ����)
			// �Ի簢: contacts[0].point - �� ��ġ
			// �ݻ簢: �� ��ġ - contacts[0].point�� �� �� == collision.contacts[0].normal(����)�� �˾Ƽ� ����
			Vector2 inDir = collision.contacts[0].point - (Vector2)transform.position;
			var dir = Vector2.Reflect(inDir, collision.contacts[0].normal).normalized;

			rb.AddForce(dir * reflectPower);
		}
		// �÷��̾�� ���� �浹
		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(collision.gameObject);
		}
	}

}
