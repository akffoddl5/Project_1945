using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class Enemy_windowIcon_Dowoon : Enemy_Dowoon
{
    public Transform[] bullet_Genrator;
    public GameObject[] bullet_Prefab;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    
    public override void Start_ShotCoroutine()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {
        int bulletCount = bullet_Prefab.Length;
        while(true)
        {
            GameObject target;
            if (isTarget(out target))
            {

                for (int i = 0; i < bullet_Genrator.Length; ++i)
                {
                    var b = Instantiate(bullet_Prefab[i], bullet_Genrator[i].position, Quaternion.identity).GetComponent<Bullet_Dowoon>();

                    var _dir = (target.transform.position - bullet_Genrator[i].position).normalized;
                    b.SetDirection(_dir);

                    switch (i)
                    {
                        case 0:
                            b.bullet_Speed = 17.0f;
                            break;
                        case 1:
                            b.bullet_Speed = 8.5f;
                            break;
                        case 2:
                            b.bullet_Speed = 11.5f;
                            break;
                        case 3:
                            b.bullet_Speed = 19.0f;
                            break;
                    }

                    yield return new WaitForSeconds(0.1f);
                }

                yield return new WaitForSeconds(0.5f);
            }
           
        }
        
    }
}
