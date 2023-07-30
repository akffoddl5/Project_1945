using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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

    
    public float activationTime =1f;

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

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            
            var _bullet =Instantiate(bullet, pos.position, Quaternion.identity); 
            _bullet.GetComponent<Bullet_info>().att = PlayerDamage;
            

            isZKeyPressed = true;
            timePressed = 0f;
        }

        if (isZKeyPressed && Input.GetKey(KeyCode.Z))
        {
            PlayerCharg.fillAmount += 0.002f; //������ ���� �ӵ�
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


        //// gameObjectToCreate �������� ���� �÷��̾� ��ġ�� ����
        //var _bullet = Instantiate(ChatgeShoot, pos.position, Quaternion.identity);
        //_bullet.GetComponent<Bullet_info>().att = PlayerDamage;
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
