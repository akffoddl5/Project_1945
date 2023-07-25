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
    void CreateBullte() //적 총알 생성
    {
        
            Instantiate(Bullet, ms.position, Quaternion.identity);
            Invoke("CreateBullte", 1f);
        


    }
    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime); //speed 만큼 밑으로 움직이기

    }

    private void OnBecameInvisible() //화면에 안보이면 삭제
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hp -= 1; //나중에 bullet_info에서 공격력 가져오고 적용시키기
        Destroy(collision.gameObject);
        Debug.Log(Hp);
        if(Hp <= 0)
        {
            Instantiate(Effect, ms.position, Quaternion.identity);
            Destroy(gameObject);
            
        }
        
    }


}
