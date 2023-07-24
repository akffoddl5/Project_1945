using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEditorInternal;
using UnityEngine;

public class June_PlayerSawn : MonoBehaviour
{
    public GameObject Player ;
    private bool CarryPlayer = true; //플레이어가 맵에 도착 시 false

    void Start()
    {
        Instantiate(Player, new Vector3(0,-6,0), Quaternion.identity); //플렝이어 생성

        //플레이어 충돌, 조작 끄기
        GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>().enabled = false;  
        GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerMovement>().enabled = false;
       

        StartCoroutine("playerspawn"); //플레이어 맵 안으로 들여오기
        Invoke("Stop", 1); 
        
    }
    void Stop()
    {
        CarryPlayer = false; //코루틴속 while문 멈추기
        //플레이어 조작, 충돌 활성
        GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>().enabled = true;
        GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerMovement>().enabled = true;
        StopCoroutine("playerspawn");

    }
    void Update()
    {
      
    }

    IEnumerator playerspawn()
    {

        
        while (CarryPlayer)
        {
            //1초마다
            yield return new WaitForSeconds(0.01f); //지연
            GameObject.FindGameObjectWithTag("Player").transform.Translate(0, 3 * Time.deltaTime, 0); //플레이어 생성 위치로부터 맵으로 끌고 오기.

        }
        yield return new WaitForSeconds(0.5f);
    }


}
