using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Kjh_fish : MonoBehaviour
{
    float time;
    public static bool ex =true;
    int CountDeF=0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Kjh_player move = new Kjh_player();
        if (ex == false)
        {
            time += Time.deltaTime;

        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Kjh_player>().Move(false);
            // other.gameObject << Ãæµ¹ÇÑ °´Ã¼ 
            if (time >=2)
            {
                other.GetComponent<Kjh_player>().Move(true);
                time = 0;
                ex = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        ex = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Kjh_Monster.CountAll++;
        if (collision.gameObject.CompareTag("Player_bullet"))
         CountDeF++;

        if (GameObject.Find("knife"))
        {
            CountDeF += 5;
        }

        if (CountDeF == 40)
        {
            Kjh_Monster.CountZ++;
            Destroy(gameObject);
            CountDeF = 0;
        }
        else if(CountDeF >= 40)
        {
            Destroy(gameObject);
            CountDeF = 0;
        }
    
    }
}