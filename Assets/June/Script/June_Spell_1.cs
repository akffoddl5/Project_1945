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

        if (collision.gameObject == GameObject.FindGameObjectWithTag("Player"))
        {
            return;
        }



        if (collision.gameObject != GameObject.FindGameObjectWithTag("ENEMY"))
            Destroy(collision.gameObject);

       

        if (collision.gameObject == GameObject.FindGameObjectWithTag("ENEMY"))
            collision.gameObject.GetComponent<June_Enemy>().Hp -= gameObject.GetComponent<Bullet_info>().att+10;

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
