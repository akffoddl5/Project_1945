//감사합니다 용석형님!!!!!!!!!!!!!
//감사합니다 용석형님!!!!!!!!!!!!!
//감사합니다 용석형님!!!!!!!!!!!!!
//감사합니다 용석형님!!!!!!!!!!!!!
//감사합니다 용석형님!!!!!!!!!!!!!

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class June_ItemMove : MonoBehaviour
{


    float valuable_x_max;
    float valuable_x_min;
    float valuable_y_max;
    float valuable_y_min;

    float current_x = 0f;
    float current_y = 0f;
    public float speed = 5f;

    //랜덤 4방향으로 이동 시작
    Rigidbody2D rb;
    Vector2[] rand_vec_arr = new Vector2[4] { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, -1), new Vector2(-1, 1) };

    void Start()
    {
        float field_size_x = Camera.main.orthographicSize * Screen.width / Screen.height;
        float field_size_y = Camera.main.orthographicSize;

        float item_size_x = transform.GetComponent<CircleCollider2D>().bounds.size.x;
        float item_size_y = transform.GetComponent<CircleCollider2D>().bounds.size.y;

        valuable_x_max = Camera.main.transform.position.x + (field_size_x - item_size_x / 2.0f);
        valuable_x_min = Camera.main.transform.position.x - (field_size_x - item_size_x / 2.0f);
        valuable_y_max = Camera.main.transform.position.y + (field_size_y - item_size_y / 2.0f);
        valuable_y_min = Camera.main.transform.position.y - (field_size_y - item_size_y / 2.0f);

        //Debug.Log(item_size_x + " " + item_size_y + " " + valuable_x_max + " " + valuable_x_min + " " + valuable_y_max + " " + valuable_y_min);

        rb = GetComponent<Rigidbody2D>();
        int ran_idx = Random.Range(0, 4);
        rb.velocity = rand_vec_arr[ran_idx] * speed;
    }


    void Update()
    {
        current_x = transform.position.x;
        current_y = transform.position.y;

        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, valuable_x_min, valuable_x_max),
            Mathf.Clamp(transform.position.y, valuable_y_min, valuable_y_max));

        if (current_x <= valuable_x_min || current_x >= valuable_x_max) rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        if (current_y <= valuable_y_min || current_y >= valuable_y_max) rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //GameObject.GetComponent<Bullet_info>().att +=1;
            collision.gameObject.GetComponent<June_PlayerShooting>().PlayerDamage++;
            Destroy(gameObject);
        }
    }


}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class June_ItemMove : MonoBehaviour
//{
//    public Rigidbody2D rb;
//    Vector3 lastVelocity;
//    public float Speed;
//    float randomX, randomY;

//    void Start()
//    {

//        rb = GetComponent<Rigidbody2D>();
//        randomX = Random.Range(-1f, 1f);
//        randomY = Random.Range(-1f, 1f);

//        Vector2 dir = new Vector2(randomX, randomY).normalized;

//        rb.AddForce(dir * Speed);
//    }
//    void FixedUpdate()
//    {
//        lastVelocity = rb.velocity;
//    }
//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if(collision.gameObject.CompareTag("Player"))
//        {

//            Destroy(gameObject);
//        }
//        var speed = lastVelocity.magnitude;
//        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

//        rb.velocity = direction * Mathf.Max(speed, 0f);


//    }

//    //private void OnTriggerEnter2D(Collider2D collision)
//    //{
//    //    if(collision.tag == "Player")
//    //    {
//    //        Destroy(gameObject);

//    //    }
//    //}

//}
