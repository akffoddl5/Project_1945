using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Dowoon : MonoBehaviour
{
    public  int hp = 4;
    public int maxHp = 150;
    public SpriteRenderer renderer;
    public bool isOpen;
    public bool isAttackAble = false;
    public bool isArrive = false;
    public float moveSpeed = 8.5f;

    [Header("Á×´ÂÀÌÆåÆ®")]
    public GameObject bomb_Prefab;
    public Vector3 size;

   protected float shootDelay = 0;
    int onHitCount = 0;

    public Vector3 goalPos;
   

    public GameObject bulletPrefab;
    public GameObject Panel;

    public Coroutine co_colorChange;

    public virtual void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

     //   StartCoroutine(TripleShot());
    }

    public virtual void Update()
    {
        if(!isArrive)
        GoToGoalPos();
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

            if (co_colorChange == null)
                co_colorChange = StartCoroutine(colorChange());
            else if (co_colorChange != null)
            {
                StopCoroutine(co_colorChange);

                co_colorChange = StartCoroutine(colorChange());
            }

            //if(collision.gameObject.GetComponent<Bullet_Dowoon>() !=null)
            //{
            //    collision.gameObject.GetComponent<Bullet_Dowoon>().DestroySelf();
            //}

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
            Destroy(b, 5f);
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

        if (bomb_Prefab != null)
        {
            var b =Instantiate(bomb_Prefab, transform.position, Quaternion.identity);
            b.transform.localScale = size;
        }

        Destroy(this.gameObject);
    }

   
}
