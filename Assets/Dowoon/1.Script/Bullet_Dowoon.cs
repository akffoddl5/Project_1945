using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Dowoon : MonoBehaviour
{

     Vector3 Dir = new Vector3(0, 1, 0);
    public float bullet_Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
   public virtual void  Update()
    {
        Fly();
        Debug.Log("부모업데이트");
    }

    public virtual void Fly()
    {
        transform.Translate(Dir * bullet_Speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 _Dir)
    { 
        Dir = _Dir;
    }


}
