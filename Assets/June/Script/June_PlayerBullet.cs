using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class June_PlayerBullet : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    public GameObject Effect;
    public Sprite[] Sprites;
    public float Speed = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(0, 0, 90);
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * Speed * Time.deltaTime);


    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

}
