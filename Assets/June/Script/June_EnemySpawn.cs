using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class June_EnemySpawn : MonoBehaviour
{
    public GameObject[] Spawn;

   

    //보스
    public float BossStart; //시작
    public float BossStop; //스폰 끝나는 시간


    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject EnemyMiddle;
    public GameObject Boss;

    Text PlayerWords;
    Text BossWords;

    //bool swi1 = true;
    //bool swi2 = true;
    //bool swi3 = true;

    bool isBoss = false;

    void Start()
    {
        Invoke("Boss1SpawnWait", BossStart - 5f);
        Invoke("Boss1Spawn", BossStart);
    }

    public void Boss1SpawnWait()
    {
        isBoss = true;


    }
    public void Boss1Spawn()
    {
        Instantiate(Boss, new Vector3(0, 5f, 0), Quaternion.identity);
    }





    private void FixedUpdate()
    {
        if(isBoss != true) 
        {
            NormalattackPattern();
            EliteAttackPatter();
        }
        
    }
    float lastShootTime;
    float lastShootTime2;
    public int patternNum; //패턴 번호
    public int patternNum2; //패턴 번호
    public int SpawnRandNum;

   

    void EliteAttackPatter() //엘리트적 패턴
    {
        if (Time.time > lastShootTime2 + 8.0f)
        {
            lastShootTime2 = Time.time;
            patternNum2 = Random.Range(1, 3);  //패턴 랜덤
            StartCoroutine(attack(patternNum2));
        }
        IEnumerator attack(int p)
        {
            if(p ==1)
            {
            Instantiate(EnemyMiddle, Spawn[0].transform.position, Quaternion.Euler(0, 0, 30));
            Instantiate(EnemyMiddle, Spawn[2].transform.position, Quaternion.Euler(0, 0, -30));
            yield return new WaitForSeconds(0.5f);
            }
            if (p == 2)
            {
                Instantiate(EnemyMiddle, Spawn[3].transform.position, Quaternion.Euler(0, 0, 90));
                Instantiate(EnemyMiddle, Spawn[4].transform.position, Quaternion.Euler(0, 0, -90));
                yield return new WaitForSeconds(0.5f);
            }
            if(p == 3)
            {
                Instantiate(EnemyMiddle, Spawn[1].transform.position, Quaternion.Euler(0, 0, 0));
                yield return new WaitForSeconds(1f);
                Instantiate(EnemyMiddle, Spawn[2].transform.position, Quaternion.Euler(0, 0, -15));
                yield return new WaitForSeconds(1f); 
                Instantiate(EnemyMiddle, Spawn[0].transform.position, Quaternion.Euler(0, 0, 15));
                
            }
        }

    }

    void NormalattackPattern()
    {
        if (Time.time > lastShootTime + 3.0f)
        {
            lastShootTime = Time.time;
            patternNum = Random.Range(1, 5);  //패턴 랜덤
            SpawnRandNum = Random.Range(0, 8);
            StartCoroutine(attack(patternNum));
        }
        IEnumerator attack(int p)
        {

            if (p == 1)  //3방향 일직선 5번번
            {
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(Enemy1, Spawn[SpawnRandNum].transform.position, Quaternion.Euler(0,0,315));
                    Instantiate(Enemy1, Spawn[SpawnRandNum].transform.position, Quaternion.Euler(0, 0, 45));
                    yield return new WaitForSeconds(0.5f);
                }
                
            }
            if (p == 2)  //3번 자리에서 4번방향으로 유도
            {
                for (int i = 0; i < 6; i++)
                {
                    Instantiate(Enemy2, Spawn[SpawnRandNum].transform.position, Quaternion.Euler(0, 0, 90));

                    yield return new WaitForSeconds(0.5f);
                }
            }
            if (p == 3)  //7번 자리에서 2번방향으로 && 8번 자리에서 0번 방향으로
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(Enemy2, Spawn[8].transform.position, Quaternion.Euler(0, 0, -120));
                    Instantiate(Enemy2, Spawn[7].transform.position, Quaternion.Euler(0, 0, 120));

                    yield return new WaitForSeconds(0.8f);
                }
            }
            if (p == 4)  //7번 자리에서 2번방향으로 && 8번 자리에서 0번 방향으로
            {
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(Enemy2, Spawn[0].transform.position, Quaternion.Euler(0, 0, 20));
                    Instantiate(Enemy2, Spawn[2].transform.position, Quaternion.Euler(0, 0, -20));

                    yield return new WaitForSeconds(0.8f);
                }
                for (int i = 0; i< 3;i++)
                {
                    Instantiate(Enemy1, Spawn[3].transform.position, Quaternion.Euler(0, 0, 45));
                    Instantiate(Enemy1, Spawn[4].transform.position, Quaternion.Euler(0, 0, -45));
                }
            }
            if (p == 5)  //7번 자리에서 2번방향으로 && 8번 자리에서 0번 방향으로
            {
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(Enemy1, Spawn[1].transform.position, Quaternion.Euler(0, 0, 20));
                    Instantiate(Enemy1, Spawn[1].transform.position, Quaternion.Euler(0, 0, -20));

                    yield return new WaitForSeconds(0.8f);
                }
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(Enemy1, Spawn[0].transform.position, Quaternion.Euler(0, 0, 45));
                   
                }
            }


        }


    }






















}
