using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class June_Spell_1 : MonoBehaviour
{
    
    void Start()
    {
        

        GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>().enabled = false;
        Invoke("SelfDie", 2.5f);
        StartCoroutine(SpellSound());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(!collision.CompareTag("Player") &&
           !collision.CompareTag("Wall") &&
           !collision.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);

        }
        else if (collision.CompareTag("ENEMY"))
        {
            //collision.gameObject.GetComponent<June_Enemy>().Hp -= gameObject.GetComponent<Bullet_info>().att + 10;
        }


    
    }
    void Update()
    {
        transform.Translate(Vector2.up * 100 * Time.deltaTime);
    }


    IEnumerator SpellSound()
    {
        yield return new WaitForSecondsRealtime(2.2f);
        AudioSource Spell = GetComponent<AudioSource>();
        Spell.Play();
    }
  

    void SelfDie()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>().enabled = true;
        Destroy(gameObject);
    }
    private void OnBecameInvisible()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>().enabled = true;
        Destroy(gameObject);
    }
}
