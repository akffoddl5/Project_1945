using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Jiwon_Boss : MonoBehaviour
{
	float maxHp = 50;
	float nowHp;

	bool isPhase_02;
	bool isPhase_03; // ü���� 30% ���ϰ� �Ǹ� true

	// Boss Reflect ���� ����
	int randomDir = -1;
	Vector2 shootFirstDir; // �ݻ�� ����
	float reflectPower = 500f; // ForcedMode2D.Impulse�� �ϸ� �̷��� ū ������ �� �൵ ��

	// ���� �ڷ�ƾ
	Coroutine co_circle;
	Coroutine co_hRazer;
	Coroutine co_triple;
	Coroutine co_tornado;

	// HRazer ���� ����
	// -3���� 1.5 ������ ������ �������� ������
	float minHRazerY = -4.5f, maxHRazerY=1.5f;
	float hRazer_scaleY = 1f; //�������� yũ��

	const int WARN_COUNT = 3; // 3�� �����Ÿ�


	int circleCount=0;

	//float speed = 3f;

	// [�ؾƲ] ���� �Ѿ� ������
	public GameObject Pref_crossRazer; // ���� 1������ ������
	public GameObject Pref_bossBullet; // fire
	public GameObject Pref_warning; // HRazer ��ġ �˷��ִ� ������
	public GameObject Pref_HRazer; // HRazer

	// [�ؾ] ���� �Ѿ�
	GameObject objCross;
	
	Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		nowHp = maxHp; // ���� ü���� �ִ� ü������ �����ϱ�

		isPhase_02 = false;
		isPhase_03 = false;

		// ó�� Boss ���� ��ġ Vec3 (0, 0, 0)
		gameObject.transform.position = Vector3.zero;

		// crossRazer ����
		objCross = Instantiate(Pref_crossRazer);
		// objCross�� Boss�� �ڽ����� ���� �����
		objCross.transform.parent = gameObject.transform;

		if (randomDir < 0)
		{
			randomDir = Random.Range(0, 2); // 0 <= randomDir < 2
			switch (randomDir)
			{
				case 0:
					// ������ �Ʒ� �밢������ ���ư����� reflectDir �ʱ�ȭ 
					shootFirstDir = (new Vector2 (1, -1)).normalized;
					break;
				case 1:
					// ���� �Ʒ� �밢������ ���ư����� reflectDir �ʱ�ȭ
					shootFirstDir = (new Vector2(-1, -1)).normalized;
					break;
			}

		}

	}

	private void Update()
	{
		// �̰� GetDamage() ���� ó�����ֵ��� �ٲٰ�
		// Destroy���� �� �� �� ���� �����ϵ��� �ϴ� �Լ��� �����
			// if���� ������Ʈ�� ������ ��� ������ �´��� Ȯ���ϰ� �ż� 

		// ü���� 70�۰� �Ǹ� crossRazer ���� + ���� ���� �̵�
		if (!isPhase_02 && (nowHp <= maxHp * (70 / 100.0f)) && (nowHp > maxHp * (30 / 100.0f)))
		{
			// objCross ����
			// �ٽ� �� �� �Ŵϱ� SetActive(false); ���� Destroy(crossRazer);�� ���� �� ����
			if (objCross != null)
			{
				Destroy(objCross);
			}

			// ������2 �ڷ�ƾ ����

			// �� ���� �̵� y���� 0->3.5�� �̵��ϱ� Lerp?
			// ������ 0~1 ������ ��
			Vector2 destination = new Vector2(transform.position.x, 3.5f);
			transform.position = Vector2.Lerp(transform.position, destination, 0.03f);

			// dist = �� ��ǥ�� ������ ��ǥ ������ �Ÿ�
			var dist = Vector3.Distance(transform.position, destination);

			// ��ǥ������ �� ��ǥ�� 0.1���� �۾����� if �ȿ� �ڵ� ����
			if (dist <= 0.1f)
			{
				isPhase_02 = true;
				co_hRazer = StartCoroutine(HRazer());
				co_triple = StartCoroutine(TripleShot());
				co_tornado = StartCoroutine(Tornado());
			}

		}

		//ü���� 30% ���Ϸ� �������� ��Ƣ���ó�� ���� ��ü�� ƨ��鼭 �Ѿ� ���
		if ((nowHp <= maxHp*(30/100.0f)) && !isPhase_03)
		{
			isPhase_03 = true;

			// ������2 ���� �ڷ�ƾ ����
			StopCoroutine(co_hRazer);
			StopCoroutine(co_triple);
			StopCoroutine(co_tornado);

			// reflect
			rb.AddForce(shootFirstDir * reflectPower);

			// ������3 �ڷ�ƾ ����
			co_circle = StartCoroutine(CircleFire());

		}
	}

	// ������Phase ����Pattern �ڷ�ƾ���� �����ϱ�
	IEnumerator HRazer()
	{
		while (true)
		{
			// HRazerY: HRazer�� ������ Y��ǥ
			float hRazerY = Random.Range(minHRazerY, maxHRazerY);

			// HRazerY ��ġ���� ��� ������ 3�� ��������
			Vector3 hrPosition = new Vector3(0, hRazerY, 0);
			GameObject objWarning = Instantiate(Pref_warning, hrPosition, Quaternion.identity);

			for (int i = 0; i < WARN_COUNT; i++)
			{
				// SetActive(false); (true); 3�� �ݺ�
				objWarning.SetActive(false);
				yield return new WaitForSeconds(0.1f);
				objWarning.SetActive(true);
				yield return new WaitForSeconds(0.1f);
			}
			Destroy(objWarning);

			// HRazer �߻�
			// HRazer�� Scale y���� 0���� HRazerY�� ����ŭ ���ϰ� Ŀ����
			//hr.transform.localScale.y = 0; <- �̷��� �ϸ� ������

			GameObject objHRazer = Instantiate(Pref_HRazer, hrPosition, Quaternion.identity);
			Vector3 scaleOfHR = new Vector3(6, 1, 1); // HRazer�� ������
			Vector3 scaleChange = new Vector3(0, 0.2f, 0); // (6, 0, 1) += scaleChange;�� scale ����

			// 0.1�ʸ��� localScale.y���� scaleChange.y��ŭ ������Ű��
			while (objHRazer.transform.localScale.y < scaleOfHR.y)
			{
				objHRazer.transform.localScale += scaleChange;
				yield return new WaitForSeconds(0.05f);
				// ���ߴµ� y���� 1�� �Ѱ� �Ǹ� HRazer�� ������� �������ֱ�
				if (objHRazer.transform.localScale.y >= scaleOfHR.y)
				{
					objHRazer.transform.localScale = scaleOfHR;
				}
			}

			//  0.1f �� hr ������Ʈ ����
			Destroy(objHRazer, 0.1f);

			// 0.5 �ʸ��� ���� �ڵ��
			yield return new WaitForSeconds(0.5f);
		}
	}
	IEnumerator TripleShot()
	{
		int CheckCount = 0;
		float randomWeightAngle = 230;

		while (true)
		{
			float attRate = 3; // �����ֱ�
			int countBullet = 4; // 4�� * 3���� = 12���� �Ѿ��� �߻��
			int countDir = 3;

			if (CheckCount == 0) randomWeightAngle = Random.Range(210f, 250f);

			float intervalAngle = 120 / countDir; // �߻�ü ������ ����

			for (int i = 0; i < countDir; i++)
			{
				GameObject b = Instantiate(Pref_bossBullet, transform.position, Quaternion.identity);

				// �߻�ü�� ���� ���ϱ� x, y
				float angle = intervalAngle * i + randomWeightAngle;
				// ������ �̿��ؼ� x, y ��ǥ�� ���ϱ�
				float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // ���� ���� ��Ծ���. �������� ����� ����
				float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

				Vector2 dir = new Vector2(x, y);
				b.GetComponent<Jiwon_Boss_bullet>().SetDir(dir);
			}

			CheckCount++;
			
			if (CheckCount >= countBullet)
			{
				CheckCount = 0;
				yield return new WaitForSeconds(attRate);

			}
			else
				yield return new WaitForSeconds(0.3f);
		}
	}
	IEnumerator Tornado()
	{
		while (true)
		{
			int side = 4;
			float angle = 360.0f / side;
			int point = 10;
			int radius = 5;
			float current_angle = 0;
			float current_angle2 = 0;
			for (int i = 0; i < side; i++)
			{
				current_angle += angle;
				current_angle2 = 0;
				//if (i != 0) continue;
				for (int j = 0; j < point; j++)
				{
					current_angle2 = -1.0f * j * angle / (float)point;
					float cal_angle = current_angle + current_angle2;
					float scalar = radius * Mathf.Cos(current_angle2 * Mathf.PI / 180);
					scalar = Mathf.Abs(scalar);

					Vector2 dir = new Vector2(Mathf.Cos(cal_angle * Mathf.PI / 180), Mathf.Sin(cal_angle * Mathf.PI / 180));
					Vector3 tmp2 = new Vector3(dir.x, dir.y, 0) * scalar;

					//Debug.Log(scalar + " " + dir + " " + cal_angle + " " + current_angle2);

					GameObject tmp = Instantiate(Pref_bossBullet, transform.position + tmp2, Quaternion.identity);
					tmp.GetComponent<Jiwon_Boss_bullet>().SetDir(dir);
				}
			}
			yield return new WaitForSeconds(5f);
		}
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
				clone.GetComponent<Jiwon_Boss_bullet>().SetDir(new Vector2(x, y));
			}
			// �߻�ü�� �����Ǵ� ���� ���� ������ ���� ����
			weightAngle += 10;

			circleCount++;
			if(circleCount >= 3) 
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
		if (collision.gameObject.CompareTag("Wall"))
		{
			// Vector2.Reflect(�Ի簢, ����)
			// �Ի簢: contacts[0].point - �� ��ġ
			// �ݻ簢: �� ��ġ - contacts[0].point�� �� �� == collision.contacts[0].normal(����)�� �˾Ƽ� ����
			Vector2 inDir = collision.contacts[0].point - (Vector2)transform.position;
			var dir = Vector2.Reflect(inDir, collision.contacts[0].normal).normalized;

			rb.AddForce(dir*reflectPower);
		}
	}
	//�÷��̾��� �Ѿ˿� ������ ü���� ���̵���
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Player_bullet"))
        {
			nowHp -= collision.gameObject.GetComponent<Bullet_info>().att;
			Destroy(collision.gameObject); // �Ѿ� ����

			// �ε����ٰ� hp�� 0�� �Ǹ� �ڽŵ� ����
			if (nowHp <= 0)
			{
				//Dead�Լ� ���� �ű⿡ Destroy �ֱ�
				Destroy(gameObject);
			}

        }
    }

}
