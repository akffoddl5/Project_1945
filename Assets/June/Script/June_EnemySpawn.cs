using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class June_EnemySpawn : MonoBehaviour
{
   

    public float Xleft = -2.5f; //맵 좌측 끝
    public float XRight = 2.5f; //맵 우측 끝

    //적 1
    public float StartTime1; //시작
    public float SpawnStop1; //스폰 끝나는 시간

    //적2
    public float StartTime2; //시작
    public float SpawnStop2; //스폰 끝나는 시간

    //엘리트 적
    public float StartTime3; //시작
    public float SpawnStop3; //스폰 끝나는 시간

    //보스
    public float BossStart; //시작
    public float BossStop; //스폰 끝나는 시간


    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject EnemyMiddle;
    public GameObject Boss;


    bool swi1 = true;
    bool swi2 = true;
    bool swi3 = true;


    void Start()
    {
        StartCoroutine("RandomSpawn1");   //몬스터 생성 코루틴
        StartCoroutine("RandomSpawn2");   //몬스터 생성 코루틴
        StartCoroutine("RandomSpawn3");   //몬스터 생성 코루틴
        
        Invoke("Stop1", SpawnStop1);// 
        Invoke("Stop2", SpawnStop2);// 
        Invoke("Stop3", SpawnStop3);// 

        Invoke("Boss1Spawn", BossStart);
    }



    void Stop1()
    {
        swi1 = false; //코루틴속 while문 멈추기
        StopCoroutine("RandomSpawn");

    }
    void Stop2()
    {
        swi2 = false; // 코루틴속 while문 멈추기
        StopCoroutine("RandomSpawn2");
        
    }
    void Stop3()
    {
        swi3 = false; // 코루틴속 while문 멈추기
        StopCoroutine("RandomSpawn3");

    }
    Vector3 B = new Vector3(0, 1.5f, 0);

    //적 1
    IEnumerator RandomSpawn1()
    {
        yield return new WaitForSeconds(StartTime1);
        while (swi1)
        {
            //1초마다
            yield return new WaitForSeconds(1);
            //x값 랜덤
            float X = Random.Range(Xleft, XRight);
            //x값 랜덤값 y값 자기자신값
            Vector3 r = new Vector3(X, transform.position.y, 0);
            //몬스터 생성
            Instantiate(Enemy1, r, Quaternion.identity);
        }
    }

    //적2
    IEnumerator RandomSpawn2()
    {
        yield return new WaitForSeconds(StartTime2);
        while (swi2) 
        {
            //1초마다
            yield return new WaitForSeconds(2);

            //x값 랜덤
            float X = Random.Range(Xleft, XRight);
            //x값 랜덤값 y값 자기자신값
            Vector3 r = new Vector3(X, transform.position.y, 0);
            //몬스터 생성
            Instantiate(Enemy2, r, Quaternion.identity);
        }
    }

    //엘리트 적
    IEnumerator RandomSpawn3()
    {
        yield return new WaitForSeconds(StartTime3);
        while (swi3) 
        {
            //1초마다
            yield return new WaitForSeconds(2);

            //x값 랜덤
            float X = Random.Range(Xleft, XRight);
            //x값 랜덤값 y값 자기자신값
            Vector3 r = new Vector3(X, transform.position.y, 0);
            //몬스터 생성
            Instantiate(EnemyMiddle, r, Quaternion.identity);
        }
    }
    public void Boss1Spawn()
    {
       // Instantiate(Boss, B, Quaternion.identity);

    }












}
