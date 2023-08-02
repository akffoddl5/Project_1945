using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;
public class Boss_Mouse_Dowoon : Enemy_Dowoon
{
    public GameObject target_player;
    public GameObject HpBar;
    public GameObject HpBarObject;
    public enum BossState
    {
        Idle = 0,
        Pattern1,
        Pattern2,
        Pattern3,


    }

    public BossState currPattern = BossState.Idle;
    public BossState lastPattern = BossState.Idle;


    [Header("���� ")]
    public Vector3 SpawnPos;
    public Vector3 Spawn_arrivePos;

    [Header("���� 1 ������")]
    public Vector3 Pattern1_Start;
    public Vector3 Pattern1_End;
    public Vector3 Pattern1_DragEnd;
    public Vector3 Pattern1_MoveToShot;
    public Vector3 Pattern1_SpinShotPos;
    public Vector3 Pattern1_SpinDragStart;
    public Vector3 Pattern1_SpinDragEnd;
    public Vector3 Pattern1_DragMoveStartPos;
    public Vector3 Pattern1_DragMove_1;
    public Vector3 Pattern1_DragMove_2;
    public Vector3 Pattern1_DragMove_3;
    public Vector3 Pattern1_DragMove_4;

    [Header("���� 1 Prefab")]
    public Transform Box_Generator;
    public Transform Bullet_Generator;
    public GameObject boxPrefab;
    public GameObject _boss_bulletPrefab;
    [Header("���� 2 ������")]
    public Vector3 Pattern2_Start; // ���Ͻ��� �巡��
    public Vector3 Pattern2_End; // �巡�� ��
    public Vector3 Pattern2_3; // ũ��
    public Vector3 Pattern2_4; // ��� 
    public Vector3 Pattern2_5; // �»��
    public Vector3 Pattern2_6; // ����  5/6�ݺ�
    public Vector3 Pattern2_7; // ���� [�½�ũ��] 
    public Vector3 Pattern2_8; // �½�ũ�� �ٷ� �Ʒ�  7/8�ݺ�
    public Vector3 Pattern2_9; // �½�ũ�� ���� [������]
    public Vector3 Pattern2_10; // ũ�� ��� �Ʒ� �Ѿ˹߻� 




    [Header("���� 2 Prefab")]
    public GameObject TaskBar;
    [Header("���� 3 ������")]
    public Vector3 Pattern3_Start;


    public GameObject Boss_Sprite;

    Coroutine CurrPattern_Coroutine;

    public bool isPatternStart;
    float patternDelay = 1.0f;


    GameObject go_box;


    // Start is called before the first frame update
    public override void Start()
    {
        target_player = GameObject.FindGameObjectWithTag("Player");
        patternDelay = 2.0f;
        StartCoroutine(PatternSetter());

        renderer = Boss_Sprite.GetComponent<SpriteRenderer>();
        // StartCoroutine(Pattern1_Shot());

        var Canvas = GameObject.FindGameObjectWithTag("Canvas");
        HpBarObject = Instantiate(HpBar, Canvas.transform);
        HpBarObject.GetComponent<Slider>().value = (float)hp / maxHp;

        moveSpeed = 4.0f;

    }

    // Update is called once per frame
    public override void Update()
    {
        if (!isArrive)
            GoToGoalPos();


        SetHpValue();
        //if(!isPatternStart && Boss_Sprite.transform.localEulerAngles.z >= 0)
        //{

        //    Boss_Sprite.transform.Rotate(new Vector3(0, 0, -330 * Time.deltaTime));

        //    if(Boss_Sprite.transform.localEulerAngles.z <= 0)
        //    {
        //        var local = Boss_Sprite.transform.localEulerAngles;
        //        local.z = 0;
        //        Boss_Sprite.transform.localEulerAngles = local;
        //    }


        //}



    }

