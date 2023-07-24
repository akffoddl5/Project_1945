using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Furin_bullet : MonoBehaviour
{
    public GameObject normal_bullet;
    public GameObject guide_bullet;

    private void Start()
    {
        StartCoroutine(IE_Shoot_Circle());
    }
    private void Update()
    {
        
    }

    IEnumerator IE_Shoot_Circle()
    {
        while (true)
        {
            Shoot_Circle();
            yield return new WaitForSeconds(5f);
        }
        //red
    }

    //원으로 쏘기
    public void Shoot_Circle()
    {
        int point = 20;
        float angle = 360 / point;
        for (int i = 0; i < point; i++)
        {
            float move_x = Mathf.Cos(angle * i * Mathf.PI/ 180f);
            float move_y = Mathf.Sin(angle * i * Mathf.PI/ 180f);
            Debug.Log(move_x + " " + move_y + "  <<<<");
            GameObject tmp = Instantiate(normal_bullet, transform.position , Quaternion.identity);
            tmp.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(move_x,move_y));
            
        }
    }

    //아래 세방향으로 쏘기
    public void Shoot_Fork()
    {

    }

    //회전하면서 쏘기
    public void Shoot_Cycle()
    {

    }


    //별모양으로 쏘기
    public void Shoot_Star()
    {
    }

    
}
