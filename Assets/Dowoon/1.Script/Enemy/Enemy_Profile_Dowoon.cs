using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Profile_Dowoon : Enemy_Dowoon
{
    // Start is called before the first frame update
    public override void Start()
    {
        shootDelay = 3.5f;

        hp = 150;
    }

    public override void Start_ShotCoroutine()
    {
        StartCoroutine(Shot());
    }

    IEnumerator Shot()
    {

        yield return new WaitForSeconds(0.1f);
    }



}
