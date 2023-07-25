using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class KYS_Furin_bullet : MonoBehaviour
{
    public GameObject normal_bullet;
    public GameObject guide_bullet;
    

    private void Start()
    {
        //StartCoroutine(IE_Shoot_Cycle());
        StartCoroutine(IE_Shoot_Controll());


        //InvokeRepeating("Shoot_Fork", 2f,2f);        
        //StartCoroutine(IE_Shoot_Fork());
        //StartCoroutine(IE_Shoot_Circle());
    }

    int sides = 4; // �ٰ����� ������ ����
    float radius = 1.5f; // �ٰ����� ������
    float speed = 5f;
    float fireRate = 0.5f; // �߻� �ӵ�

    float angle = 0f;
    float fireTimer = 0f;
    private void Update()
    {

        //Debug.Log("tt" + fireRate + " " + fireTimer);
        fireTimer += Time.deltaTime;
        //Debug.Log("tt" + fireRate + " " + fireTimer);

        if (fireTimer >= fireRate && 1==2)
        {
            // �ð��� ���� �������� ���� ��� (2 * PI�� sides�� ������ �յ��ϰ� ������ ������ ����)
            float angleIncrement = 2f * Mathf.PI / sides;
            angle += angleIncrement;

            // �ٰ����� ������ ��ǥ ��� (���� ���������� ������ ���� �̵�)
            float x = radius * Mathf.Cos(angle);
            float y = radius * Mathf.Sin(angle);

            // ������ ��ǥ�� �̿��Ͽ� ���� ����
            Vector2 direction = new Vector2(x, y).normalized;

            //Debug.Log(Quaternion.LookRotation(new Vector3(direction.x, direction.y, 0)).eulerAngles);
            Debug.Log(direction);
            // �Ѿ� ����
            GameObject bullet = Instantiate(normal_bullet, transform.position, Quaternion.identity);
            bullet.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(direction, 1f);
            //bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;

            // Ÿ�̸� �ʱ�ȭ
            fireTimer = 0f;
        }
    }

    IEnumerator IE_Shoot_Controll()
    {
        StartCoroutine(IE_Shoot_Fork());
        yield return new WaitForSeconds(3.0f);
        StartCoroutine(IE_Shoot_Circle());
        yield return new WaitForSeconds(3.0f);
        while (true)
        {
            StartCoroutine(IE_Shoot_Cycle());
            yield return new WaitForSeconds(3.0f);

        }
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

    IEnumerator IE_Shoot_Fork()
    {
        while (true)
        {
            Shoot_Fork();
            yield return new WaitForSeconds(0.2f);
            Shoot_Fork();
            yield return new WaitForSeconds(0.2f);
            Shoot_Fork();
            yield return new WaitForSeconds(0.2f);
            Shoot_Fork();
            yield return new WaitForSeconds(0.2f);
            Shoot_Fork();
            yield return new WaitForSeconds(0.2f);
            Shoot_Fork();
            yield return new WaitForSeconds(5f);
        }
    }

    //������ ���
    public void Shoot_Circle()
    {
        int point = 40;
        float angle = 360.0f / point;   //z�� ȸ���� 180���� ����

        for (int i = 0; i < point; i++)
        {
            //GameObject tmp = Instantiate(normal_bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle * i)));
            float dir_x = Mathf.Cos(angle * i / 180 * Mathf.PI);
            float dir_y = Mathf.Sin(angle * i / 180 * Mathf.PI);
            GameObject tmp = Instantiate(normal_bullet, transform.position, Quaternion.identity);
            tmp.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(dir_x, dir_y), 1f);
            //Debug.Log(angle * i + " <>  " + point + "  " + i + " " + tmp.name + " " + tmp.transform.rotation);
        }
    }

    //�Ʒ� ���������� ���
    public void Shoot_Fork()
    {
        float angle = 30;
        for (int i = 1; i <= 3; i++)
        {
            float dir_x = Mathf.Cos((210 + angle * i) / 180 * Mathf.PI);
            float dir_y = Mathf.Sin((210 + angle * i) / 180 * Mathf.PI);
            Debug.Log(dir_x + " " + dir_y);
            GameObject tmp = Instantiate(normal_bullet, transform.position, Quaternion.identity);
            tmp.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(new Vector2(dir_x, dir_y), 1f);
            //GameObject tmp = Instantiate(normal_bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 60 + 15 * i)));
            //Debug.Log(angle * i + " <>  " + point + "  " + i + " " + tmp.name + " " + tmp.transform.rotation);
        }
    }

    IEnumerator IE_Shoot_Cycle()
    {
        int point = 60;
        float angle = 360 / 30;
        float current_angle = 0;
        for (int i = 0; i < point; i++) {
            current_angle += angle;
            float dir_x = Mathf.Cos(current_angle / 180.0f * Mathf.PI);
            float dir_y = Mathf.Sin(current_angle / 180.0f * Mathf.PI);
            Shoot_Cycle(new Vector3(dir_x,dir_y,0));
            yield return new WaitForSeconds(0.02f);
        }
        
       
        
    }

    //ȸ���ϸ鼭 ���
    public void Shoot_Cycle(Vector3 dir)
    {
        GameObject tmp = Instantiate(normal_bullet, transform.position, Quaternion.identity);
        tmp.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(dir,1f);
    }

    //��Ʈ������� ���
    public void Shoot_Heart()
    {
        //��Ʈ��
        //r = (1- |t|)(1+3|t|)    -1 < t < 1
        
        
        

    }

    IEnumerator IE_Shoot_Square()
    {
        while (true)
        {
            Shoot_Square();
            yield return new WaitForSeconds(0.2f);
            Shoot_Square();
            yield return new WaitForSeconds(0.2f);
            Shoot_Square();
            yield return new WaitForSeconds(0.2f);
            Shoot_Square();
            yield return new WaitForSeconds(0.2f);
            Shoot_Square();
            yield return new WaitForSeconds(0.2f);
            Shoot_Square();
            yield return new WaitForSeconds(5f);
        }
    }


    //�׸������� ���
    public void Shoot_Square()
    {
        int side = 4;
        float angle = 360.0f / side;
        int point = 10;
        int radius = 5;
        float current_angle = 0;
        float current_angle2 = 0;
        for (int i = 0; i < side; i++)
        {
            //90 180 270 360//
            current_angle += angle;
            current_angle2 = 0;
            //if (i != 0) continue;
            for (int j = 0; j < point; j++)
            {
                //-9 -18 - 27
                current_angle2 = -1.0f * j * angle / (float)point;
                //81 ~0
                float cal_angle = angle + current_angle2;
                float scalar = radius * Mathf.Cos(cal_angle * Mathf.PI / 180);
                scalar = Mathf.Abs(scalar);

                Vector2 dir = new Vector2(Mathf.Cos(current_angle+ current_angle2 * Mathf.PI / 180), Mathf.Sin(current_angle+ current_angle2 * Mathf.PI / 180));
                Vector3 tmp2 = new Vector3(dir.x, dir.y, 0) * scalar;
                //Debug.Log(scalar + " " + dir + " " + cal_angle + " " + current_angle2);

                GameObject tmp = Instantiate(normal_bullet, transform.position + tmp2, Quaternion.identity);
                tmp.GetComponent<KYS_Enemy_Bullet_normal>().Shoot(dir, 0.5f) ;
            }
        }
        //for (int i = 0; i < point; i++)
        //{
        //    GameObject tmp = Instantiate(normal_bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, angle * i)));
        //    Debug.Log(angle * i + " <>  " + point + "  " + i + " " + tmp.name + " " + tmp.transform.rotation);
        //}
    }

}