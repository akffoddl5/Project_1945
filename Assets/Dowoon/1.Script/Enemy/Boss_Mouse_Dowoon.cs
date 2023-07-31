using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss_Mouse_Dowoon : Enemy_Dowoon
{

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
    [Header("패턴 3 포지션")]
    public Vector3 Pattern3_Start;


    public GameObject Boss_Sprite;

    Coroutine CurrPattern_Coroutine;

    public bool isPatternStart;
    float patternDelay = 1.0f;

    
    GameObject go_box;
    // Start is called before the first frame update
    void Start()
    {

        patternDelay = 2.0f;
       StartCoroutine( PatternSetter());
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
        var dist = 100f;
        while(dist >= 0.2f)
        {
            var goal = Pattern1_Start;
            var dir = Pattern1_Start - transform.position;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            dist = Vector3.Distance(goal, transform.position);

            yield return new WaitForEndOfFrame();

        }

        yield return new WaitForSeconds(0.45f);

       
        dist = 100;

        // 포인트 2로 이동 및 드래그박스 생성
        go_box = Instantiate(boxPrefab, Box_Generator.position, Quaternion.identity);
        var startPos = Box_Generator.position;

        while (dist >= 0.2f)
        {
            var goal = Pattern1_End; ;
            var dir = Pattern1_End - transform.position;

            float minX = Mathf.Abs(startPos.x - Box_Generator.position.x);
            float minY = Mathf.Abs(startPos.y - Box_Generator.position.y);

            var offset = startPos + (Box_Generator.position - startPos)/2;
            go_box.transform.position = offset;
            go_box.transform.localScale = new Vector3(minX, minY,0);

           transform.Translate(dir * moveSpeed * Time.deltaTime);
             dist = Vector3.Distance(goal, transform.position);
           
            yield return new WaitForFixedUpdate();

        }


        yield return new WaitForSeconds(0.11f);



        dist = 100;
        // 다음위치로 이동하여 드래그박스 투하 

        go_box.GetComponent<Rigidbody2D>().gravityScale = 0.6f;

        while (dist >= 0.2f)
        {
            var goal = Pattern1_DragEnd; ;
            var dir = goal - transform.position;

            transform.Translate(dir * moveSpeed * Time.deltaTime);
            dist = Vector3.Distance(goal, transform.position);

      
            yield return new WaitForFixedUpdate();
        }

        
        while (Boss_Sprite.transform.localEulerAngles.z <= 130)
        {
            Boss_Sprite.transform.Rotate(new Vector3(0, 0, 300 * Time.deltaTime));
            Debug.Log(transform.localEulerAngles.z);
            if(Boss_Sprite.transform.localEulerAngles.z >= 130)
            {
         
                break;
            }

            yield return new WaitForFixedUpdate();
        }


        yield return new WaitForSeconds(0.2f);
        var shot = StartCoroutine(Pattern1_Shot());
        dist = 100;

        while (dist >= 0.6f)
        {
            var goal = Pattern1_MoveToShot;
            var dir = goal - transform.position;
            Debug.Log("거리 :[드래그]" + dist);
            transform.Translate(dir * (moveSpeed/3) * Time.deltaTime);
            dist = Vector3.Distance(goal, transform.position);

            if (dist <= 2.0)
            {
                if(shot != null)
                StopCoroutine(shot);
            }
            yield return new WaitForSeconds(0.02f);
        }



        EndPattern(1.5f);






    }

    IEnumerator Pattern_2()
    {

        var dist = 100f;

        while (dist >= 0.2f)
        {
            var goal = Pattern2_Start;
            var dir = goal - transform.position;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            dist = Vector3.Distance(goal, transform.position);

            yield return new WaitForEndOfFrame();

        }

        yield return new WaitForSeconds(0.5f);

         dist = 100f;
        while (dist >= 0.2f)
        {
            var goal = Pattern2_End;
            var dir = goal - transform.position;
            transform.Translate(dir * moveSpeed * Time.deltaTime);
            dist = Vector3.Distance(goal, transform.position);

            yield return new WaitForEndOfFrame();

        }

        yield return new WaitForSeconds(1.5f);

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






    IEnumerator Pattern1_Shot()
    {

        while (true)
        {
            var bullet = Instantiate(bulletPrefab, Bullet_Generator.position, Quaternion.Euler(0, 0, 0));
            bullet.GetComponent<Bullet_Dowoon>().SetDirection(new Vector3(0, -1, 0));

            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator Pattern2_Shot()
    {

        while (true)
        {
            var bullet = Instantiate(bulletPrefab, Bullet_Generator.position, Quaternion.Euler(0, 0, 0));
            var bullet2 = Instantiate(bulletPrefab, Bullet_Generator.position, Quaternion.Euler(0, 0, 0));
            bullet.GetComponent<Bullet_Dowoon>().SetDirection(new Vector3(0, -1, 0));

            yield return new WaitForSeconds(0.15f);
        }
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


        patternDelay = _delay;
        isPatternStart = false;
        currPattern = BossState.Idle;
        

        StartCoroutine(PatternSetter());
    }

    IEnumerator PatternSetter()
    {

        
      
        yield return new WaitForSeconds(patternDelay);

        isPatternStart = true;

        ChangePattern();



    }

}

