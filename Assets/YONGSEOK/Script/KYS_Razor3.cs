using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KYS_Razor3 : MonoBehaviour
{
    Vector3 target = Vector3.zero;
    Transform my_coord;
    Vector3 Virtual_coord = new Vector3(0, -10, 0);
    Vector3 v1, v2;
    float speed = 1f;
    public float current_t = 0;
    List<Vector3> enemy_list = new List<Vector3>();

    private void Start()
    {
        my_coord = GameObject.FindGameObjectWithTag("Player").transform;
        Research();


    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }

    private void Update()
    {
        current_t += speed * Time.deltaTime;
        if (target != Vector3.zero && current_t <= 1)
        {
            List<Vector3> tmp = new List<Vector3>();
            tmp.Add(my_coord.position);
            tmp.Add(new Vector3(0,5));
            tmp.Add(new Vector3(-2,-5));
            tmp.Add(new Vector3(4,-5));
            tmp.Add(new Vector3(0,10));
            //tmp.Add(new Vector3(-2, -35));
            //tmp.Add(new Vector3(0, 35));


            enemy_list.Insert(0, my_coord.position);
            enemy_list.Insert(1, my_coord.position + new Vector3(1,-5,0));
            enemy_list.Insert(1, my_coord.position + new Vector3(-10, -10, 0));
            enemy_list.Insert(1, my_coord.position + new Vector3(10, -5, 0));
            //enemy_list.Insert(1, my_coord.position + new Vector3(5, 3, 0));
            //enemy_list.Insert(1, new Vector3(3, 7, 0));


            List<Vector3> next_vector = Belzier_recursive(tmp, current_t);
            //Debug.Log(next_vector);
            transform.position = next_vector[0];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //3차 벨지에 예시
    public Vector3 Belzier(Vector3 P0, Vector3 P1, Vector3 P2, float t)
    {

        Vector3 M0 = Vector3.Lerp(P0, P1, t);
        Vector3 M1 = Vector3.Lerp(P1, P2, t);

        return Vector3.Lerp(M0, M1, t);

    }

    //N차 벨지에
    public List<Vector3> Belzier_recursive(List<Vector3> _points, float t)
    {
        List<Vector3> next_list = new List<Vector3>();
        for (int i = 0; i < _points.Count; i++)
        {
            if (i == _points.Count - 1) break;
            Vector3 v1 = _points[i];
            Vector3 v2 = _points[i + 1];
            next_list.Add(Vector3.Lerp(v1, v2, t));
            //next_list.add(Vector3.Lerp(v1, v2, t));
        }

        //Debug.Log(next_list.Count);
        if (next_list.Count <= 1)
        {
            return next_list;
        }

        Debug.Log("count : " + next_list.Count);

        return Belzier_recursive(next_list, t);
    }

    public void Research()
    {
        v1 = transform.position;
        v2 = Virtual_coord;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 90f);
        //List<GameObject> enemy_list = new List<GameObject>();
        List<GameObject> coord_list = new List<GameObject>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("ENEMY"))
            {
                enemy_list.Add(colliders[i].gameObject.transform.position);
                Debug.Log(colliders[i].name);
            }
        }
        target = enemy_list[enemy_list.Count-1];
    }
}