    IEnumerator Pattern_1()
    {
        // ����Ʈ 1�� �̵�


        while (true)
        {
            var v = MoveToGoal(Pattern1_Start);
            if (v <= 0.2f)
                break;

            yield return new WaitForEndOfFrame();

        }


        // ����Ʈ 2�� �̵� �� �巡�׹ڽ� ����
        go_box = Instantiate(boxPrefab, Box_Generator.position, Quaternion.identity);
        Destroy(go_box, 10f);
        var startPos = Box_Generator.position;

        while (true)
        {
            var v = MoveToGoal(Pattern1_End);
            if (v <= 0.2f)
                break;

            float minX = Mathf.Abs(startPos.x - Box_Generator.position.x);
            float minY = Mathf.Abs(startPos.y - Box_Generator.position.y);

            var offset = startPos + (Box_Generator.position - startPos) / 2;
            go_box.transform.position = offset;
            go_box.transform.localScale = new Vector3(minX, minY, 0);


            yield return new WaitForEndOfFrame();

        }


        yield return new WaitForSeconds(0.11f);




        // ������ġ�� �̵��Ͽ� �巡�׹ڽ� ���� 

        go_box.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

        while (true)
        {


            var v = MoveToGoal(Pattern1_DragEnd);
            if (v <= 0.2f)
                break;


            yield return new WaitForEndOfFrame();
        }


        while (Boss_Sprite.transform.localEulerAngles.z <= 130)
        {
            Boss_Sprite.transform.Rotate(new Vector3(0, 0, 300 * Time.deltaTime));

            if (Boss_Sprite.transform.localEulerAngles.z >= 130)
            {

                break;
            }

            yield return new WaitForEndOfFrame();
        }


        yield return new WaitForSeconds(0.2f);
        var shot = StartCoroutine(Pattern1_Shot());

        var offSetSpeed = 1.0f;
        while (true)
        {

            var v = MoveToGoal(Pattern1_MoveToShot,offSetSpeed);
            if (v <= 0.2f)
                break;
            if (v > 3.0f)
                offSetSpeed = 0.3f;
            else if (v <= 2.3f)
                offSetSpeed = 1f;




            if (v <= 2.0)
            {
                if (shot != null)
                    StopCoroutine(shot);
            }
            yield return new WaitForFixedUpdate();
        }



        while (true)
        {

            var v = MoveToGoal(Pattern1_SpinShotPos);
            if (v <= 0.2f)
            {

                break;

            }

            yield return new WaitForEndOfFrame();
        }

        List<Transform> bullets = new List<Transform>();
        yield return StartCoroutine(Pattern1_Shot2());




        EndPattern(1.5f);






    }

