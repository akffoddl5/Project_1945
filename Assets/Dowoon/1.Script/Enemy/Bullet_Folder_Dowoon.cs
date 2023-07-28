using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Folder_Dowoon : Bullet_Dowoon
{
    // Start is called before the first frame update

    public GameObject bulletSpr;
    

    public override void Update()
    {
          bulletSpr.transform.Rotate(0, 0, 115  * Time.deltaTime);
          Fly();

      //  transform.Translate(transform.up * 10.5f * Time.deltaTime);
    }






}
