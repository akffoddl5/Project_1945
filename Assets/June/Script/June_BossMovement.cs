using System.Collections.Generic;
using System.Collections;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class June_BossMovement : MonoBehaviour
{
    private bool CarryBoss = true; //보스가 맵에 도착 시 false

    public Image BossHpBar; //보스 hp
    float BossOriginHp; //설정한 hp가져오기
    float BossHp; //실시간으로 보스hp
    public Image BossHpBar_Prefab;

    public float moveSpeed = 3f; // 보스 이동 속도
    public float minX, maxX, minY, maxY; // 보스 이동 범위

    private bool isMoving = true; //보스의 움직임 체크
    private Vector3 targetPosition;


    private bool isbossTalk5;
    private bool isbossTalk3;

    void Start()
    {
        BossHpBar = GameObject.Find("BossEnergy").GetComponent<Image>();

        BossOriginHp = gameObject.GetComponent<June_Enemy>().Hp;

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = false;//배경음 끄고 보스 bgm on

        isbossTalk5 = true;
        isbossTalk3 = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = false; //튕그는 벽들과의 충돌 막기
        gameObject.GetComponent<June_BossBullet>().enabled = false;

        StartCoroutine("BossSpawn"); //보스 맵 안으로 들여오기
        Invoke("Stop", 1);

        targetPosition = GetRandomPosition();
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private Vector3 GetRandomPosition()
    {
        // 보스가 이동 가능한 랜덤한 위치를 반환
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY, transform.position.z);
    }
    private void Update()
    {
        BossHp = gameObject.GetComponent<June_Enemy>().Hp;
        BossHpBar.fillAmount = BossHp / BossOriginHp;
            // StopCoroutine("playerspawn");
            if (isMoving)
            {
                MoveTowardsTarget();
            }

            // 일정 시간마다 새로운 위치로 이동
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                targetPosition = GetRandomPosition();
            }

       

            if (BossHpBar.fillAmount <= 0.5f && isbossTalk5)
        {
            StartCoroutine(showBossTxt5());
        }
        if (BossHpBar.fillAmount <= 0.25f && isbossTalk3)
        {
            StartCoroutine(showBossTxt3());
        }


    }
    private void FixedUpdate()
    {

    }
    
    IEnumerator BossLastPattern()
    {
        for (int i = 0; i < 10; i++)
        {
            GameObject.Find("Rotate").transform.GetChild(0).transform.GetChild(0).GetComponent<Slider>().value += 0.1f;
            yield return new WaitForSecondsRealtime(2.5f);

        }
        yield return new WaitForSecondsRealtime(2.5f);

    }
    



    IEnumerator showBossTxt5()
    {
        isbossTalk5 = false;
        AudioSource ImpSound = transform.GetChild(1).transform.GetChild(1).GetComponent<AudioSource>();
        ImpSound.Play();
        for (int i = 0; i < 22; i++)
        {
            transform.GetChild(1).transform.GetChild(1).transform.localPosition += new Vector3(50, 0, 0);
            yield return new WaitForSeconds(0f);

        }

        yield return new WaitForSeconds(1f);
        transform.GetChild(1).transform.GetChild(1).transform.localPosition = new Vector3(-1100, 638, 0);
        isbossTalk5 = false;
    }
    IEnumerator showBossTxt3()
    {
        isbossTalk3 = false;
        AudioSource ImpSound = transform.GetChild(1).transform.GetChild(0).GetComponent<AudioSource>();
        ImpSound.Play();
        for (int i = 0; i < 22; i++)
        {
            transform.GetChild(1).transform.GetChild(0).transform.localPosition += new Vector3(50, 0, 0);
            yield return new WaitForSeconds(0.01f);

        }
        yield return new WaitForSeconds(1f);
        transform.GetChild(1).transform.GetChild(0).transform.localPosition = new Vector3(-1100, 638, 0);
        isbossTalk3 = false;
    }
    void Stop()
    {
        CarryBoss = false; //코루틴속 while문 멈추기
        //보스 조작, 충돌 활성
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        StopCoroutine(BossSpawn());



        //개인 프로젝트에서는 보스 불릿 주석, playertalk는 주석 풀기
        // gameObject.GetComponent<June_BossBullet>().enabled = true;
        gameObject.GetComponent<June_PlayerBossTalk>().enabled = true;


    }
    IEnumerator BossSpawn()
    {


        while (CarryBoss)
        {

            yield return new WaitForSeconds(0.01f); //지연
            GameObject.Find("BossEnergy").transform.Translate(0, -100 * Time.deltaTime, 0);
            gameObject.transform.Translate(0, -2 * Time.deltaTime, 0); //보스 생성 위치로부터 맵으로 끌고 오기.

        }
        yield return new WaitForSeconds(0.5f);
    }

    private void OnDestroy()
    {
        Debug.Log("주금");
        UI_Manager.instance.GameClear_UI();
    }



}
