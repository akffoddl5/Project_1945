using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class June_PlayerShooting : MonoBehaviour
{
    public GameObject bullet = null; //�̻��� 
    public Transform pos = null; //�̻��� �߻�
    public float FireSpeed = 0.2f;
    public GameObject Lazer;
    public float PlayerDamage ;

    public Image PlayerCharg;
    public GameObject Spell;

    Image cutimage;


    public float activationTime =4f;

    private float timePressed = 0f;
    private bool isZKeyPressed = false;

    
    void Start()
    {
        cutimage = transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>();

        PlayerDamage = 1;
        StartCoroutine(AutoFire());
        PlayerCharg = GameObject.Find("ChargeMagic").GetComponent<Image>();
    }
    private void OnEnable()
    {
       // StartCoroutine(AutoFire());
    }

    int CrowCount = 0;

  
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(true);
            

            Color c = cutimage.color; // �ƾ� �÷�
            c.a = 0.8f;
            cutimage.color = c;
            StartCoroutine(SpellShoot());
            
            

            Instantiate(Spell,new Vector3(-2.5f,-7f,0), Quaternion.identity);
            
        }
        else
    //        Time.timeScale = 1;


        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            
            var _bullet =Instantiate(bullet, pos.position, Quaternion.identity); 
            _bullet.GetComponent<Bullet_info>().att = PlayerDamage;
            

            isZKeyPressed = true;
            timePressed = 0f;
        }

        if (isZKeyPressed && Input.GetKey(KeyCode.Z))
        {
            PlayerCharg.fillAmount += 0.005f; //������ ���� �ӵ�
            timePressed += Time.deltaTime;
        }
        if (isZKeyPressed && Input.GetKeyUp(KeyCode.Z))
        {
            PlayerCharg.fillAmount = 0; //������ �ʱ�ȭ

            isZKeyPressed = false;
            // Z Ű�� ������ �ִ� �ð��� activationTime���� ũ�� ���� ������Ʈ ����
            if (timePressed >= activationTime)
            {
                CrowCount++;
                CreateCrow();
            }
        }

    }

    private void CreateCrow()
    {
        transform.GetChild(CrowCount+2).gameObject.SetActive(true);
    }


    
    IEnumerator SpellShoot()
    {
        
        Time.timeScale = 0;

        for (int i = 0; i < 10; i++)
       {
          
            
           transform.GetChild(0).transform.GetChild(1).transform.position += new Vector3(80, 0, 0);
           transform.GetChild(0).transform.GetChild(2).transform.position += new Vector3(110, -110, 0);
        yield return new WaitForSecondsRealtime(0.01f);

       }
       
        yield return new WaitForSecondsRealtime(1f);
        
        transform.GetChild(0).transform.GetChild(1).transform.position = new Vector3(-810, 0, 0);
        transform.GetChild(0).transform.GetChild(2).transform.position += new Vector3(-1100, 1100, 0);
        Color c = cutimage.color;
        c.a = 0;
        cutimage.color = c;
        //yield return StartCoroutine(); //�޸� �صα�

        Time.timeScale = 1;
    }



    IEnumerator AutoFire()
    {
        for (; ; )
        {
            if (Input.GetKey(KeyCode.X) == true)
            {
                var _bullet = Instantiate(bullet, pos.position, Quaternion.identity);
                _bullet.GetComponent<Bullet_info>().att = PlayerDamage;

            }
            yield return new WaitForSeconds(FireSpeed);

        }
    }


    
}
