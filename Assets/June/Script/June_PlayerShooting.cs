using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class June_PlayerShooting : MonoBehaviour
{
    public GameObject bullet = null; //미사일 
    public Transform pos = null; //미사일 발사
    public float FireSpeed = 0.2f;
    public GameObject Lazer;
    public float PlayerDamage ;



    public GameObject ChatgeShoot; 
    public float activationTime = 1.5f;

    private float timePressed = 0f;
    private bool isZKeyPressed = false;

    void Start()
    {
        PlayerDamage = 1;
        StartCoroutine(AutoFire());
    
    }
    private void OnEnable()
    {
       // StartCoroutine(AutoFire());
    }

 

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            
            var _bullet =Instantiate(bullet, pos.position, Quaternion.identity); 
            _bullet.GetComponent<Bullet_info>().att = PlayerDamage;

            isZKeyPressed = true;
            timePressed = 0f;
        }

        if (isZKeyPressed && Input.GetKey(KeyCode.Z))
        {
            timePressed += Time.deltaTime;
        }
        if (isZKeyPressed && Input.GetKeyUp(KeyCode.Z))
        {
            isZKeyPressed = false;
            // Z 키를 누르고 있던 시간이 activationTime보다 크면 게임 오브젝트 생성
            if (timePressed >= activationTime)
            {
                CreateGameObject();
            }
        }

    }

    private void CreateGameObject()
    {
        // gameObjectToCreate 프리팹을 현재 플레이어 위치에 생성
        var _bullet = Instantiate(ChatgeShoot, pos.position, Quaternion.identity);
        _bullet.GetComponent<Bullet_info>().att = PlayerDamage;
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
