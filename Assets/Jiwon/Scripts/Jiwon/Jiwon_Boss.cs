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
	bool isPhase_03; // 체력이 30% 이하가 되면 true

	// Boss Reflect 관련 변수
	int randomDir = -1;
	Vector2 shootFirstDir; // 반사될 방향
	float reflectPower = 500f; // ForcedMode2D.Impulse로 하면 이렇게 큰 값으로 안 줘도 됨

	// 패턴 코루틴
	Coroutine co_circle;
	Coroutine co_hRazer;
	Coroutine co_triple;
	Coroutine co_tornado;

	// HRazer 관련 변수
	// -3에서 1.5 사이의 값에서 레이저가 생성됨
	float minHRazerY = -4.5f, maxHRazerY=1.5f;
	float hRazer_scaleY = 1f; //레이저의 y크기

	const int WARN_COUNT = 3; // 3번 깜빡거림


	int circleCount=0;

	//float speed = 3f;

	// [붕어빵틀] 보스 총알 프리팹
	public GameObject Pref_crossRazer; // 스우 1페이즈 레이저
	public GameObject Pref_bossBullet; // fire
	public GameObject Pref_warning; // HRazer 위치 알려주는 프리팹
	public GameObject Pref_HRazer; // HRazer

	// [붕어빵] 보스 총알
	GameObject objCross;
	
	Rigidbody2D rb;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		nowHp = maxHp; // 현재 체력을 최대 체력으로 설정하기

		isPhase_02 = false;
		isPhase_03 = false;

		// 처음 Boss 생성 위치 Vec3 (0, 0, 0)
		gameObject.transform.position = Vector3.zero;

		// crossRazer 생성
		objCross = Instantiate(Pref_crossRazer);
		// objCross가 Boss의 자식으로 들어가게 만들기
		objCross.transform.parent = gameObject.transform;

		if (randomDir < 0)
		{
			randomDir = Random.Range(0, 2); // 0 <= randomDir < 2
			switch (randomDir)
			{
				case 0:
					// 오른쪽 아래 대각선으로 날아가도록 reflectDir 초기화 
					shootFirstDir = (new Vector2 (1, -1)).normalized;
					break;
				case 1:
					// 왼쪽 아래 대각선으로 날아가도록 reflectDir 초기화
					shootFirstDir = (new Vector2(-1, -1)).normalized;
					break;
			}

		}

	}

	private void Update()
	{
		// 이거 GetDamage() 만들어서 처리해주도록 바꾸고
		// Destroy같은 걸 딱 한 번만 실행하도록 하는 함수를 만들기
			// if문이 업데이트에 있으면 계속 조건이 맞는지 확인하게 돼서 

		// 체력이 70퍼가 되면 crossRazer 삭제 + 맵의 위로 이동
		if (!isPhase_02 && (nowHp <= maxHp * (70 / 100.0f)) && (nowHp > maxHp * (30 / 100.0f)))
		{
			// objCross 삭제
			// 다시 안 쓸 거니까 SetActive(false); 보다 Destroy(crossRazer);가 나을 거 같다
			if (objCross != null)
			{
				Destroy(objCross);
			}

			// 페이즈2 코루틴 시작

			// 맵 위로 이동 y값이 0->3.5로 이동하기 Lerp?
			// 보간은 0~1 사이의 값
			Vector2 destination = new Vector2(transform.position.x, 3.5f);
			transform.position = Vector2.Lerp(transform.position, destination, 0.03f);

			// dist = 내 좌표와 목적지 좌표 사이의 거리
			var dist = Vector3.Distance(transform.position, destination);

			// 목표지점과 내 좌표가 0.1보다 작아지면 if 안에 코드 실행
			if (dist <= 0.1f)
			{
				isPhase_02 = true;
				co_hRazer = StartCoroutine(HRazer());
				co_triple = StartCoroutine(TripleShot());
				co_tornado = StartCoroutine(Tornado());
			}

		}

		//체력이 30% 이하로 떨어지면 공튀기기처럼 보스 자체도 튕기면서 총알 쏘도록
		if ((nowHp <= maxHp*(30/100.0f)) && !isPhase_03)
		{
			isPhase_03 = true;

			// 페이즈2 패턴 코루틴 종료
			StopCoroutine(co_hRazer);
			StopCoroutine(co_triple);
			StopCoroutine(co_tornado);

			// reflect
			rb.AddForce(shootFirstDir * reflectPower);

			// 페이즈3 코루틴 시작
			co_circle = StartCoroutine(CircleFire());

		}
	}

	// 페이즈Phase 패턴Pattern 코루틴으로 구현하기
	IEnumerator HRazer()
	{
		while (true)
		{
			// HRazerY: HRazer가 생성될 Y좌표
			float hRazerY = Random.Range(minHRazerY, maxHRazerY);

			// HRazerY 위치에서 경고 프리팹 3번 깜빡깜빡
			Vector3 hrPosition = new Vector3(0, hRazerY, 0);
			GameObject objWarning = Instantiate(Pref_warning, hrPosition, Quaternion.identity);

			for (int i = 0; i < WARN_COUNT; i++)
			{
				// SetActive(false); (true); 3번 반복
				objWarning.SetActive(false);
				yield return new WaitForSeconds(0.1f);
				objWarning.SetActive(true);
				yield return new WaitForSeconds(0.1f);
			}
			Destroy(objWarning);

			// HRazer 발사
			// HRazer의 Scale y값을 0에서 HRazerY의 값만큼 슉하고 커지게
			//hr.transform.localScale.y = 0; <- 이렇게 하면 에러남

			GameObject objHRazer = Instantiate(Pref_HRazer, hrPosition, Quaternion.identity);
			Vector3 scaleOfHR = new Vector3(6, 1, 1); // HRazer의 사이즈
			Vector3 scaleChange = new Vector3(0, 0.2f, 0); // (6, 0, 1) += scaleChange;로 scale 변경

			// 0.1초마다 localScale.y값을 scaleChange.y만큼 증가시키기
			while (objHRazer.transform.localScale.y < scaleOfHR.y)
			{
				objHRazer.transform.localScale += scaleChange;
				yield return new WaitForSeconds(0.05f);
				// 더했는데 y값이 1을 넘게 되면 HRazer의 사이즈로 설정해주기
				if (objHRazer.transform.localScale.y >= scaleOfHR.y)
				{
					objHRazer.transform.localScale = scaleOfHR;
				}
			}

			//  0.1f 뒤 hr 오브젝트 삭제
			Destroy(objHRazer, 0.1f);

			// 0.5 초마다 위의 코드들
			yield return new WaitForSeconds(0.5f);
		}
	}
	IEnumerator TripleShot()
	{
		int CheckCount = 0;
		float randomWeightAngle = 230;

		while (true)
		{
			float attRate = 3; // 공격주기
			int countBullet = 4; // 4개 * 3방향 = 12개의 총알이 발사됨
			int countDir = 3;

			if (CheckCount == 0) randomWeightAngle = Random.Range(210f, 250f);

			float intervalAngle = 120 / countDir; // 발사체 사이의 각도

			for (int i = 0; i < countDir; i++)
			{
				GameObject b = Instantiate(Pref_bossBullet, transform.position, Quaternion.identity);

				// 발사체가 각도 구하기 x, y
				float angle = intervalAngle * i + randomWeightAngle;
				// 각도를 이용해서 x, y 좌표를 구하기
				float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // 라디안 각도 까먹었음. 다음에는 까먹지 말기
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
				clone.GetComponent<Jiwon_Boss_bullet>().SetDir(new Vector2(x, y));
			}
			// 발사체가 생성되는 시작 각도 설정을 위한 변수
			weightAngle += 10;

			circleCount++;
			if(circleCount >= 3) 
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
		if (collision.gameObject.CompareTag("Wall"))
		{
			// Vector2.Reflect(입사각, 법선)
			// 입사각: contacts[0].point - 내 위치
			// 반사각: 내 위치 - contacts[0].point를 뺀 것 == collision.contacts[0].normal(법선)이 알아서 해줌
			Vector2 inDir = collision.contacts[0].point - (Vector2)transform.position;
			var dir = Vector2.Reflect(inDir, collision.contacts[0].normal).normalized;

			rb.AddForce(dir*reflectPower);
		}
	}
	//플레이어의 총알에 닿으면 체력이 까이도록
	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (collision.CompareTag("Player_bullet"))
        {
			nowHp -= collision.gameObject.GetComponent<Bullet_info>().att;
			Destroy(collision.gameObject); // 총알 삭제

			// 부딪히다가 hp가 0이 되면 자신도 삭제
			if (nowHp <= 0)
			{
				//Dead함수 만들어서 거기에 Destroy 넣기
				Destroy(gameObject);
			}

        }
    }

}
