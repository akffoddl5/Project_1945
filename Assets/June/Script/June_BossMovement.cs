using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class June_BossMovement : MonoBehaviour
{
    private bool CarryBoss = true; //보스가 맵에 도착 시 false

    public Image BossHpBar;
     float BossOriginHp;
     float BossHp;
    public Image BossHpBar_Prefab;


    void Start()
    {
        BossHpBar = GameObject.Find("BossEnergy").GetComponent<Image>();

        BossOriginHp = gameObject.GetComponent<June_Enemy>().Hp;

        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        gameObject.GetComponent<June_BossBullet>().enabled = false;

        StartCoroutine("BossSpawn"); //플레이어 맵 안으로 들여오기
        Invoke("Stop", 1);
    }


    private void Update()
    {
        BossHp = gameObject.GetComponent<June_Enemy>().Hp;
        BossHpBar.fillAmount = BossHp/BossOriginHp;

        StopCoroutine("playerspawn");

    }
    private void FixedUpdate()
    {
     
    }

    void Stop()
    {
        CarryBoss = false; //코루틴속 while문 멈추기
        //보스 조작, 충돌 활성
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<June_BossBullet>().enabled = true;

    }
    IEnumerator BossSpawn()
    {


        while (CarryBoss)
        {
            //1초마다
            yield return new WaitForSeconds(0.01f); //지연
            GameObject.Find("BossEnergy").transform.Translate(0, -60 * Time.deltaTime, 0);
            gameObject.transform.Translate(0, -2 * Time.deltaTime, 0); //플레이어 생성 위치로부터 맵으로 끌고 오기.

        }
        yield return new WaitForSeconds(0.5f);
    }






    
}
