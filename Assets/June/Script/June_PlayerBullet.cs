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
    public int ItemCount;
    public GameObject item;

   
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

        if(GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerShooting>().PlayerDamage == 1) //powerup에 따라서 총알 스프라이트 변경
            spriteRenderer.sprite = Sprites[0];
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerShooting>().PlayerDamage == 2)
            spriteRenderer.sprite = Sprites[1];
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerShooting>().PlayerDamage == 3)
            spriteRenderer.sprite = Sprites[2];
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerShooting>().PlayerDamage == 4)
            spriteRenderer.sprite = Sprites[3];

    }


    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
