using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class June_ItemMove : MonoBehaviour
{
    public Rigidbody2D rb;
    Vector3 lastVelocity;
    public float Speed;
    float randomX, randomY;

    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        randomX = Random.Range(-1f, 1f);
        randomY = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(randomX, randomY).normalized;

        rb.AddForce(dir * Speed);
    }
    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

        rb.velocity = direction * Mathf.Max(speed, 0f);

    }
}
