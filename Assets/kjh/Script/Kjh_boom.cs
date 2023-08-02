using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Kjh_boom : MonoBehaviour
{
    public GameObject to;
    //using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
    int CountDeB = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Kjh_Monster.CountAll++;
        if (collision.CompareTag("Player_bullet"))
        {


            Destroy(collision.gameObject);


            CountDeB++;

        }

        if (GameObject.Find("knife"))
        {
            CountDeB+= 5;
        }

        if (CountDeB == 2)
            {
            //Kjh_Monster.CountZ++;
            
                Destroy(gameObject);
                CountDeB = 0;
            }
        else if (CountDeB >= 2)
        {
           
            
            Destroy(gameObject);
            CountDeB = 0;
        }

    }

}