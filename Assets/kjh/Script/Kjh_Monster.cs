using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class Kjh_Monster : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject boom;
    public GameObject to;
    //public GameObject to;
    public Transform boomT;
    float speed = 2.0f;
    static bool timeB = false;
    float timeF = 0;
    Rigidbody2D rb;

    float boomX;
    float boomY;
    float boomAnimTime = 0f;
    Animator anim;





    void Start()
    {

        anim = boom.GetComponent<Animator>();

        boomcoroutin();
        //tocoroutin();

    }


    void Update()
    {

       // boomstart();





    }

    void boomcoroutin()
    {

        StartCoroutine(Boom());

        Invoke("boomcoroutin", 10);

        Invoke("toFuntion", 3);









    }

    void toFuntion()
    {

        Instantiate(to, boom.transform.position, Quaternion.identity);


    }

    IEnumerator Boom()
    {



        boomX = Random.Range(3, -3);
        boomY = Random.Range(0, 30);


        Vector3 boomV = new Vector3(boomX, 0, 0) - new Vector3(0, boomY, 0);
        boomV = boomV.normalized;

        boom = Instantiate(boom, boomT.position, Quaternion.identity);
        boom.GetComponent<Rigidbody2D>().velocity = boomV * speed;

        timeB = false;


        yield return new WaitForSeconds(5);



    }



    //void boomstart()//영역 제한
    //{
    //    boomX = boom.transform.position.x;
    //    boomY = boom.transform.position.y;


    //    if (boomX <= -2.3f || boomX >= 2.3f)
    //    {

    //        boom.GetComponent<Rigidbody2D>().velocity = new Vector2(-boomX - 1, -boomY) * speed;



    //    }


    //}
    
}
