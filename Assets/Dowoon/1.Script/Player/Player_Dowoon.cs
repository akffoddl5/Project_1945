using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Dowoon : MonoBehaviour
{
    float moveSpeed = 10.5f;
    Animator anim;
    [SerializeField]    
    GameObject bulletPos;
    [SerializeField]
    GameObject bulletPrefab; 
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //bulletPos = GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate( new Vector3(1,0,0) * h * moveSpeed * Time.deltaTime);
        transform.Translate( new Vector3(0,1,0)* v * moveSpeed * Time.deltaTime);

        anim.SetFloat("h", h);
        anim.SetFloat("v", v);


        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShotBullet();
        }       
    }

    public void ShotBullet()
    {
        var pos = bulletPos.transform.position;
        var b1 = Instantiate(bulletPrefab, bulletPos.transform.position, Quaternion.identity);
        var b2 = Instantiate(bulletPrefab, bulletPos.transform.position, Quaternion.identity);
        var b3 = Instantiate(bulletPrefab, bulletPos.transform.position, Quaternion.identity);
        //    var b1 = ObjectPool_Dowoon.GetBullet(BulletType.Player);
        b1.transform.position = pos;

        pos.x -= 0.6f;
        pos.y -= 0.4f;
      //  var b2 = ObjectPool_Dowoon.GetBullet(BulletType.Player);
        b2.transform.position = pos;
        pos.x += 1.2f;
       // var b3 = ObjectPool_Dowoon.GetBullet(BulletType.Player);
        b3.transform.position = pos;
       
    }
}
