using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Toong_Bullet : MonoBehaviour
{
    public GameObject normal_bullet;
    public GameObject guide_bullet;

    private void Start()
    {
        
        StartCoroutine(IE_Shoot_Circle());
        

    }
    IEnumerator IE_Shoot_Circle()
    {
        while (true)
        {
            Shoot_Circle();
            yield return new WaitForSeconds(0.2f);
            Shoot_Circle();
            yield return new WaitForSeconds(0.2f);
            Shoot_Circle();
            yield return new WaitForSeconds(0.2f);
            Shoot_Circle();
            yield return new WaitForSeconds(5f);
        }
    }

    //원으로 쏘기
    public void Shoot_Circle()
    {
        int point = 40;
        float angle = 360.0f / point;   //z축 회전은 180도가 전부

        for (int i = 0; i < point; i++)
        {
            //GameObject tmp = Instantiate(normal_bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle * i)));
            float dir_x = Mathf.Cos(angle * i / 180 * Mathf.PI);
            float dir_y = Mathf.Sin(angle * i / 180 * Mathf.PI);
            GameObject tmp = Instantiate(normal_bullet, transform.position + new Vector3(dir_x, dir_y, 0), Quaternion.identity);
            tmp.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(dir_x, dir_y), 1f);
            //Debug.Log(angle * i + " <>  " + point + "  " + i + " " + tmp.name + " " + tmp.transform.rotation);
        }
    }

    IEnumerator IE_Shoot_flower()
    {
        Shoot_flower();
        yield return new WaitForSeconds(4f);
        Shoot_flower();
        yield return new WaitForSeconds(4f);
    }

    //꽃 탄막   ~2 ~ +2 로 일단 ㄱㄱㄱ
    //
    public void Shoot_flower()
    {
        float start_x_radian = -1f;
        float end_x_radian = 1f;
        float current_x = -1f ;
        int point = 20;
        float interval = (end_x_radian - start_x_radian) / (float)point;


        for (int i = 0; i < point; i++)
        {
            current_x += interval;
            float tmp_x = current_x;
            float tmp_y = Mathf.Atan(3 * current_x) * 3/2.0f;
            float tmp2_x = current_x;
            float tmp2_y = Mathf.Tan(current_x) * 3 /2.0f ;

            GameObject a1 = Instantiate(normal_bullet, transform.position + new Vector3(tmp_x, tmp_y, 0), Quaternion.identity);
            GameObject a2 = Instantiate(normal_bullet, transform.position + new Vector3(tmp2_x, tmp2_y, 0), Quaternion.identity);

            //반전
            GameObject a3 = Instantiate(normal_bullet, transform.position + new Vector3(-tmp_x, tmp_y, 0), Quaternion.identity);
            GameObject a4 = Instantiate(normal_bullet, transform.position + new Vector3(-tmp2_x, tmp2_y, 0), Quaternion.identity);


            //두번쨰
            float tmp3_x = current_x;
            float tmp3_y = Mathf.Atan(current_x);
            float tmp4_x = current_x;
            float tmp4_y = Mathf.Tan(current_x) / 5;
            GameObject a5 = Instantiate(normal_bullet, transform.position + new Vector3(tmp3_x, tmp3_y, 0), Quaternion.identity);
            GameObject a6 = Instantiate(normal_bullet, transform.position + new Vector3(tmp4_x, tmp4_y, 0), Quaternion.identity);

            //반전
            GameObject a7 = Instantiate(normal_bullet, transform.position + new Vector3(-tmp3_x, tmp3_y, 0), Quaternion.identity);
            GameObject a8 = Instantiate(normal_bullet, transform.position + new Vector3(-tmp4_x, tmp4_y, 0), Quaternion.identity);

            a1.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 1f);
            a2.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 1f);
            a3.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 1f);
            a4.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 1f);
            a5.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 1f);
            a6.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 1f);
            a7.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 1f);
            a8.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 1f);


        }

        
        
    }

    
    
}
