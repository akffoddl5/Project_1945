using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KYS_Razor3 : MonoBehaviour
{
    GameObject target = null;
    Transform my_coord;
    Vector3 Virtual_coord = new Vector3(0, -10, 0);
    Vector3 v1, v2;
    float speed = 2f;
    public float current_t = 0;
    List<Vector3> enemy_list = new List<Vector3>();
    List<GameObject> complete_list = new List<GameObject>();
    float life_time = 3f;

    private void Start()
    {
        my_coord = GameObject.FindGameObjectWithTag("Player").transform;
        Research(my_coord.position);
        //tag = "Player_bullet";

    }

    private void Update()
    {
        current_t += speed * Time.deltaTime;
        life_time -= Time.deltaTime;

        if (life_time < 0)
        {
            //Debug.Log("life time by ");
            life_time = 5f;
            tag = "Player_bullet";
            Destroy(gameObject,0.5f);
        }
        if (target != null && current_t <= 1)
        {
            //if(current_t > 0.8) gameObject.tag = "Player_bullet";
            if (current_t > 0.98)
            {
                complete_list.Add(target);
                Research(target.transform.position);
                if (target != null) current_t = 0;
                
            }


            List<Vector3> next_vector = Belzier_recursive(enemy_list, current_t);
            //Debug.Log(target + " " + enemy_list.Count);
            //transform.rotation = Quaternion.FromToRotation(transform.position, next_vector[0]);
            if (next_vector.Count > 0)
                transform.position = next_vector[0];
            else
            {
               // Debug.Log("tag 바꿈");
                tag = "Player_bullet";
                Destroy(gameObject,0.1f);
                target = null;
                //Destroy(gameObject);
            }
            //float angle = Mathf.Atan2(next_vector[0].y, next_vector[0].x) * Mathf.Rad2Deg;

            //transform.rotation = Quaternion.AngleAxis(angle - 90 + 90 + 90, Vector3.forward);
        }
        else
        {
            //Research();
            //if(target == null) Destroy(gameObject);
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
            next_list.Add(Vector3.Lerp(v1, v2,  t));
            // next_list.Add(Vector3.Lerp(v1, v2, t  * t));
            //next_list.add(Vector3.Lerp(v1, v2, t));
        }

        //Debug.Log(next_list.Count);
        if (next_list.Count <= 1)
        {
            return next_list;
        }

        //Debug.Log("count : " + next_list.Count);

        return Belzier_recursive(next_list, t);
    }

    public void Research(Vector3 start)
    {
        v1 = transform.position;
        v2 = Virtual_coord;
        target = null;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 90f);
        //List<GameObject> enemy_list = new List<GameObject>();
        List<GameObject> coord_list = new List<GameObject>();
        enemy_list.Clear();
        enemy_list.Add(start);
        //enemy_list.Add(my_coord.position);
        //enemy_list.Insert(1, my_coord.position + new Vector3(0,-2,0));
        //enemy_list.Insert(1, new Vector3(-2.8f, -5f, 0));
        //enemy_list.Insert(1, new Vector3(-2.8f, -5, 0));
        //enemy_list.Insert(1, new Vector3(2.8f, -5, 0));
        //enemy_list.Insert(1, new Vector3(2.8f, -5, 0));
        //enemy_list.Insert(1, new Vector3(2.8f, 0, 0));
        //enemy_list.Insert(1, new Vector3(2.8f, 0, 0));
        //enemy_list.Insert(1, new Vector3(-2.8f, 0, 0));
        //enemy_list.Insert(1, new Vector3(-2.8f, 0, 0));
        for (int i = 0; i < colliders.Length; i++) { 
            if (colliders[i].CompareTag("ENEMY") && !complete_list.Contains(colliders[i].gameObject))
            {
                enemy_list.Add(colliders[i].gameObject.transform.position);
                target = colliders[i].gameObject;
                //break;
                //Debug.Log(colliders[i].name);

            }
        }
        
    }
}
