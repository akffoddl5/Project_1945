using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class June_CorwBulletDirection : MonoBehaviour
{
    public GameObject target =null;
    Vector3 dir;
    Vector3 dirNo;

    public void Start()
    {
        gameObject.transform.Rotate(0, 0, 90);

    }

    public void Update()
    {
        transform.parent.transform.Translate(Vector2.up * 3 * Time.deltaTime);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == GameObject.Find("Boss(Clone)"))
        {
            target = collision.gameObject;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            transform.parent.GetComponent<June_CrowBullet>().enabled= true;
        }
       
        
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

}