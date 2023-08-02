using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Player_Bullet_GEN : MonoBehaviour
{
    public GameObject[] generators = new GameObject[2];
    public GameObject[] generators_guide = new GameObject[2];
    [SerializeField]
    float shoot_cool = 0f;  //60 FRAME 공격
    float guid_cool = 0f;
    public float shoot_cool_max = 10f;
    int current_generator = 0;
    int current_generator2 = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        //1,2 자동공격
        shoot_cool -= Time.deltaTime;
        if (shoot_cool <= 0)
        {
            current_generator++;
            if (current_generator >= generators.Length) current_generator = 0;
            
            generators[current_generator].GetComponent<KYS_Bullet_generator>().Shoot();

            shoot_cool = shoot_cool_max * Time.deltaTime;
        }

        //3,4 유도 자동공격
        //guid_cool -= Time.deltaTime;
        //if (guid_cool <= 0)
        //{
        //    current_generator2++;
        //    if (current_generator2 >= generators_guide.Length) current_generator2 = 0;

        //    generators_guide[current_generator2].GetComponent<KYS_Bullet_generator>().Shoot();

        //    guid_cool = 1 * 90 * Time.deltaTime;
        //}
        

        

    }
}
