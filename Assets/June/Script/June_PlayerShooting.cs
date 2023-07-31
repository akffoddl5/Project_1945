using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class June_PlayerShooting : MonoBehaviour
{
    public GameObject bullet = null; //미사일 
    public Transform pos = null; //미사일 발사
    public float FireSpeed = 0.2f;
    public GameObject Lazer;
    public float PlayerDamage ;

    public Image PlayerCharg;
    public GameObject Spell;

    
    public float activationTime =4f;

    private float timePressed = 0f;
    private bool isZKeyPressed = false;

    void Start()
    {
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
            // Time.timeScale = 0;
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
            PlayerCharg.fillAmount += 0.005f; //마법진 생성 속도
            timePressed += Time.deltaTime;
        }
        if (isZKeyPressed && Input.GetKeyUp(KeyCode.Z))
        {
            PlayerCharg.fillAmount = 0; //마법진 초기화

            isZKeyPressed = false;
            // Z 키를 누르고 있던 시간이 activationTime보다 크면 게임 오브젝트 생성
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
        StartCoroutine(FadeInStart());
        for(int i = 0; i < 8; i++) 
        GameObject.Find("Canvas").transform.GetChild(6).transform.position += new Vector3(100, 0, 0);
        yield return new WaitForSecondsRealtime(1);
        GameObject.Find("Canvas").transform.GetChild(6).transform.position = new Vector3(-810, 0, 0);



        Time.timeScale = 1;
    }

    IEnumerator playerSpellShow()
    {
        GameObject.Find("Canvas").transform.GetChild(6).transform.position +=new Vector3(100, 0, 0);
        yield return new WaitForSeconds(0.1f);
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


    public IEnumerator FadeOutStart()
    {
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        for (float f = 0.8f; f > 0; f -= 0.02f)
        {
            Color c = GameObject.Find("Canvas").transform.GetChild(1).gameObject.GetComponent<Image>().color;
            c.a = f;
           

            GameObject.Find("Canvas").transform.GetChild(1).gameObject.GetComponent<Image>().color = c;
            yield return new WaitForSeconds(0);
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.SetActive(false);
         
        }
    }
    //페이드 인
    public IEnumerator FadeInStart()
    {
        GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(true);
        for (float f = 0f; f < 0.8; f += 0.02f)
        {
            Color c = GameObject.Find("Canvas").transform.GetChild(1).gameObject.GetComponent<Image>().color;
            c.a = f;
            GameObject.Find("Canvas").transform.GetChild(1).gameObject.GetComponent<Image>().color = c;
            yield return null;
        }
    }
}
