using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class kjh_pbullet : MonoBehaviour
{
   

    float speed = 2.0f;
    

    



    bool ismove=false;

    void Start()
    {
     


    }


    void Update()
    {

        bullet();


    }

    void bullet()
    {
        
       if (Input.GetKey(KeyCode.Space))
        {

            Quaternion.Euler(0, 0, 45f);
          
        }
    }

    
    void Ismove()
    {
        float mx = Input.GetAxis("Horizontal");
        float my = Input.GetAxis("Vertical");


        if (mx != 0 || my != 0)
        {
            ismove = true;
        }

        if (mx == 0 && my == 0)
        {
            ismove = false;
        }

        

    }

}

   

  





