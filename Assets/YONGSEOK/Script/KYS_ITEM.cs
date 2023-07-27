using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class KYS_ITEM : MonoBehaviour
{
    //public static Dictionary<string,GameObject> item_dic = new Dictionary<string,GameObject>(); // ITEM MANGER 가 가지고 있는걸로

    float valuable_x_max;
    float valuable_x_min;
    float valuable_y_max;
    float valuable_y_min;

    float current_x = 0f;
    float current_y = 0f;
    public float speed = 5f;

    //랜덤 4방향으로 이동 시작
    Rigidbody2D rb;
    Vector2[] rand_vec_arr = new Vector2[4] {new Vector2(1,1), new Vector2(1,-1), new Vector2(-1,-1), new Vector2(-1,1) };

    void Start()
    {
        float field_size_x = Camera.main.orthographicSize * Screen.width / Screen.height;
        float field_size_y = Camera.main.orthographicSize;

        float item_size_x = transform.GetComponent<BoxCollider2D>().bounds.size.x;
        float item_size_y = transform.GetComponent<BoxCollider2D>().bounds.size.y;

        valuable_x_max = Camera.main.transform.position.x + (field_size_x - item_size_x / 2.0f);
        valuable_x_min = Camera.main.transform.position.x - (field_size_x - item_size_x / 2.0f);
        valuable_y_max = Camera.main.transform.position.y + (field_size_y - item_size_y / 2.0f);
        valuable_y_min = Camera.main.transform.position.y - (field_size_y - item_size_y / 2.0f);

        Debug.Log(item_size_x + " " + item_size_y + " " + valuable_x_max + " " + valuable_x_min + " " + valuable_y_max + " " + valuable_y_min);

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

    
}
