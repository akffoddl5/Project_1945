using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Folder_Dowoon : Enemy_Dowoon
{
    // Start is called before the first frame update
    void Start()
    {
        shootDelay = 2.0f;
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
            hp -= 2;

            if (hp <= 0)
            {
                hp = 0;
                Panel.GetComponent<EnemyPanel_Dowoon>().DestroyEnemy(this.gameObject);
            }
        }
    }

   
}
