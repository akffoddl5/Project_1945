using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{

    public GameObject rotateTarget;
    public GameObject spr;

    public bool b_isCollideAble; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      // spr.transform.RotateAround(rotateTarget.transform.position, new Vector3(0, 0, 1), 55f * Time.deltaTime);

        //transform.Translate(0, 0.2f*Time.deltaTime, 0);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(b_isCollideAble) 
            {
                Debug.Log(" 방해물 맞음");
            }
          
        }
    }
}
