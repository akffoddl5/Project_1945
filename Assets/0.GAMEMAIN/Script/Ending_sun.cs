using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending_sun : MonoBehaviour
{
	// Boss Reflect 관련 변수
	int randomDir = -1;
	Vector2 shootFirstDir; // 반사될 방향
	float reflectPower = 500f; // ForcedMode2D.Impulse로 하면 이렇게 큰 값으로 안 줘도 됨

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
					// 오른쪽 아래 대각선으로 날아가도록 reflectDir 초기화 
					shootFirstDir = (new Vector2(1, -1)).normalized;
					break;
				case 1:
					// 왼쪽 아래 대각선으로 날아가도록 reflectDir 초기화
					shootFirstDir = (new Vector2(-1, -1)).normalized;
					break;
			}

		}
		rb.AddForce(shootFirstDir * reflectPower);
		co_circle = StartCoroutine(CircleFire());

	}

	IEnumerator CircleFire()
	{
		float attRate = 1; // 공격 주기 1초
		int count = 20; // 발사하는 갯수
		float intervalAngle = 360 / count; // 발사체 사이의 각도
		float weightAngle = 0; // 가중되는 각도(항상같은 위치로 발사하지 않도록 설정)

		// 원형태로 방사하는 발사체 생성 (count 개수만큼)
		while (true)
		{
			//Debug.Log("발사"+gameObject.name);
			// count개 날려야 하니까 count만큼 반복
			for (int i = 0; i < count; ++i)
			{
				// 발사체 생성
				GameObject clone = Instantiate(Pref_bossBullet, transform.position, Quaternion.identity);

				// 발사체 이동 방향(각도)
				float angle = weightAngle + intervalAngle * i;
				// 발사체 이동 방향(벡터)
				// Cos(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
				float x = Mathf.Cos(angle * Mathf.PI / 180.0f);
				// Sin(각도), 라디안 단위의 각도 표현을 위해 PI/180을 곱함
				float y = Mathf.Sin(angle * Mathf.PI / 180.0f);
				// 발사체 이동 방향 설정
				clone.GetComponent<BossBullet_Jiwon>().SetDir(new Vector2(x, y));
			}
			// 발사체가 생성되는 시작 각도 설정을 위한 변수
			weightAngle += 10;

			circleCount++;
			if (circleCount >= 3)
			{
				circleCount = 0;
				yield return new WaitForSeconds(3);
			}
			else
			{
				// attackRate 시간 만큼 대기
				yield return new WaitForSeconds(attRate);
			}
		}

	}


	// 벽에 부딪히면 튕겨야 하니까 OnCollisionEnter2D
	private void OnCollisionEnter2D(Collision2D collision)
	{
		// 보스랑 벽 충돌
		if (collision.gameObject.CompareTag("Wall"))
		{
			// Vector2.Reflect(입사각, 법선)
			// 입사각: contacts[0].point - 내 위치
			// 반사각: 내 위치 - contacts[0].point를 뺀 것 == collision.contacts[0].normal(법선)이 알아서 해줌
			Vector2 inDir = collision.contacts[0].point - (Vector2)transform.position;
			var dir = Vector2.Reflect(inDir, collision.contacts[0].normal).normalized;

			rb.AddForce(dir * reflectPower);
		}
		// 플레이어와 보스 충돌
		if (collision.gameObject.CompareTag("Player"))
		{
			Destroy(collision.gameObject);
		}
	}

}
