using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Kjh_LightM : MonoBehaviour
{
    int CountDeL=0;
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


           


            CountDeL++;
        }

        if (GameObject.Find("knife"))
        {
            CountDeL+= 5;
        }


        if (CountDeL == 10)
        {
            //Kjh_Monster.CountZ++;

            Destroy(gameObject);
            CountDeL = 0;
        }
        else if (CountDeL >= 10)
        {
            Destroy(gameObject);
            CountDeL = 0;
        }
    }
}
