using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class June_Enemy : MonoBehaviour
{
    GameObject Obj;

    public List<GameObject> PlayerItem;

    public float Hp ;
    public float speed = 1;
    public GameObject Effect;


    //public float Delay = 1f;
    public Transform ms;
    public GameObject Bullet;


    void Start()
    {
        Invoke("CreateBullte", 1f);
        if (GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().current_character == Charactor.신준)
        { PlayerItem = GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().SJ_item_list; }
        if (GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().current_character == Charactor.지원)
        { PlayerItem = GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().JW_item_list; }
        if (GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().current_character == Charactor.도운)
        { PlayerItem = GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().DW_item_list; }
        if (GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().current_character == Charactor.정현)
        { PlayerItem = GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().JH_item_list; }
        if (GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().current_character == Charactor.용석)
        { PlayerItem = GameObject.Find("Fade_UI").GetComponent<ITEM_MANAGER>().YS_item_list; }

    }
    void CreateBullte() //적 총알 생성
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Instantiate(Bullet, ms.position, Quaternion.identity);
            Invoke("CreateBullte", 1.4f);

        }

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


        if (collision.gameObject.CompareTag("Player_bullet")) //Player에게 닿았을 때
        {
        // collison   = 트리거 작동시킨 충돌체 
        //  즉 collision.gameobject =  총알 
        Hp -= collision.gameObject.GetComponent<Bullet_info>().att; //나중에 bullet_info에서 공격력 가져오고 적용시키기
        Debug.Log(Hp);
        if(Hp <= 0)
        {
            Instantiate(Effect, ms.position, Quaternion.identity);
            GameObject.Find("SpawnManager").GetComponent<June_EnemySpawn>().CountDestroy++;
            if (GameObject.Find("SpawnManager").GetComponent<June_EnemySpawn>().CountDestroy % 4 == 0)
                    Instantiate(PlayerItem[0], ms.position, Quaternion.identity);
                Destroy(gameObject);

           
        }
            
        }
        
    }


}
