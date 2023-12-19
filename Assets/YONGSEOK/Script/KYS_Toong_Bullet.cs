using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KYS_Toong_Bullet : MonoBehaviour
{
    public GameObject normal_bullet;
    public GameObject guide_bullet;
    List<List<GameObject> > gameObjects = new List<List<GameObject> >();
    public AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(IE_Shoot_Controll());
		StartCoroutine(AudioPlay());
		//StartCoroutine(IE_Shoot_Square(3f, Vector3.zero));
		//StartCoroutine(IE_Shoot_Triangle(7.5f, new Vector3(0,0,90)));
	}

    IEnumerator AudioPlay()
    {
        while (true)
        {
            yield return new WaitForSeconds(8f);
		    audioSource.Play();
        }
    }

    IEnumerator IE_Shoot_Controll()
    {
        while (true)
        {
            int tri_random_val = Random.Range(0, 3);
            int[] arr_idx = new int[3] { 0,0,0 };
            for (int i = 0; i < 3; i++)
            {
                if (i == tri_random_val)    //이떄 진짜 총알 쏘는걸로
                {
                    arr_idx[i] = 1;
                }
            }


            StartCoroutine(IE_Shoot_Triangle(2f, Vector3.zero, arr_idx[0]));
            StartCoroutine(IE_Shoot_Square(3f, Vector3.zero, arr_idx[1]));
            StartCoroutine(IE_Shoot_Triangle(7.5f, new Vector3(0, 0, 90), arr_idx[2]));

            yield return new WaitForSeconds(1);
            for (int i = 0; i < gameObjects.Count; i++)
            {
                for (int j = 0; j < gameObjects[i].Count; j++) {
                    gameObjects[i][j].GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0,-1),0.3f);
                }
            }
            gameObjects.Clear();
            yield return new WaitForSeconds(5);

            StartCoroutine(IE_Shoot_Circle());
            //StartCoroutine(IE_Shoot_flower());
            yield return new WaitForSeconds(5);

            StartCoroutine(IE_Shoot_flower());
            StartCoroutine(IE_Shoot_Circle());
            yield return new WaitForSeconds(5);

            StartCoroutine(IE_Shoot_flower());
            StartCoroutine(IE_Shoot_Circle());
            yield return new WaitForSeconds(5);
        }
        
        
    }

    //삼각형 쏘기
    IEnumerator IE_Shoot_Triangle(float _radius, Vector3 _dir, int _bullet_val)
    {
        
        yield return new WaitForSeconds(1);
        //Shoot_Triangle(2f, Vector3.zero);
        Shoot_Triangle(_radius, _dir, _bullet_val);
    }

    IEnumerator IE_Shoot_Square(float _radius, Vector3 _dir,int _bullet_val)
    {
        yield return new WaitForSeconds(1);
        //Shoot_Square(3f, Vector3.zero);
        Shoot_Square(_radius, _dir, _bullet_val);


    }

    //세모 만들기
    public void Shoot_Triangle(float _radius, Vector3 _rotate, int _bullet_val){
        GameObject current_bullet;
        Bullet_Type type = Bullet_Type.NORMAL;
        if (_bullet_val == 0)
        {
            current_bullet = guide_bullet;
            type = Bullet_Type.GUIDE;
        }
        else
        {
            current_bullet = normal_bullet;
            type = Bullet_Type.NORMAL;
        }
        float min_x = -0.5f;
        float max_x = 0.5f;
        int point = 20;
        float interval = (max_x - min_x) / (float)point;
        float current_x = min_x;
        float radius = _radius;
        List<GameObject> tmp_list = new List<GameObject>();
        for (int i = 0; i < point; i++)
        {
            current_x += interval;
            float tmp_x = current_x;
            float tmp_y1 = -0.2f;
            float tmp_y2 = 2 * current_x + 0.5f;
            float tmp_y3 = -2 * current_x + 0.5f;

            if (tmp_x >= -0.3f && tmp_x <= 0.3f) {

                //GameObject a1 = Instantiate(current_bullet, transform.position + new Vector3(tmp_x, tmp_y1,0) * radius, Quaternion.identity);
                GameObject a1 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(tmp_x, tmp_y1, 0) * radius, Quaternion.identity);
                tmp_list.Add(a1);
            }
            if (tmp_y2 < 0.5f && tmp_y2 >= -0.2f)
            {
                //GameObject a2 = Instantiate(current_bullet, transform.position + new Vector3(tmp_x, tmp_y2,0) * radius, Quaternion.identity);
                GameObject a2 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(tmp_x, tmp_y2, 0) * radius, Quaternion.identity);
                tmp_list.Add(a2);
            }
            if (tmp_y3 < 0.5f && tmp_y3 >= -0.2f)
            {
                //GameObject a3 = Instantiate(current_bullet, transform.position + new Vector3(tmp_x, tmp_y3,0) * radius, Quaternion.identity);
                GameObject a3 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(tmp_x, tmp_y3, 0) * radius, Quaternion.identity);
                tmp_list.Add(a3);
            }
        }
        gameObjects.Add(tmp_list);

    }

    //네모 만들기. 만들기만 하고 쏘지 말자 일단 리스트로 만들어서 나중에 한꺼번에 쏘기
    public void Shoot_Square(float _radius, Vector3 _rotate, int _bullet_val)
    {
        GameObject current_bullet;
        Bullet_Type type = Bullet_Type.NORMAL;
        if (_bullet_val == 0)
        {
            current_bullet = guide_bullet;
            type = Bullet_Type.GUIDE;
        }
        else
        {
            current_bullet = normal_bullet;
            type = Bullet_Type.NORMAL;
        }
        float min_x = -0.55f;
        float max_x = 0.5f;
        int point = 15;
        float interval = (max_x - min_x)/ point;
        float current_x = min_x;
        float radius = _radius;
        Vector3[] offset = new Vector3[3] { Vector3.zero, new Vector3(-1, 1, 0), new Vector3(1, 1, 0) };
        List<GameObject> tmp_list = new List<GameObject>();
        for (int tc = 0; tc < offset.Length; tc++)
        {
            if (tc != 0) continue;
            current_x = min_x;
            for (int i = 0; i < point; i++)
            {
                current_x += interval;

                float tmp_x = current_x;
                float tmp_y1 = -tmp_x + 0.5f;
                float tmp_y2 = -tmp_x - 0.5f;
                float tmp_y3 = tmp_x + 0.5f;
                float tmp_y4 = tmp_x - 0.5f;


                if (tmp_x >= 0)
                {
                    //GameObject a1 = Instantiate(current_bullet, offset[tc] + transform.position + new Vector3(tmp_x, tmp_y1, 0) * radius, Quaternion.identity);
                    //GameObject a4 = Instantiate(current_bullet, offset[tc] + transform.position + new Vector3(tmp_x, tmp_y4, 0) * radius, Quaternion.identity);
                    GameObject a1 = KYS_ObjectPool.instance.Instantiate(type, offset[tc] + transform.position + new Vector3(tmp_x, tmp_y1, 0) * radius, Quaternion.identity);
                    GameObject a4 = KYS_ObjectPool.instance.Instantiate(type, offset[tc] + transform.position + new Vector3(tmp_x, tmp_y4, 0) * radius, Quaternion.identity);
                    
                    tmp_list.Add(a1);
                    tmp_list.Add(a4);
                }

                if (tmp_x <= 0)
                {
                    //GameObject a2 = Instantiate(current_bullet, offset[tc] + transform.position + new Vector3(tmp_x, tmp_y2, 0) * radius, Quaternion.identity);
                    //GameObject a3 = Instantiate(current_bullet, offset[tc] + transform.position + new Vector3(tmp_x, tmp_y3, 0) * radius, Quaternion.identity);

                    GameObject a2 = KYS_ObjectPool.instance.Instantiate(type, offset[tc] + transform.position + new Vector3(tmp_x, tmp_y2, 0) * radius, Quaternion.identity);
                    GameObject a3 = KYS_ObjectPool.instance.Instantiate(type, offset[tc] + transform.position + new Vector3(tmp_x, tmp_y3, 0) * radius, Quaternion.identity);

                    tmp_list.Add(a2);
                    tmp_list.Add(a3);
                }

                

            }
        }

        gameObjects.Add(tmp_list);


        


    }

    IEnumerator IE_Shoot_Circle()
    {
        
        Shoot_Circle();
        yield return new WaitForSeconds(1f);
        Shoot_Circle();
        yield return new WaitForSeconds(1f);
        Shoot_Circle();
        yield return new WaitForSeconds(5f);
        
    }

    //원으로 쏘기
    public void Shoot_Circle()
    {
        float radian = 3f;
        int point = 40;
        float angle = 360.0f / point;   //z축 회전은 180도가 전부

        for (int i = 0; i < point; i++)
        {
            //GameObject tmp = Instantiate(normal_bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle * i)));
            float dir_x = Mathf.Cos(angle * i / 180 * Mathf.PI);
            float dir_y = Mathf.Sin(angle * i / 180 * Mathf.PI);
            //GameObject tmp = Instantiate(normal_bullet, transform.position + new Vector3(dir_x, dir_y, 0) * radian, Quaternion.identity);
            GameObject tmp = KYS_ObjectPool.instance.Instantiate(Bullet_Type.NORMAL, transform.position + new Vector3(dir_x, dir_y, 0) * radian, Quaternion.identity);
            Vector3 new_dir = transform.position - tmp.transform.position;

            tmp.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(new_dir.x, new_dir.y), 0.3f);
            //Debug.Log(transform.position.x + " " + transform.position.y + " " + dir_x + " " + dir_y );
        }
    }

    IEnumerator IE_Shoot_flower()
    {
        int r = Random.Range(0, 3);
        for (int i = 0; i < 3; i++)
        {
            if (r == i)
            {
                Shoot_flower(i*2-2, 0);  //얘는 가짜 총알
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                Shoot_flower(i*2-2, 1);  //얘는 진짜 총알
                yield return new WaitForSeconds(0.2f);
            }
        }

        //Shoot_flower(0);
        //yield return new WaitForSeconds(0.2f);
        //Shoot_flower(-2);
        //yield return new WaitForSeconds(0.2f);
        //Shoot_flower(2);
        //yield return new WaitForSeconds(0.2f);
    }

    //꽃 탄막   ~2 ~ +2 로 일단 ㄱㄱㄱ
    //
    public void Shoot_flower(float offset_x, int bullet_val)
    {

        GameObject current_bullet;

        Bullet_Type type = Bullet_Type.NORMAL;
        if (bullet_val == 0)
        {
            current_bullet = guide_bullet;
            type = Bullet_Type.GUIDE;
        }
        else
        {
            current_bullet = normal_bullet;
            type = Bullet_Type.NORMAL;
        }


        float start_x_radian = -1f;
        float end_x_radian = 1f;
        float current_x = -1f;
        int point = 15;
        float interval = (end_x_radian - start_x_radian) / (float)point;


        for (int i = 0; i < point; i++)
        {
            current_x += interval;
            float tmp_x = current_x;
            float tmp_y = Mathf.Atan(3 * current_x) * 3/2.0f;
            float tmp2_x = current_x;
            float tmp2_y = Mathf.Tan(current_x) * 3 /2.0f ;

            //GameObject a1 = Instantiate(current_bullet, transform.position + new Vector3(tmp_x + offset_x, tmp_y, 0), Quaternion.identity);
            //GameObject a2 = Instantiate(current_bullet, transform.position + new Vector3(tmp2_x + offset_x, tmp2_y, 0), Quaternion.identity);
            GameObject a1 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(tmp_x + offset_x, tmp_y, 0), Quaternion.identity);
            GameObject a2 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(tmp2_x + offset_x, tmp2_y, 0), Quaternion.identity);

            //반전
            //GameObject a3 = Instantiate(current_bullet, transform.position + new Vector3(-tmp_x + offset_x, tmp_y, 0), Quaternion.identity);
            //GameObject a4 = Instantiate(current_bullet, transform.position + new Vector3(-tmp2_x + offset_x, tmp2_y, 0), Quaternion.identity);
            GameObject a3 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(-tmp_x + offset_x, tmp_y, 0), Quaternion.identity);
            GameObject a4 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(-tmp2_x + offset_x, tmp2_y, 0), Quaternion.identity);


            //두번쨰
            float tmp3_x = current_x;
            float tmp3_y = Mathf.Atan(current_x);
            float tmp4_x = current_x;
            float tmp4_y = Mathf.Tan(current_x) / 5;
            //GameObject a5 = Instantiate(current_bullet, transform.position + new Vector3(tmp3_x + offset_x, tmp3_y, 0), Quaternion.identity);
            //GameObject a6 = Instantiate(current_bullet, transform.position + new Vector3(tmp4_x + offset_x, tmp4_y, 0), Quaternion.identity);
            GameObject a5 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(tmp3_x + offset_x, tmp3_y, 0), Quaternion.identity);
            GameObject a6 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(tmp4_x + offset_x, tmp4_y, 0), Quaternion.identity);


            //반전
            //GameObject a7 = Instantiate(current_bullet, transform.position + new Vector3(-tmp3_x + offset_x, tmp3_y, 0), Quaternion.identity);
            //GameObject a8 = Instantiate(current_bullet, transform.position + new Vector3(-tmp4_x + offset_x, tmp4_y, 0), Quaternion.identity);
            GameObject a7 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(-tmp3_x + offset_x, tmp3_y, 0), Quaternion.identity);
            GameObject a8 = KYS_ObjectPool.instance.Instantiate(type, transform.position + new Vector3(-tmp4_x + offset_x, tmp4_y, 0), Quaternion.identity);


            a1.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 0.5f);
            a2.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 0.5f);
            a3.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 0.5f);
            a4.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 0.5f);
            a5.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 0.5f);
            a6.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 0.5f);
            a7.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 0.5f);
            a8.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(0, -1), 0.5f);


        }

        
        
    }

    
    
}
