using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kjh_to : MonoBehaviour
{
    
    // Start is called before the first frame update

    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
           
            float toY= collision.gameObject.GetComponent<Transform>().position.y;
            toY = toY - 5;
            collision.gameObject.GetComponent<Transform>().position = new Vector3(0, toY, 0);
        }
    }

   
}