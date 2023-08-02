using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class June_Spell_1 : MonoBehaviour
{
    
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 오브젝트가 Player 태그를 가진 경우 무시
        if (collision.CompareTag("Player"))
        {
            return;
        }

        // 충돌한 오브젝트가 Wall 태그를 가진 경우 무시
        if (collision.CompareTag("Wall"))
        {
            return;
        }

        // ENEMY 태그를 가진 오브젝트가 아닌 경우 충돌한 오브젝트를 파괴
        if (!collision.CompareTag("ENEMY"))
        {
            Destroy(collision.gameObject);
        }

        //// ENEMY 태그를 가진 오브젝트인 경우 해당 오브젝트의 HP를 감소시킵니다.
        //if (collision.CompareTag("ENEMY"))
        //{
            //collision.gameObject.GetComponent<June_Enemy>().Hp -= gameObject.GetComponent<Bullet_info>().att + 10;
        //}
    }
    void Update()
    {
        transform.Translate(Vector2.up * 100 * Time.deltaTime);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
