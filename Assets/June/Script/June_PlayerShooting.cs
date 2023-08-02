using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class June_PlayerShooting : MonoBehaviour
{
    public GameObject bullet = null; //미사일 
    public Transform pos = null; //미사일 발사
    public float FireSpeed = 0.2f;
    public GameObject Lazer;
    public float PlayerDamage ;

    public Image PlayerCharg;
    public GameObject Spell;

    Image cutimage;


    public float activationTime =4f;

    private float timePressed = 0f;
    private bool isZKeyPressed = false;




    public GameObject m_missilePrefab; // 미사일 프리팹.
    public GameObject m_target; // 도착 지점.

    [Header("유도 미사일 기능 관련")]
    public float m_speed = 2; // 미사일 속도.
    [Space(10f)]
    public float m_distanceFromStart = 6.0f; // 시작 지점을 기준으로 얼마나 꺾일지.
    public float m_distanceFromEnd = 3.0f; // 도착 지점을 기준으로 얼마나 꺾일지.
    [Space(10f)]
    public int m_shotCount = 12; // 총 몇 개 발사할건지.
    [Range(0, 1)] public float m_interval = 0.15f;
    public int m_shotCountEveryInterval = 2; // 한번에 몇 개씩 발사할건지.



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
        m_target = GameObject.FindGameObjectWithTag("ENEMY");
        if (Input.GetKeyDown(KeyCode.V))
        {
            
           GameObject missile = Instantiate(m_missilePrefab,pos.position,Quaternion.identity);
                    //missile.GetComponent<June_PlayerGuidBullet>().Init(this.gameObject.transform, m_speed, m_distanceFromStart, m_distanceFromEnd);
                    missile.GetComponent<Bullet_info>().att = PlayerDamage;

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(true);
            

            Color c = cutimage.color; // 컷씬 컬러
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
        //yield return StartCoroutine(); //메모 해두기

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