    IEnumerator Pattern1_Shot2()
    {

        List<Transform> bullets = new List<Transform>();

        for (int i = 0; i < 40; ++i)
        {

            //�Ѿ� ����
            GameObject temp = Instantiate(bulletPrefab);
            GameObject temp2 = Instantiate(bulletPrefab);
            GameObject temp3 = Instantiate(bulletPrefab);
            GameObject temp4 = Instantiate(bulletPrefab);
            //2���� ����
            Destroy(temp, 35f);
            Destroy(temp2, 35f);
            Destroy(temp3, 35f);
            Destroy(temp4, 35f);
            //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
            temp.transform.position = Bullet_Generator.transform.position;
            temp2.transform.position = Bullet_Generator.transform.position;
            temp3.transform.position = Bullet_Generator.transform.position;
            temp4.transform.position = Bullet_Generator.transform.position;

            //?���Ŀ� Target���� ���ư� ������Ʈ ����
            bullets.Add(temp.transform);
            bullets.Add(temp2.transform);
            bullets.Add(temp3.transform);
            bullets.Add(temp4.transform);


            var offset = 15f;
            if (i % 2 == 0)
            {
                offset = 15f;



            }
            else if ( i % 2 == 1)
            {
                offset = -22f;
            }

            temp.transform.rotation = Quaternion.Euler(0, 0, 45 + (i * offset)+i);
            temp2.transform.rotation = Quaternion.Euler(0, 0, 135 + (i * offset)+i);
            temp3.transform.rotation = Quaternion.Euler(0, 0, 225 + (i * offset)-i);
            temp4.transform.rotation = Quaternion.Euler(0, 0, 315 + (i * offset)-i);

            temp.gameObject.tag = "Obstacle";
            temp2.gameObject.tag = "Obstacle";
            temp3.gameObject.tag = "Obstacle";
            temp4.gameObject.tag = "Obstacle";

             var t = 2.0f - (i * 0.05f);
            //var t = 2.0f;
           StartCoroutine(temp.GetComponent<Bullet_Dowoon>().StopBullet(t));
            StartCoroutine(temp2.GetComponent<Bullet_Dowoon>().StopBullet(t));
            StartCoroutine(temp3.GetComponent<Bullet_Dowoon>().StopBullet(t));
            StartCoroutine(temp4.GetComponent<Bullet_Dowoon>().StopBullet(t));

            var bulletTime = 0.1f + (i * 0.01f);
         //  var bulletTime = 0.05f;
            yield return new WaitForSeconds(bulletTime);
        } 

        yield return new WaitForSeconds(1.0f);

        while (true)
        {

            var v = MoveToGoal(Pattern1_SpinDragStart);
            if (v <= 0.2f)
            {

                break;

            }
        }

        go_box = Instantiate(boxPrefab, Box_Generator.position, Quaternion.identity);
        Destroy(go_box, 10f);
        var startPos = Box_Generator.position;

        while (true)
        {
            var v = MoveToGoal(Pattern1_SpinDragEnd);
            if (v <= 0.2f)
                break;

            float minX = Mathf.Abs(startPos.x - Box_Generator.position.x);
            float minY = Mathf.Abs(startPos.y - Box_Generator.position.y);

            var offset = startPos + (Box_Generator.position - startPos) / 2;
            go_box.transform.position = offset;
            go_box.transform.localScale = new Vector3(minX, minY, 0);


            yield return new WaitForEndOfFrame();

        }




        while (true)
        {
            var v = MoveToGoal(Pattern1_DragMoveStartPos, 1.0f);
            if (v <= 0.2f)
                break;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.73f);


        Destroy(go_box);

        for (int i = 0; i < bullets.Count; ++i)
        {
            if (bullets[i])
            {
                bullets[i].GetComponent<SpriteRenderer>().color = Color.green;
                bullets[i].GetComponent<Bullet_Dowoon>().bullet_Speed = 0.0f;
                bullets[i].transform.parent = transform;
            }
        }

        yield return new WaitForSeconds(0.33f);

        
        yield return StartCoroutine(RotateMouse(Pattern1_DragMove_1, bullets));
 
        while (true)
        {
            var v = MoveToGoal(Pattern1_DragMove_1,0.2f);
            if (v <= 0.2f)
                break;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.33f);


        yield return StartCoroutine(RotateMouse(Pattern1_DragMove_2, bullets));



        while (true)
        {
            var v = MoveToGoal(Pattern1_DragMove_2, 0.2f);
            if (v <= 0.2f)
                break;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.33f);

        yield return StartCoroutine(RotateMouse(Pattern1_DragMove_3, bullets));

        while (true)
        {
            var v = MoveToGoal(Pattern1_DragMove_3, 0.2f);
            if (v <= 0.2f)
                break;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.33f);


        yield return StartCoroutine(RotateMouse(Pattern1_DragMove_4, bullets));

        while (true)
        {
            var v = MoveToGoal(Pattern1_DragMove_4, 0.2f);
            if (v <= 0.2f)
                break;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(0.33f);



        for (int i= 0; i< bullets.Count; ++i)
        {
            if (bullets[i])
            {
                bullets[i].GetComponent<SpriteRenderer>().color = Color.red;
                bullets[i].GetComponent<Bullet_Dowoon>().bullet_Speed = 3.5f;
                bullets[i].transform.parent = null;
                Destroy(bullets[i].gameObject, 3f);
            }
        }


        yield return new WaitForSeconds(0.01f);

    }

    void SetParents(List<Transform> bullet,bool b)
    {
        for(int i=0; i< bullet.Count; ++i)
        {
            if (bullet[i] != null)
            {
                if (b)
                    bullet[i].transform.parent = transform;
                else
                    bullet[i].transform.parent = null;
            }
        }
    }

    IEnumerator RotateMouse(Vector3 lookPos, List<Transform> bullet)
    {
        float zOffset = 760;
        float t = Random.Range(1.2f, 2.2f);
        SetParents(bullet, false);
        while(true)
        {
            t -= Time.deltaTime;
            Boss_Sprite.transform.Rotate(new Vector3(0, 0, zOffset * Time.deltaTime));
             
          

            if(t <= 0)
            {
                var dir = lookPos - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                Boss_Sprite.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
                break;
            }
               
            
            

            yield return new WaitForEndOfFrame();
        }


        SetParents(bullet, true);

        yield return new WaitForSeconds(0.45f);


    }
    
    IEnumerator Pattern_2()
    {

        

        while (true)
        {
           var v = MoveToGoal(Pattern2_Start);
            if(v <= 0.2f)
            {
                break;
            }

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        var bar = Instantiate(TaskBar, Box_Generator.position, Quaternion.identity);
        bar.transform.parent = Box_Generator.transform;

       
        while(true)
        {
            var v = MoveToGoal(Pattern2_End);
            if (v <= 0.2f)
            {
                break;
            }

            yield return new WaitForEndOfFrame();

        }

        yield return new WaitForSeconds(0.1f);

        bar.transform.parent = null;

        //ũ�� ��������
        while (true)
        {
            var v = MoveToGoal(Pattern2_3,0.3f);
            if (v <= 0.2f)
            {
                break;
            }

            yield return new WaitForEndOfFrame();

        }
        yield return new WaitForSeconds(0.1f);

        var chrome = bar.GetComponent<TaskBar_Dowoon>().Icon_Chrome;

        chrome.transform.parent = transform;

        // ũ�� ���� ������ 
        while (true)
        {
            var v = MoveToGoal(Pattern2_4);
            if (v <= 0.2f)
            {
                break;
            }

            yield return new WaitForEndOfFrame();

        }


        yield return new WaitForSeconds(0.1f);

        chrome.transform.parent = null;
      var rotateStart =   StartCoroutine(chrome.GetComponent<Chrome_Dowoon>().RotateStart());
      


        //TO DO :�¿�� �����̴� �ڷ�ƾ ���� �ϱ�
        // �� ��� �����̴� �ڷ�ƾ ���� �� ���� ���� 
        //  ������ ���Ͽ� ĳ���Ͱ� ������ �װ��ϱ� 
         while (true)
         {
            var v = MoveToGoal(Pattern2_5);
      
            if (v <= 0.3f)
            { 
                var local = Boss_Sprite.transform.localEulerAngles;
                local.z = 130;
                Boss_Sprite.transform.localEulerAngles = local;
                break;
            }
            yield return new WaitForEndOfFrame();
         }

        yield return new WaitForSeconds(1f);

        var shot = StartCoroutine(Pattern2_Shot());
    

        yield return StartCoroutine(MoveSide(Pattern2_6,Pattern2_5,2.3f,4));
        StopCoroutine(shot);


       

        var s = Boss_Sprite.transform.localEulerAngles;
        s.z = 0;
        Boss_Sprite.transform.localEulerAngles = s;

        while (true)
        {
            var v = MoveToGoal(Pattern2_10);

            if (v <= 0.3f)
            {
                
                break;
            }
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);
        


        yield return StartCoroutine(Pattern2_Shot2());


        bar.transform.parent = Box_Generator.transform;
        var barPos = bar.transform.localPosition;
        // barPos.x = -8.3f;
        barPos.y = -0.5f;
        bar.transform.localPosition  = barPos;

        yield return StartCoroutine(MoveSide(Pattern2_8, Pattern2_7, 0.08f, 4));


        var bar_Mail = bar.GetComponent<TaskBar_Dowoon>().Icon_Mail;
        var bar_Shop = bar.GetComponent<TaskBar_Dowoon>().Icon_Shop;

        while(true)
        {
            var v = MoveToGoal(Pattern2_9);

            if(v <= 0.2f)
            {
                bar.transform.parent = null;
                bar.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
                bar.GetComponent<Rigidbody2D>().AddForce(-Vector3.right * 2.5f, ForceMode2D.Impulse);

                var barlocal = bar.transform.localEulerAngles;
                barlocal.z = 35;
                bar.transform.localEulerAngles = barlocal;

                bar_Shop.GetComponent<Rigidbody2D>().AddForce(-Vector2.right * 1.1f, ForceMode2D.Impulse);
                bar_Shop.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

                bar_Mail.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 3.1f, ForceMode2D.Impulse);
                bar_Mail.GetComponent<Rigidbody2D>().gravityScale = 1.0f;

                Destroy(bar, 6f);
                chrome.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                break;
            }


            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2.0f);


        EndPattern(1.0f);


    }




    IEnumerator Pattern_3()
    {

        var dist = 100f;
        while (dist >= 0.2f)
        {
            var goal = Pattern1_Start;
            var dir = Pattern1_Start - transform.position;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            dist = Vector3.Distance(Pattern1_Start, transform.position);
            Debug.Log("�Ÿ� : " + dist);
            yield return new WaitForSeconds(0.1f);

        }

        yield return null;
    }

    IEnumerator MoveSide(Vector3 goal1, Vector2 goal2, float moveTime, int _moveCount)
    {
        var isRight = true;
        var dist = 0.0f;
        int moveCount = _moveCount;
        while (moveCount >= 0)
        {
            if(isRight)
            {
                dist = MoveToGoal(goal2);

                if(dist <= 0.2f)
                {
                    yield return new WaitForSeconds(moveTime);
                    isRight = false;
                    moveCount--;
                }
            }
            else
            {
                dist = MoveToGoal(goal1);

                if (dist <= 0.2f)
                {
                    yield return new WaitForSeconds(moveTime);
                    isRight = true;
                    moveCount--;
                }      
            }
            yield return new WaitForEndOfFrame();
        }


    }
    float MoveToGoal(Vector3 GoalPos)
    {
        var dist = 100f;
       
        var dir = GoalPos - transform.position;
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        dist = Vector3.Distance(GoalPos, transform.position);
        

        return dist;
    }

    float MoveToGoal(Vector3 GoalPos, float offset)
    {
        var dist = 100f;

        var dir = GoalPos - transform.position;
        transform.Translate(dir * moveSpeed * offset * Time.deltaTime);

        dist = Vector3.Distance(GoalPos, transform.position);


        return dist;
    }

    IEnumerator Pattern1_Shot()
    {
        GameObject[] bullets = new GameObject[30];
        for(int i=0; i< bullets.Length;i++)
        {
            bullets[i] = Instantiate(bulletPrefab, Bullet_Generator.position, Quaternion.Euler(0, 0, 0));
            bullets[i].SetActive(false);
        }


        for( int i=0; i< bullets.Length; ++i)
        {

            bullets[i].SetActive(true);
            bullets[i].GetComponent<Bullet_Dowoon>().SetDirection(new Vector3(0, -1, 0));
            bullets[i].transform.position = Bullet_Generator.position;
            yield return new WaitForSeconds(0.12f);
           

        }
    }

    IEnumerator Pattern2_Shot2()
    {
        int count = 2;
        List<Transform> bullets = new List<Transform>();
        while (count >0)
        {
            for (int i = 0; i < 40; i += 2)
            {

                //�Ѿ� ����
                GameObject temp = Instantiate(bulletPrefab);
                GameObject temp2 = Instantiate(bulletPrefab);
                //2���� ����
                Destroy(temp, 15f);
                Destroy(temp2, 15f);
                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                temp.transform.position = Bullet_Generator.transform.position;
                temp2.transform.position = Bullet_Generator.transform.position;
               
                //?���Ŀ� Target���� ���ư� ������Ʈ ����
                bullets.Add(temp.transform);
                bullets.Add(temp2.transform);
                //Z�� ���� ���ؾ� ȸ���� �̷�����Ƿ�, Z�� i�� �����Ѵ�.
                temp.transform.rotation = Quaternion.Euler(0, 0, i*5);
                temp2.transform.rotation = Quaternion.Euler(0, 0, (i*5)+180);

                temp.GetComponent<Bullet_Dowoon>().bullet_Speed = 2.2f;
                temp2.GetComponent<Bullet_Dowoon>().bullet_Speed = 2.2f;

                yield return new WaitForSeconds(0.15f);
            }

            StartCoroutine(BulletToTarget(bullets, 1.0f));
            yield return new WaitForSeconds(2.0f);

            count--;
        }
        
    }


    IEnumerator Pattern2_Shot()
    {
        List<Transform> bullets = new List<Transform>();
        while (true)
        {

            for (int i = 0; i < 360; i += 13)
            {
                //�Ѿ� ����
                GameObject temp = Instantiate(bulletPrefab);

                //2���� ����
                Destroy(temp, 4f);

                //�Ѿ� ���� ��ġ�� (0,0) ��ǥ�� �Ѵ�.
                temp.transform.position = Bullet_Generator.transform.position;
                Destroy(temp, 5f);
                //?���Ŀ� Target���� ���ư� ������Ʈ ����
                bullets.Add(temp.transform);
                
                //Z�� ���� ���ؾ� ȸ���� �̷�����Ƿ�, Z�� i�� �����Ѵ�.
                temp.transform.rotation = Quaternion.Euler(0, 0, i);

                

              
            }
            StartCoroutine(BulletToTarget(bullets,0.35f));
            yield return new WaitForSeconds(4.0f);
            //�Ѿ��� Target �������� �̵���Ų��.

        }
    }

    private IEnumerator BulletToTarget(IList<Transform> objects, float shotTime)
    {
        //0.5�� �Ŀ� ����
        yield return new WaitForSeconds(shotTime);

        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i] != null)
            {
                //���� �Ѿ��� ��ġ���� �÷����� ��ġ�� ���Ͱ��� �y���Ͽ� ������ ����
                Vector3 targetDirection = target_player.transform.position - objects[i].position;

                //x,y�� ���� �����Ͽ� Z���� ������ ������. -> ~�� ������ ����
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

                //Target �������� �̵�
                objects[i].rotation = Quaternion.Euler(0, 0, angle);
                objects[i].GetComponent<Bullet_Dowoon>().b_localDirection = true;
                objects[i].GetComponent<Bullet_Dowoon>().bullet_Speed = 3.5f;                
            }
        }

        //������ ����
        objects.Clear();
    }
    private void ChoosePattern()
    {
        while (true)
        {
            int randomNum = Random.Range((int)BossState.Idle, (int)BossState.Pattern3);
                        
           
            switch (randomNum)
            {
                case 0:
                    currPattern = BossState.Pattern1;
                    break;
                case 1:
                case 2:
                    currPattern = BossState.Pattern2;

                    break;
            }

            if (lastPattern != currPattern)
            {
                lastPattern = currPattern;
                break;
            }

        }
        ChangePattern();
    }
    void ChangePattern()
    {
        IEnumerator IE_coroutine = null;
        switch (currPattern)
        {
            case BossState.Idle:

                ChoosePattern();

                break;
            case BossState.Pattern1:
                IE_coroutine = Pattern_1();
                break;
            case BossState.Pattern2:
                IE_coroutine = Pattern_2();
                break;
            case BossState.Pattern3:
                IE_coroutine = Pattern_3();
                break;
            default:
                break;

        }

        if (IE_coroutine != null)
        {
            CurrPattern_Coroutine = StartCoroutine(IE_coroutine);

        }
    }

    void EndPattern(float _delay)
    {
        Debug.Log("���� ��" + currPattern);

        patternDelay = _delay;
        isPatternStart = false;

        currPattern = BossState.Idle;


        var rot = Boss_Sprite.transform.localEulerAngles;
        rot.z = 0;
        Boss_Sprite.transform.localEulerAngles = rot;

        StartCoroutine(PatternSetter());
    }

    IEnumerator PatternSetter()
    {

        
      
        yield return new WaitForSeconds(patternDelay);

        isPatternStart = true;

        ChangePattern();



    }

    public void SetHpValue()
    {
        HpBarObject.GetComponent<Slider>().value = (float)hp / maxHp;
    }
}

