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
        // �浹�� ������Ʈ�� Player �±׸� ���� ��� ����
        if (collision.CompareTag("Player"))
        {
            return;
        }

        // �浹�� ������Ʈ�� Wall �±׸� ���� ��� ����
        if (collision.CompareTag("Wall"))
        {
            return;
        }

        // ENEMY �±׸� ���� ������Ʈ�� �ƴ� ��� �浹�� ������Ʈ�� �ı�
        if (!collision.CompareTag("ENEMY"))
        {
            Destroy(collision.gameObject);
        }

        //// ENEMY �±׸� ���� ������Ʈ�� ��� �ش� ������Ʈ�� HP�� ���ҽ�ŵ�ϴ�.
        //if (collision.CompareTag("ENEMY"))
        //{
            //collision.gameObject.GetComponent<June_Enemy>().Hp -= gameObject.GetComponent<Bullet_info>().att + 10;
        //}
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
