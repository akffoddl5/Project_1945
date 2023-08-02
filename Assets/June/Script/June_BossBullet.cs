using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class June_BossBullet : MonoBehaviour
{
    private float BossHP; //���� HP     
    private float BossOrginHP; //���� ����HP     

    public float Speed = 3; //���� ������ �ӵ�

    public float Delay = 2f; //�Ѿ� �ֱ�
    public Transform ms; //�Ѿ� ������ ��

    public GameObject Bullet1; //�׳� ������ �Ѿ�
    public GameObject Bullet2; //�÷��̾� Ÿ���� �Ѿ�
    public GameObject Bullet_Boss;

    float lastShootTime;

    public int patternNum; //���� ��ȣ


    void Start()
    {
        
        BossOrginHP = gameObject.GetComponent<June_Enemy>().Hp; //�������� Hp���� ==> ���� ��ȭ�� ���ؼ�
       // Invoke("CreateBullte", Delay);

    }

    void CreateBullte()
    {
           // Invoke("CreateBullte", Delay);
    }


    void FixedUpdate()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {

        attackPattern();
        }
    }



           

        

    




    void attackPattern()
    {
        BossHP = gameObject.GetComponent<June_Enemy>().Hp;

        if (Time.time > lastShootTime + 2.0f)
        {
            lastShootTime = Time.time;
            patternNum = Random.Range(1, 4);  //���� ����
            if (BossHP <= BossOrginHP / 2) //50%ü�� ���ϸ� ������ ���� ����
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



    public float speed = 3f;

    float x, y;

  

    
    IEnumerator attack(int p) //���� �Լ���
    {
        if(p ==0)
        {
          
           
           
           GameObject bullet = Instantiate(Bullet_Boss, ms.position, Quaternion.Euler(0, 0, 180));
           June_BossBulletInfo BossBullet = bullet.GetComponent<June_BossBulletInfo>();
            for (int x = 0; x < 20; x++)
               {
                   // y = (x * MathF.Sin(MathF.Log(2.6f) * x * 10));
           
                   y = (x * MathF.Sin(x * 10));
                   BossBullet.SetDir(new Vector2(y, x));
                   BossBullet.SetSpeed(1.5f);
               
                yield return new WaitForSeconds(0.4f);

            }
           
           

        }



        if (p == 1)  //3���� ������ 5����
        {
            for (int i = 0; i < 5; i++)
            {
                Instantiate(Bullet1, ms.position, ms.transform.rotation);
                Instantiate(Bullet1, ms.position, Quaternion.Euler(0, 0, 35));
                Instantiate(Bullet1, ms.position, Quaternion.Euler(0, 0, -35));

                yield return new WaitForSeconds(0.2f);
            }
        }
        if (p == 2)  //3���� ������ 10��
        {
            for (int i = 0; i < 10; i++) 
            {
                if (GameObject.FindGameObjectWithTag("Player") != null)
                {

                    Instantiate(Bullet2, ms.position, ms.transform.rotation);
                Instantiate(Bullet2, ms.position, Quaternion.Euler(0, 0, 35));
                Instantiate(Bullet2, ms.position, Quaternion.Euler(0, 0, -35));
                }

                yield return new WaitForSeconds(0.2f);
            }
        }

        if (p == 3) //360�� ������ ����
        {

            for (int i = 0; i < 3; i++)
            {

                for (int j = 0; j < 360; j += 10)
                    Instantiate(Bullet1, ms.position, Quaternion.Euler(0, 0, j));

                yield return new WaitForSeconds(0.2f);
            }
        }

        if (p == 4) //360�� 1�� , 3���� 5�� ����
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
        if (p == 5) //360�� 5�� �÷��̾� �߰�
        {

            for (int i = 0; i < 5; i++)
            {

                for (int j = 0; j < 360; j += 10)
                    if (GameObject.FindGameObjectWithTag("Player") != null)
                        Instantiate(Bullet2, ms.position, Quaternion.Euler(0, 0, j));

                yield return new WaitForSeconds(0.2f);
            }
        }
    }


















}
