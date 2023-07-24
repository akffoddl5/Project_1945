using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class June_PlayerShooting : MonoBehaviour
{
    public GameObject bullet = null; //미사일 
    public Transform pos = null; //미사일 발사
    public float FireSpeed = 0.2f;
    public GameObject Lazer;

    void Start()
    {

        StartCoroutine("AutoFire");
   
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            Instantiate(bullet, pos.position, Quaternion.identity);
        }

      

    }
    IEnumerator AutoFire()
    {
        for (; ; )
        {
            if (Input.GetKey(KeyCode.X) == true)
            {
                Instantiate(bullet, pos.position, Quaternion.identity);

            }
            yield return new WaitForSeconds(FireSpeed);

        }
    }
}
