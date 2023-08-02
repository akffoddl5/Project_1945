using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Kjh_Monster : MonoBehaviour
{

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

    float speed = 2.0f;
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

        anim = boom.GetComponent<Animator>();
        LightM();
        // lightMR = GameObject.Find("LightMonster").transform.Find("Canvars").transform.Find("Panel").gameObject;
        boomcoroutin();

        lightMR = GameObject.Find("Circle");
    }


    void Update()
    {
        LightM2();
        
        j += Time.deltaTime;
        Debug.Log(CountAll);

    }

    void boomcoroutin()
    {

        StartCoroutine(Boom());

        Invoke("boomcoroutin", 4);

        int a = Random.Range(2, 6);
        Invoke("toFuntion", a);


        if (CountAll% 7 == 1)
        {

            Invoke("fish_dir", 2);
        }

        if (CountAll % 13== 1)
        {

            Invoke("LightM", 2);
        }


    }



    void toFuntion()
    {
        if (boom != null)
        {
            Instantiate(to, boom2.transform.position, Quaternion.identity);
        }


    }


    IEnumerator Boom()
    {



        boomX = Random.Range(3, -3);
        boomY = Random.Range(5, 30);


        Vector3 boomV = new Vector3(boomX, 0, 0) - new Vector3(0, boomY, 0);
        boomV = boomV.normalized;

        boom2 = Instantiate(boom, boomT.position, Quaternion.identity);
        boom2.GetComponent<Rigidbody2D>().velocity = boomV * speed;


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

    }
    void LightM()
    {




        lightM1 = Instantiate(lightM, lightMT.position, Quaternion.identity);

       
    }



    void LightM2()
    {



        lightM1.transform.position = Vector2.SmoothDamp(lightM1.transform.position, Vector2.zero, ref vel, speedL);
        j = 0;
        LighBim();


    }
    void LighBim()
    {



        if (j>=2)
        {
            lightMR.transform.localScale = new Vector3(j*1000, j*1000, 0);
          
           
            if (j >= 5)
            {
                j = 0;
               Destroy(lightMR);
            Destroy(lightM1);

            }

        }





    }


    private void Del()
    {
        GameObject gm = GameObject.Find("player");
        if (gm.transform.position.x >= 8 || gm.transform.position.x <= -8 || gm.transform.position.y >= 8 || gm.transform.position.x <= -8)
        {
            Destroy(fish);
            Destroy(fish1);
            Destroy(boom);
            Destroy(boom2);
         
        }
    }


}