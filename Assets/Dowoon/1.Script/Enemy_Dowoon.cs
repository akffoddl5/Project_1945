using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dowoon : MonoBehaviour
{
    SpriteRenderer renderer;
    public bool isOpen;
    public bool isAttackAble = false;
    int onHitCount = 0;

    public Vector3 goalPos;
    public bool isArrive = false; 

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(isAttackAble)
        {
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

        transform.Translate(dir.normalized * 10.5f * Time.deltaTime);

        var dist = Vector3.Distance(transform.position, goalPos);
        if(dist <= 0.2f)
        {
            isArrive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player_bullet"))
        {
            
        }
    }

}
