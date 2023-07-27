using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dowoon : MonoBehaviour
{
    int hp = 4;
    SpriteRenderer renderer;
    public bool isOpen;
    public bool isAttackAble = false;
    int onHitCount = 0;

    public Vector3 goalPos;
    public bool isArrive = false;

    public GameObject bulletPrefab;
    public GameObject Panel;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        StartCoroutine(TripleShot());
    }

    private void Update()
    {
        if(isAttackAble)
        {
            if(!isArrive)
            GoToGoalPos();

        }
    }
    void OnHitByBullet()
    {
        if(renderer != null)
        {
           
        }
    }

    public void SetGoalPos(Vector3 pos)
    {
        goalPos = pos;

    }

    void GoToGoalPos()
    {
        var dir = goalPos - transform.position;

        transform.Translate(dir.normalized * 8.5f * Time.deltaTime);

        var dist = Vector3.Distance(transform.position, goalPos);
        if(dist <= 0.1f)
        {
            isArrive = true;
            StartCoroutine(Shoot());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player_bullet"))
        {
            hp -= 2;

            if( hp <= 0)
            {
                hp = 0;
                Panel.GetComponent<EnemyPanel_Dowoon>().DestroyEnemy(this.gameObject);
            }
        }
    }

    public void Attackplayer()
    {
        var target = GameObject.FindGameObjectWithTag("Player");

        if(target != null)
        {
            var b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            var _dir = (target.transform.position - transform.position).normalized;

            b.GetComponent<EBullet_Dowoon>().dir = _dir;
        }
    }
    
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            Attackplayer();

        }
    }
}
