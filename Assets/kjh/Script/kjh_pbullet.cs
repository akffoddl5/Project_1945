using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Transform;
using UnityEngine.UIElements;



public class kjh_pbullet : MonoBehaviour
{

    public GameObject pbullet;
    public GameObject knife;
    float speed = 3.0f;
    float speedK = 100000.0f;
    public Transform pbulletF;
    public Transform pbulletL;
    public Transform pbulletR;
    public Transform knifeF;
    public Transform knifeL1;
    public Transform knifeL2;
    public Transform knifeR1;
    public Transform knifeR2;

    static int countK = 0;


    GameObject[] bulletObj = new GameObject[8];




    Rigidbody2D rb2D;




    bool ismove = false;

    void Start()
    {

        rb2D = GetComponent<Rigidbody2D>();


    }
    void Update()
    {

        bullet();
        Knife();

    }

    void bullet()
    {
        //if (ismove && Input.GetKey(KeyCode.Space))
        //{
        //    for (int i = 0; i < 4; i++)
        //    {
        //        pbulletManager.transform.Rotate(0, 0, 22.5f * i);
        //        Instantiate(gameObject,pbulletManager.transform.position,Quaternion.identity);
        //        rb2D.velocity= Vector3.up;
        //    }

        //}     

        if (Input.GetKeyDown(KeyCode.Space) && !(Input.GetKeyDown(KeyCode.Z)))
        {

            if (!Input.GetKey(KeyCode.Z) && (Input.GetAxis("Horizontal") == 0))
            {
                bulletObj[5] = Instantiate(pbullet, pbulletF.position, pbulletF.transform.rotation);
                bulletObj[5].GetComponent<Rigidbody2D>().velocity = Vector3.up * speed;

            }
            else if (!Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.LeftArrow))
            {
                bulletObj[6] = Instantiate(pbullet, pbulletL.position, pbulletL.transform.rotation);
                bulletObj[6].GetComponent<Rigidbody2D>().velocity = Vector3.left * speed;

            }
            else if (!Input.GetKey(KeyCode.Z) && Input.GetKey(KeyCode.RightArrow))
            {
                bulletObj[7] = Instantiate(pbullet, pbulletR.position, pbulletR.transform.rotation);
                bulletObj[7].GetComponent<Rigidbody2D>().velocity = Vector3.right * speed;
            }




        }
    }

    void Knife()
    {

        if (Kjh_Monster.CountZ >=6&& Input.GetKeyDown(KeyCode.Z))
        {


            if (Input.GetKeyDown(KeyCode.Z))
            {
                bulletObj[0] = Instantiate(knife, knifeF.position, knifeF.transform.rotation);
                bulletObj[1] = Instantiate(knife, knifeL1.position, knifeL1.transform.rotation);
                bulletObj[2] = Instantiate(knife, knifeL2.position, knifeL2.transform.rotation);
                bulletObj[3] = Instantiate(knife, knifeR1.position, knifeR1.transform.rotation);
                bulletObj[4] = Instantiate(knife, knifeR2.position, knifeR2.transform.rotation);


                bulletObj[0].transform.SetParent(knifeF);
                bulletObj[1].transform.SetParent(knifeL1);
                bulletObj[2].transform.SetParent(knifeL2);
                bulletObj[3].transform.SetParent(knifeR1);
                bulletObj[4].transform.SetParent(knifeR2);


            }

            ///bulletObj[a].transform.position = knifeT[a].position;


            if (Input.GetKey(KeyCode.Z) && Input.GetKeyDown(KeyCode.Space))
            {

                countK++;


                if (countK == 1)
                {


                    //Destroy(bulletObj[0]);
                    bulletObj[0].GetComponent<Rigidbody2D>().AddForce(Vector3.up * Time.deltaTime * speedK);//몬스터 따라가기할거임
                }
                if (countK == 2)
                {

                    Debug.Log(countK);
                    //Destroy(bulletObj[1]);
                    bulletObj[1].GetComponent<Rigidbody2D>().AddForce(Vector3.up * Time.deltaTime * speedK);

                }
                if (countK == 3)
                {


                    // Destroy(bulletObj[3]);
                    bulletObj[3].GetComponent<Rigidbody2D>().AddForce(Vector3.up * Time.deltaTime * speedK);//몬스터 따라가기할거임
                }
                if (countK == 4)
                {


                    //Destroy(bulletObj[2]);
                    bulletObj[2].GetComponent<Rigidbody2D>().AddForce(Vector3.up * Time.deltaTime * speedK);//몬스터 따라가기할거임
                }
                if (countK == 5)
                {

                    // Destroy(bulletObj[4]);
                    bulletObj[4].GetComponent<Rigidbody2D>().AddForce(Vector3.up * Time.deltaTime * speedK);//몬스터 따라가기할거임

                }








                //bulletObj[0].GetComponent<Rigidbody2D>().velocity = Vector3.up * Time.deltaTime * speed;//몬스터 따라가기할거임
            }
        }


        if (!Input.GetKey(KeyCode.Z))
        {

            Destroy(bulletObj[0]);
            Destroy(bulletObj[1]);
            Destroy(bulletObj[2]);
            Destroy(bulletObj[3]);
            Destroy(bulletObj[4]);
            countK = 0;
        }



    }


    void Ismove()
    {
        float mx = Input.GetAxis("Horizontal");
        float my = Input.GetAxis("Vertical");


        if (mx != 0 || my != 0)//움직일때
        {
            ismove = true;
        }

        if (mx == 0 && my == 0)
        {
            ismove = false;
        }



    }
   
}










