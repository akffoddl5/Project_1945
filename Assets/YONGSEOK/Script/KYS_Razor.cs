using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Razor : MonoBehaviour
{

    Transform target = null;
    Vector3 Virtual_coord = new Vector3(10,0,0);
    Vector3 my_coord, v1, v2;
    public float speed = 10f;


    private void Start()
    {
        Research();
        Debug.Log(Virtual_coord + "" + my_coord + " " + v1 + " " + v2  + " " + target.position + " " + target.name);
    }

    private void Update()
    {
        if (target != null)
        {
            v1 = Vector3.Lerp(v1, Virtual_coord, 0.1f) * Time.deltaTime * speed;
            v2 = Vector3.Lerp(v2, target.position, 0.1f) * Time.deltaTime * speed;
            transform.Translate(Vector3.Lerp(v1, v2, 0.1f) * Time.deltaTime * speed) ;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void Research()
    {
        v1 = transform.position;
        v2 = Virtual_coord;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 9f);
        List<GameObject> enemy_list = new List<GameObject>();
        List<GameObject> coord_list = new List<GameObject>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("ENEMY"))
            {
                enemy_list.Add(colliders[i].gameObject);
                //Debug.Log(colliders[i].name);
            }
        }

        target = enemy_list[0].transform;
    }
}
