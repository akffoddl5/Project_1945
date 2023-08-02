using System.Collections.Generic;
using System.Collections;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class June_BossMovement : MonoBehaviour
{
    private bool CarryBoss = true; //������ �ʿ� ���� �� false

    public Image BossHpBar; //���� hp
    float BossOriginHp; //������ hp��������
    float BossHp; //�ǽð����� ����hp
    public Image BossHpBar_Prefab;

    public float moveSpeed = 3f; // ���� �̵� �ӵ�
    public float minX, maxX, minY, maxY; // ���� �̵� ����

    private bool isMoving = true; //������ ������ üũ
    private Vector3 targetPosition;


    private bool isbossTalk;

    void Start()
    {
        BossHpBar = GameObject.Find("BossEnergy").GetComponent<Image>();

        BossOriginHp = gameObject.GetComponent<June_Enemy>().Hp;

        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().enabled = false;//����� ���� ���� bgm on

        isbossTalk = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = false; //ƨ�״� ������� �浹 ����
        gameObject.GetComponent<June_BossBullet>().enabled = false;

        StartCoroutine("BossSpawn"); //���� �� ������ �鿩����
        Invoke("Stop", 1);

        targetPosition = GetRandomPosition();
    }

    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private Vector3 GetRandomPosition()
    {
        // ������ �̵� ������ ������ ��ġ�� ��ȯ
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

        // ���� �ð����� ���ο� ��ġ�� �̵�
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }
        
        if(BossHp <= BossOriginHp /2)
        {
            showBossTxt();
        }


    }
    private void FixedUpdate()
    {
      
    }



    void showBossTxt()
    {
        for (int i = 0; i < 10; i++)
        {
            transform.GetChild(1).transform.GetChild(0).transform.position += new Vector3(110, 0, 0);

        }
    }

    void Stop()
    {
        CarryBoss = false; //�ڷ�ƾ�� while�� ���߱�
        //���� ����, �浹 Ȱ��
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        StopCoroutine(BossSpawn());



        //���� ������Ʈ������ ���� �Ҹ� �ּ�, playertalk�� �ּ� Ǯ��
        // gameObject.GetComponent<June_BossBullet>().enabled = true;
        gameObject.GetComponent<June_PlayerBossTalk>().enabled = true;


    }
    IEnumerator BossSpawn()
    {


        while (CarryBoss)
        {

            yield return new WaitForSeconds(0.01f); //����
            GameObject.Find("BossEnergy").transform.Translate(0, -100 * Time.deltaTime, 0);
            gameObject.transform.Translate(0, -2 * Time.deltaTime, 0); //���� ���� ��ġ�κ��� ������ ���� ����.

        }
        yield return new WaitForSeconds(0.5f);
    }


   

}
