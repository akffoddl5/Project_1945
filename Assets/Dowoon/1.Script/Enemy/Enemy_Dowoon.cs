using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Dowoon : MonoBehaviour
{
    public  int hp = 4;
    SpriteRenderer renderer;
    public bool isOpen;
    public bool isAttackAble = false;
    public bool isArrive = false;
    public float moveSpeed = 8.5f;

   protected float shootDelay = 0;
    int onHitCount = 0;

    public Vector3 goalPos;
   

    public GameObject bulletPrefab;
    public GameObject Panel;

    private void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

     //   StartCoroutine(TripleShot());
    }

    public virtual void Update()
    {
        if(!isArrive)
        GoToGoalPos();
    }

    void OnHitByBullet()
    {
        if(renderer != null)
        {
           
        }
    }
    public virtual void GoToGoalPos()
    {
        var dir = goalPos - transform.position;

        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime);

        var dist = Vector3.Distance(transform.position, goalPos);
        if (dist <= 0.1f)
        {
            isArrive = true;
            Start_ShotCoroutine();
        }


    }

    public void SetGoalPos(Vector3 pos)
    {
        goalPos = pos;

    }



    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player_bullet"))
        {
            hp -= (int)collision.GetComponent<Bullet_info>().att;

            if( hp <= 0)
            {
                hp = 0;
                Die();
            }
        }
    }

    public virtual void Start_ShotCoroutine()
    {
        StartCoroutine(Shoot());
    }

    public virtual void Attackplayer()
    {
        GameObject target;
        if (isTarget(out target))
        {

            var b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            var _dir = (target.transform.position - transform.position).normalized;

            b.GetComponent<Bullet_Dowoon>().SetDirection(_dir);
        }
    }
    
    public bool isTarget(out GameObject _target)
    {
        var _istarget = false;

        var target = GameObject.FindGameObjectWithTag("Player");

        _istarget = target != null ? true : false;

        if (_istarget)
        {
            _target = target;

        
        }
        else
        {
            _target = null;
         
        }

        return _istarget;
    }
    protected IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            Attackplayer();

        }
    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }
}
