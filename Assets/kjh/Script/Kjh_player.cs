using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class Kjh_player : MonoBehaviour
{

    float moveX;
    float moveY;
    public float movespeed = 1.3f;
    Vector3 move;
    public float boost = 50.0f;

    Animator anim;
    bool stop = true;
    bool stopTimes = true;



    float dashTime = 0;
    bool dash = false;
    bool dashswich = false;


    float BoostTime = 0;



    float CoolTime = 0;
    bool coolTime = true;

    //public GameObject bulletpos;
    //public GameObject pbullet;



    void Start()
    {

        anim = GetComponent<Animator>();

    }
    //private void bullet()
    //{

    //    for (float i = 2; i < 6; i++)
    //    {
    //        Quaternion quaternion = Quaternion.Euler(0f, 0f, 22.5f*i);
    //        Instantiate(pbullet,bulletpos.transform.position ,quaternion);

    //    }

    //}

    void Update()
    {
        Move();

        TimeAcction();
        Key();
        Movetree();
        //bullet();


        //dashTime += Time.deltaTime;
        //if (dashTime >= 1)
        //{
        //    dash = false;


        //}




    }

    public void Move()
    {
        if (stop == true)
        {


            moveX = Input.GetAxis("Horizontal") * Time.deltaTime * movespeed;//x,y버튼누르면 각각 숫자+ -1 나옴
            moveY = Input.GetAxis("Vertical") * Time.deltaTime * movespeed;

            Boost();

            move = new Vector3(moveX, moveY, 0);


            transform.Translate(move);


        }
    }

    //public void Boost()//부스트(주는 함수
    //{
    //    moveX = Input.GetAxis("Horizontal") * Time.deltaTime;
    //    moveY = Input.GetAxis("Vertical") * Time.deltaTime;
    //    if (coolTime == true)
    //    {

    //        if (Input.GetKeyDown(KeyCode.LeftShift) && (moveX >= -0.5))
    //        {
    //            for (BoostTime = 0; BoostTime >= 1; BoostTime += 0.4f)
    //            {
    //                moveX *= boost;
    //                coolTime = false;


    //                Instantiate(gameObject, gameObject.transform.position/3, Quaternion.identity);
    //                for (CoolTime = 0; CoolTime >= 3; BoostTime += Time.deltaTime)
    //                {

    //                    coolTime = true;

    //                }
    //            }

    //        }

    //        else if (Input.GetKeyDown(KeyCode.LeftShift) && (moveX <= 0.5))
    //        {
    //            for (BoostTime = 0; BoostTime >= 1; BoostTime += 0.4f)
    //            {
    //                moveX *= boost;
    //                coolTime = false;

    //                Instantiate(gameObject, gameObject.transform.position/3, Quaternion.identity);
    //                for (CoolTime = 0; CoolTime >= 3; BoostTime += Time.deltaTime)
    //                {

    //                    coolTime = true;

    //                }
    //            }


    //        }


    //        if (Input.GetKeyDown(KeyCode.LeftShift) && (moveY >= -0.5))
    //        {
    //            for (BoostTime = 0; BoostTime >= 1; BoostTime += 0.4f)
    //            {
    //                moveX *= boost;
    //                coolTime = false;

    //                Instantiate(gameObject, gameObject.transform.position/3, Quaternion.identity);
    //                for (CoolTime = 0; CoolTime >= 3; BoostTime += Time.deltaTime)
    //                {

    //                    coolTime = true;

    //                }
    //            }

    //        }


    //        else if (Input.GetKeyDown(KeyCode.LeftShift) && (moveY <= 0.5))
    //        {

    //            for (BoostTime = 0; BoostTime >= 1; BoostTime += 0.4f)
    //            {
    //                moveX *= boost;
    //                coolTime = false;

    //                Instantiate(gameObject, gameObject.transform.position/3, Quaternion.identity);
    //                for (CoolTime = 0; CoolTime >= 3; BoostTime += Time.deltaTime)
    //                {
    //                    coolTime = true;


    //                }

    //            }

    //        }
    //    }
    //}


    public void Boost()//부스트(주는 함수
    {
        moveX = Input.GetAxis("Horizontal") * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * Time.deltaTime;
        if (coolTime == true)
        {

            if (Input.GetKeyDown(KeyCode.LeftShift) && (moveX >= -0.5))
            {
                moveX *= boost;

            }

            else if (Input.GetKeyDown(KeyCode.LeftShift) && (moveX <= 0.5))
            {
                moveX *= boost;


            }


            if (Input.GetKeyDown(KeyCode.LeftShift) && (moveY >= -0.5))
            {
                moveY *= boost;

            }


            else if (Input.GetKeyDown(KeyCode.LeftShift) && (moveY <= 0.5))
            {

                moveY *= boost;
            }
        }
    }


    void Movetree()//총을 쏘는방향
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");



        if ((Input.GetAxis("Horizontal")) >= 0.5f)
        {

            anim.SetFloat("Horizontal", moveX);

        }
        else if ((Input.GetAxis("Horizontal")) <= -0.5f)
        {

            anim.SetFloat("Horizontal", moveX);

        }
        if (!(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            anim.SetFloat("Horizontal", 0);
        }



        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("Swhich", true);

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    anim.SetBool("Swhich", false);
                }
            }
        }




        if (Input.GetKey(KeyCode.LeftArrow))
        {


            if (Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("Swhich", true);

                if (Input.GetKey(KeyCode.LeftArrow))

                {
                    anim.SetBool("Swhich", false);

                }
            }
        }







    }
    void Key()//스페셜 키


    {
        if (Input.GetKey(KeyCode.Z))
        {
            anim.SetBool("Zkey", true);

        }
        if (!Input.GetKey(KeyCode.Z))
        {
            anim.SetBool("Zkey", false);

        }


        if (Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Space", true);

        }
        if (!Input.GetKey(KeyCode.Space))
        {
            anim.SetBool("Space", false);


        }


        if (Input.GetKey(KeyCode.LeftShift))//대쉬상채 좌우반전시
        {
            anim.SetBool("Shift", true);

        }
        if(!Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Shift", false);

        }



        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("Control", true);



        }
        if (!Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("Control", false);


        }
        if (anim.GetBool("Control") == true)
        {
            stop = false;
            stopTimes = false;
        }
        if (anim.GetBool("Control") == false)
        {
            stop = true;
            stopTimes = true;
        }
    }

    void TimeAcction()// 애니메이션 누름  구별
    {
        if (stopTimes == true)
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                anim.SetBool("Times", true);
                anim.SetBool("1Time", false);
            }
        if (!(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            anim.SetBool("Times", false);
            anim.SetBool("1Time", true);
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            anim.SetBool("Times", false);
            anim.SetBool("1Time", false);
        }

    }
    //void Swhich()// 데쉬시 1초안에 좌우반전
    //{
      
    //    if (Input.GetKeyUp(KeyCode.LeftArrow)&&Input.GetKey(KeyCode.RightArrow))
    //    {
           
    //            anim.SetBool("filp>R", true);




    //    }
    //    else if(Input.GetKeyUp(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
    //    {
    //        anim.SetBool("filp>L", true);
    //    }

        
    //    Invoke("filpStop", 0.5f);
    //}

    //void filpStop()
    //{

    //    anim.SetBool("filp>L", false);
    //    anim.SetBool("filp>R", false);
    //}



}

