using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Dowoon : Bullet_info
{

    Vector3 Dir = Vector3.zero;
    public float bullet_Speed;

    public bool b_localDirection = false;
    // Start is called before the first frame update
    private void OnEnable()
    {
       // Invoke("DestroySelf", 5f);
    }
    // Update is called once per frame
    public virtual void  Update()
    {
        Fly();
        
    }

    public virtual void Fly()
    {
        if (b_localDirection)
            transform.Translate(Vector2.right * bullet_Speed * Time.deltaTime);
        else
        {
            if (Dir != Vector3.zero)
                transform.Translate(Dir * bullet_Speed * Time.deltaTime);
            else
                transform.Translate(new Vector3(0, 1, 0) * bullet_Speed * Time.deltaTime);

        }

    }

    public void SetDirection(Vector3 _Dir)
    { 
        Dir = _Dir;
    }


    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
			Destroy(collision.gameObject);
			//DestroySelf();
        }

        

    }

  public void DestroySelf()
    {
        ObjectPool_Dowoon.ReturnBullet(this);
    }


   public IEnumerator StopBullet(float t)
    {

        yield return new WaitForSeconds(t);

        bullet_Speed = 1.0f;


        while(bullet_Speed > 0)
        {
            bullet_Speed -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

    }

}
