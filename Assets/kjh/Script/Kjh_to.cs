using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kjh_to : MonoBehaviour
{
    public AudioSource tos;
    float t;
    // Start is called before the first frame update

    void Start()
    {
        Destroy(gameObject, 2);
        Instantiate(tos);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

        private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

           
            float toY= collision.gameObject.GetComponent<Transform>().position.y;
            toY = toY +3;
            collision.gameObject.GetComponent<Transform>().position = new Vector3(0, toY, 0);
        }
    }

   
}