using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Folder_Dowoon : Enemy_Dowoon
{
    // Start is called before the first frame update

 
    public override void Start()
    {
        shootDelay = 2.0f;
        maxHp = 10;
        hp = 10;
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void  Update()
    {
        if (isAttackAble)
        {
            if (!isArrive)
                GoToGoalPos();

        }
    }


    
    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player_bullet"))
        {

            hp -= (int)collision.GetComponent<Bullet_info>().att;
            if (co_colorChange == null)
                co_colorChange = StartCoroutine(colorChange());
            else if (co_colorChange != null)
            {
                StopCoroutine(co_colorChange);

                co_colorChange = StartCoroutine(colorChange());
            }

            if (hp <= 0)
            {
                hp = 0;
                if(bomb_Prefab != null)
                {
                    var b = Instantiate(bomb_Prefab,transform.position,Quaternion.identity);
                    b.transform.localScale = size;

                }
                Panel.GetComponent<EnemyPanel_Dowoon>().DestroyEnemy(this.gameObject);
            }
        }
    }

    IEnumerator colorChange()
    {


        if (renderer != null)
        {
            renderer.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.15f);

        renderer.material.color = Color.white;
    }
}
