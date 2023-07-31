using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class June_Enemy : MonoBehaviour
{
    GameObject Obj;

    

    public float Hp ;
    public float speed = 1;
    public GameObject Effect;


    //public float Delay = 1f;
    public Transform ms;
    public GameObject Bullet;


    void Start()
    {
        Invoke("CreateBullte", 1f);

    }
    void CreateBullte() //�� �Ѿ� ����
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Instantiate(Bullet, ms.position, Quaternion.identity);
            Invoke("CreateBullte", 1.4f);

        }

    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); //speed ��ŭ ������ �����̱�
    }

    private void OnBecameInvisible() //ȭ�鿡 �Ⱥ��̸� ����
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("Player_bullet")) //Player���� ����� ��
        {
        // collison   = Ʈ���� �۵���Ų �浹ü 
        //  �� collision.gameobject =  �Ѿ� 
        Hp -= collision.gameObject.GetComponent<Bullet_info>().att; //���߿� bullet_info���� ���ݷ� �������� �����Ű��
        Destroy(collision.gameObject);
        Debug.Log(Hp);
        if(Hp <= 0)
        {
            Instantiate(Effect, ms.position, Quaternion.identity);
            GameObject.Find("SpawnManager").GetComponent<June_PlayerSawn>().CountDestroy++;
            if (GameObject.Find("SpawnManager").GetComponent<June_PlayerSawn>().CountDestroy % 4 == 0)
                Instantiate(GameObject.Find("SpawnManager").GetComponent<June_PlayerSawn>().Item, ms.position, Quaternion.identity);
            Destroy(gameObject);

           
        }
            
        }
        
    }


}
