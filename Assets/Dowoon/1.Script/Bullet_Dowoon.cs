using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Dowoon : MonoBehaviour
{

    Vector3 Dir = Vector3.zero;
    public float bullet_Speed;

    public bool b_localDirection = false;
    // Start is called before the first frame update
    void Start()
    {
        
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


}
