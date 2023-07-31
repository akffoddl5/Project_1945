using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Tilemaps;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;
public class Boss_Mouse_Dowoon : Enemy_Dowoon
{
    public GameObject target_player;
    public enum BossState
    {
        Idle = 0,
        Pattern1,
        Pattern2,
        Pattern3,


    }

    public BossState currPattern = BossState.Idle;
    public BossState lastPattern = BossState.Idle;


    [Header("시작 ")]
    public Vector3 SpawnPos;
    public Vector3 Spawn_arrivePos;

    [Header("패턴 1 포지션")]
     public Vector3 Pattern1_Start;
     public Vector3 Pattern1_End;
    public Vector3 Pattern1_DragEnd;
    public Vector3 Pattern1_MoveToShot;
    [Header("패턴 1 Prefab")]
    public Transform Box_Generator;
    public Transform Bullet_Generator;
    public GameObject boxPrefab;
    public GameObject _boss_bulletPrefab;
    [Header("패턴 2 포지션")]
    public Vector3 Pattern2_Start;
     public Vector3 Pattern2_End;
    public Vector3 Pattern2_3;
    public Vector3 Pattern2_4;
    public Vector3 Pattern2_5;
    public Vector3 Pattern2_6;


    [Header("패턴 2 Prefab")]
    public GameObject TaskBar;
    [Header("패턴 3 포지션")]
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
    }

    // Update is called once per frame
    public override void Update()
    {
        if (!isArrive)
            GoToGoalPos();


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
        // 포인트 1로 이동
        
       
        while(true)
        {
           var v = MoveToGoal(Pattern1_Start);
            if (v <= 0.2f)
                break;

            yield return new WaitForEndOfFrame();

        }

        yield return new WaitForSeconds(0.45f);

       
  

        // 포인트 2로 이동 및 드래그박스 생성
        go_box = Instantiate(boxPrefab, Box_Generator.position, Quaternion.identity);
        Destroy(go_box, 10f);
        var startPos = Box_Generator.position;

        while ( true)
        {
            var v = MoveToGoal(Pattern1_End);
            if (v <= 0.2f)
                break;

            float minX = Mathf.Abs(startPos.x - Box_Generator.position.x);
            float minY = Mathf.Abs(startPos.y - Box_Generator.position.y);

            var offset = startPos + (Box_Generator.position - startPos)/2;
            go_box.transform.position = offset;
            go_box.transform.localScale = new Vector3(minX, minY,0);

           
            yield return new WaitForFixedUpdate();

        }


        yield return new WaitForSeconds(0.11f);



      
        // 다음위치로 이동하여 드래그박스 투하 

        go_box.GetComponent<Rigidbody2D>().gravityScale = 2.0f;

        while (true)
        {
          

            var v = MoveToGoal(Pattern1_DragEnd);
            if (v <= 0.2f)
                break;


            yield return new WaitForFixedUpdate();
        }

        
        while (Boss_Sprite.transform.localEulerAngles.z <= 130)
        {
            Boss_Sprite.transform.Rotate(new Vector3(0, 0, 300 * Time.deltaTime));
           
            if(Boss_Sprite.transform.localEulerAngles.z >= 130)
            {
         
                break;
            }

            yield return new WaitForFixedUpdate();
        }


        yield return new WaitForSeconds(0.2f);
        var shot = StartCoroutine(Pattern1_Shot());
      
        var offSetSpeed = 1.0f;
        while (true)
        {
         
            var v = MoveToGoal(Pattern1_MoveToShot, offSetSpeed);
            if (v <= 0.2f)
                break;
            if (v > 5.0f)
                offSetSpeed = 0.1f;
            else if (v <= 3.0f)
                offSetSpeed = 1f;


        

            if (v <= 2.0)
            {
                if(shot != null)
                StopCoroutine(shot);
            }
            yield return new WaitForFixedUpdate();
        }



        EndPattern(1.5f);






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

        //크롬 집으러감
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

        // 크롬 집고 내려옴 
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
        var shot = StartCoroutine(Pattern2_Shot());


        //TO DO :좌우로 움직이는 코루틴 실행 하기
        // 좌 우로 움직이는 코루틴 종료 시 다음 실행 
        //  보스들 패턴에 캐릭터가 맞으면 죽게하기 
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

        yield return new WaitForSeconds(1212.1f);

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
            Debug.Log("거리 : " + dist);
            yield return new WaitForSeconds(0.1f);

        }

        yield return null;
    }

    IEnumerator MoveSide()
    {
        yield return new WaitForSeconds(0.4f);


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


    IEnumerator Pattern2_Shot()
    {
        List<Transform> bullets = new List<Transform>();
        while (true)
        {

            for (int i = 0; i < 360; i += 13)
            {
                //총알 생성
                GameObject temp = Instantiate(bulletPrefab);

                //2초후 삭제
                Destroy(temp, 4f);

                //총알 생성 위치를 (0,0) 좌표로 한다.
                temp.transform.position = Bullet_Generator.transform.position;

                //?초후에 Target에게 날아갈 오브젝트 수록
                bullets.Add(temp.transform);

                //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
                temp.transform.rotation = Quaternion.Euler(0, 0, i);

                

              
            }
            StartCoroutine(BulletToTarget(bullets));
            yield return new WaitForSeconds(1.4f);
            //총알을 Target 방향으로 이동시킨다.

        }
    }

    private IEnumerator BulletToTarget(IList<Transform> objects)
    {
        //0.5초 후에 시작
        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < objects.Count; i++)
        {
            //현재 총알의 위치에서 플레이의 위치의 벡터값을 뻴셈하여 방향을 구함
            Vector3 targetDirection = target_player.transform.position - objects[i].position;

            //x,y의 값을 조합하여 Z방향 값으로 변형함. -> ~도 단위로 변형
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            //Target 방향으로 이동
            objects[i].rotation = Quaternion.Euler(0, 0, angle);
            objects[i].GetComponent<Bullet_Dowoon>().b_localDirection = true;
        }

        //데이터 해제
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
        Debug.Log("패턴 끝" + currPattern);

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

}

