using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class June_BossBullet : MonoBehaviour
{
    private float BossHP; //몬스터 HP     
    private float BossOrginHP; //몬스터 원래HP     

    public float Speed = 3; //보스 움직임 속도

    public float Delay = 2f; //총알 주기
    public Transform ms; //총알 나오는 곳

    public GameObject Bullet1; //그냥 일직선 총알
    public GameObject Bullet2; //플레이어 타게팅 총알

    float lastShootTime;

    public int patternNum; //패턴 번호


    void Start()
    {
        
        BossOrginHP = gameObject.GetComponent<June_Enemy>().Hp; //오리지널 Hp저장 ==> 패턴 변화를 위해서
        Invoke("CreateBullte", Delay);

    }

    void CreateBullte()
    {
            Invoke("CreateBullte", Delay);
    }


    void  FixedUpdate()
    {
        attackPattern();
    }


    void attackPattern()
    {
        BossHP = gameObject.GetComponent<June_Enemy>().Hp;

        if (Time.time > lastShootTime + 2.0f)
        {
            lastShootTime = Time.time;
            patternNum = Random.Range(1, 4);  //패턴 랜덤
            if (BossHP <= BossOrginHP / 2) //50%체력 이하면 랜덤한 고정 패턴
            {
                StartCoroutine(attack(4));
                if(BossHP <= BossOrginHP / 5) 
                {
                    StartCoroutine (attack(5));
                }
            }
            else
                StartCoroutine(attack(patternNum));
        }
    }


    IEnumerator attack(int p)
    {
        if (p == 1)  //3방향 일직선 5번번
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(Bullet1, ms.position, ms.transform.rotation);
                Instantiate(Bullet1, ms.position, Quaternion.Euler(0, 0, 35));
                Instantiate(Bullet1, ms.position, Quaternion.Euler(0, 0, -35));

                yield return new WaitForSeconds(0.2f);
            }
        }
        if (p == 2)  //3방향 일직선 10번
        {
            for (int i = 0; i < 10; i++) 
            {
                Instantiate(Bullet2, ms.position, ms.transform.rotation);
                Instantiate(Bullet2, ms.position, Quaternion.Euler(0, 0, 35));
                Instantiate(Bullet2, ms.position, Quaternion.Euler(0, 0, -35));

                yield return new WaitForSeconds(0.2f);
            }
        }

        if (p == 3) //360도 일직선 세번
        {

            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 360; j += 10)
                    Instantiate(Bullet1, ms.position, Quaternion.Euler(0, 0, j));

                yield return new WaitForSeconds(0.2f);
            }
        }

        if (p == 4) //360도 1번 , 3방향 5번 동시
        {
            for (int j = 0; j < 360; j += 10)
            {
                yield return new WaitForSeconds(0.025f);
                Instantiate(Bullet1, ms.position, Quaternion.Euler(0, 0, j));

            }
            
            for (int j = 0; j < 360; j += 10)
            {
                yield return new WaitForSeconds(0.02f);
                Instantiate(Bullet1, ms.position, Quaternion.Euler(0, 0, j));

            }


        }
        if (p == 5) //360도 5번 플레이어 추격
        {

            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < 360; j += 10)
                    Instantiate(Bullet2, ms.position, Quaternion.Euler(0, 0, j));

                yield return new WaitForSeconds(0.2f);
            }
        }
    }

























}
