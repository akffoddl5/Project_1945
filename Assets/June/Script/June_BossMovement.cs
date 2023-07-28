using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class June_BossMovement : MonoBehaviour
{
    private bool CarryBoss = true; //������ �ʿ� ���� �� false

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

        StartCoroutine("BossSpawn"); //�÷��̾� �� ������ �鿩����
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
        CarryBoss = false; //�ڷ�ƾ�� while�� ���߱�
        //���� ����, �浹 Ȱ��
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
        gameObject.GetComponent<June_BossBullet>().enabled = true;

    }
    IEnumerator BossSpawn()
    {


        while (CarryBoss)
        {
            //1�ʸ���
            yield return new WaitForSeconds(0.01f); //����
            GameObject.Find("BossEnergy").transform.Translate(0, -60 * Time.deltaTime, 0);
            gameObject.transform.Translate(0, -2 * Time.deltaTime, 0); //�÷��̾� ���� ��ġ�κ��� ������ ���� ����.

        }
        yield return new WaitForSeconds(0.5f);
    }






    
}
