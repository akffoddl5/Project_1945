using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Kjh_Monster : MonoBehaviour
{
    public AudioSource fishs;
    public AudioSource booms;

    public AudioSource lights;
    


    public GameObject player;


    public GameObject boom;
    public GameObject fish;
    public GameObject lightM;
    public GameObject to;


    GameObject fish1;
    GameObject boom2;
    GameObject lightM1;
    GameObject lightMR;

    public Transform lightMT;
    public Transform boomT;
    public Transform fishT;

    float speed = 6.0f;
    float speedF = 100.0f;
    float speedL = 0.5f;

    Rigidbody2D rb;

    float boomX;
    float boomY;
    float fishX;
    float fishY;
    float boomAnimTime = 0f;
    Animator anim;

    public static int CountZ = 0;
    public static int CountAll = 0;

    Vector2 vel = Vector2.down;
    bool LB = true;
    float j = 0;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = boom.GetComponent<Animator>();
        LightM();
        // lightMR = GameObject.Find("LightMonster").transform.Find("Canvars").transform.Find("Panel").gameObject;
        boomcoroutin();
        StartCoroutine(Boss_init());


    }

    void Update()
    {
        LightM2();


      
       

    }

    IEnumerator Boss_init()
    {
        while (true)
        {
            yield return new WaitForSeconds(9);
            //Instantiate(fish);
            //fish_dir();
            //Invoke("fish_dir", 5);
            fish1 = Instantiate(fish, fishT.position, Quaternion.identity);

            fish1.GetComponent<Rigidbody2D>().AddForce(Vector2.down * speedF);

        }
    }

    void boomcoroutin()
    {
        
        StartCoroutine(Boom());

        Invoke("boomcoroutin", 2);

        float a = Random.Range(1, 2.5f);
        Invoke("toFuntion", a);


        if (CountAll %5 == 1 && 1==2)
        {

            
            if (Kjh_fish.CountDeF>= 25)
            {
                Instantiate(fishs);
                Invoke("fish_dir", 5);
                CountAll++;
            }
        }

        if (CountAll % 4== 1)
        {

            Invoke("LightM", 2);
        }


    }



    void toFuntion()
    {
        if(boom2 != null && to != null)
            Instantiate(to, boom2.transform.position, Quaternion.identity);
           
        

    }


    IEnumerator Boom()
    {



        boomX = Random.Range(3, -3);
        boomY = Random.Range(5, 30);


        Vector3 boomV = new Vector3(boomX, 0, 0) - new Vector3(0, boomY, 0);
        boomV = boomV.normalized;

        boom2 = Instantiate(boom, boomT.position, Quaternion.identity);
        boom2.GetComponent<Rigidbody2D>().velocity = boomV * speed;
        Instantiate(booms);
        Destroy(boom2, 10);

        yield return new WaitForSeconds(5);



    }
    void fish_dir()
    {
        
            fish.transform.SetParent(player.transform);

        Vector3 fishV = player.transform.position - fishT.localPosition;
        fishV = fishV.normalized;

        fishY = fishV.y;
        fishY = Mathf.Abs(fishY);
        fishV.y = -fishY;

            fish1 = Instantiate(fish, fishT.position, Quaternion.identity);
        
            fish1.GetComponent<Rigidbody2D>().AddForce(fishV * speedF);
            Destroy(fish1, 10);
        
    }
    void LightM()
    {



        lightM1 = Instantiate(lightM, lightMT.position, Quaternion.identity);
        LighBim();
        Destroy(lightM1,10);

    }



    void LightM2()
    {


        if (lightM1 != null)
        {
            lightM1.transform.position = Vector2.SmoothDamp(lightM1.transform.position, Vector2.zero, ref vel, speedL);
			LighBim();
		}


    }
    void LighBim()
    {
       
            if (j == 0)
                Invoke("bim", 2f);


            j += Time.deltaTime;

            lightMR = GameObject.Find("Circle");

           
            

            if (j >= 0.7f)
            {
                lightMR.transform.localScale = new Vector3(j * 140, j *140, 0);
                
                if (j >= 2f)
                {
                    j = 0;

                    Destroy(lightM1);

                }
            }





    }

    void bim()
    {
        Instantiate(lights);

    }

    private void OnBecameInvisible()
    {
      Destroy(gameObject);
    }

}
