using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class June_BossMovement : MonoBehaviour
{
    private bool CarryBoss = true; //������ �ʿ� ���� �� false

    public Image BossHpBar; //���� hp
     float BossOriginHp; //������ hp��������
     float BossHp; //�ǽð����� ����hp
    public Image BossHpBar_Prefab;

    public float moveSpeed = 3f; // ���� �̵� �ӵ�
    public float minX , maxX , minY ,  maxY ; // ���� �̵� ����

    private bool isMoving = true; //������ ������ üũ
    private Vector3 targetPosition;


    void Start()
    {
        BossHpBar = GameObject.Find("BossEnergy").GetComponent<Image>();

        BossOriginHp = gameObject.GetComponent<June_Enemy>().Hp;

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
        // ������ �̵� ������ ������ ��ġ�� ��ȯ�մϴ�.
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector3(randomX, randomY, transform.position.z);
    }
    private void Update()
    {
        BossHp = gameObject.GetComponent<June_Enemy>().Hp;
        BossHpBar.fillAmount = BossHp/BossOriginHp;

        // StopCoroutine("playerspawn");
        if (isMoving)
        {
            MoveTowardsTarget();
        }

        // ���� �ð����� ���ο� ��ġ�� �̵��մϴ�.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = GetRandomPosition();
        }
    }
    private void FixedUpdate()
    {
        
    }

    void Stop()
    {
        CarryBoss = false; //�ڷ�ƾ�� while�� ���߱�
        //���� ����, �浹 Ȱ��
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        //gameObject.GetComponent<June_BossBullet>().enabled = true;
        StopCoroutine(BossSpawn());
        gameObject.GetComponent<June_PlayerBossTalk>().enabled = true;

    }
    IEnumerator BossSpawn()
    {


        while (CarryBoss)
        {
            
            yield return new WaitForSeconds(0.01f); //����
            GameObject.Find("BossEnergy").transform.Translate(0, -60 * Time.deltaTime, 0);
            GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerMovement>().enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerShooting>().enabled = false;

            gameObject.transform.Translate(0, -2 * Time.deltaTime, 0); //���� ���� ��ġ�κ��� ������ ���� ����.

        }
        yield return new WaitForSeconds(0.5f);
    }




}
