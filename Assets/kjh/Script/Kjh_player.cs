using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Kjh_player : MonoBehaviour
{
    public AudioSource moves;
    public AudioSource dashs;
    float moveX;
    float moveY; 
    float moveX1;
    float moveY1;
    public float movespeed = 2.4f;
    Vector3 move;
    public float boost = 100f;

    Animator anim;

    bool stopTimes = true;

    public static bool stop;

    float dashTime = 0;
    bool dash = false;
    bool dashswich = false;


    float BoostTime = 0;



    float CoolTime = 0;
    bool coolTime = true;



    void Start()
    {

        anim = GetComponent<Animator>();

    }


    void Update()
    {
        Move(true);

        TimeAcction();
        Key();
        Movetree();




    }

    public void Move(bool b)
    {
        stop = b;
        if (stop == true)
        {


            moveX1= Input.GetAxis("Horizontal") * Time.deltaTime * movespeed;//x,y버튼누르면 각각 숫자+ -1 나옴
            moveY1 = Input.GetAxis("Vertical") * Time.deltaTime * movespeed;

            Boost();

            move = new Vector3(moveX1, moveY1, 0);


            transform.Translate(move);


        }
    }

    void movesound()
    {
        Instantiate(moves);
    }


    public void Boost()//부스트(주는 함수
    {

        moveX = Input.GetAxis("Horizontal") * Time.deltaTime;
        moveY = Input.GetAxis("Vertical") * Time.deltaTime;
        if (coolTime == true)
        {

            if (Input.GetKeyDown(KeyCode.LeftShift) && (moveX >= -0.5))
            {
                moveX1 *= boost;
                Instantiate(dashs);

            }

            else if (Input.GetKeyDown(KeyCode.LeftShift) && (moveX <= 0.5))
            {
                moveX1 *= boost;

                Instantiate(dashs);
            }


            if (Input.GetKeyDown(KeyCode.LeftShift) && (moveY >= -0.5))
            {
                moveY1 *= boost;
                Instantiate(dashs);
            }


            else if (Input.GetKeyDown(KeyCode.LeftShift) && (moveY <= 0.5))
            {

                moveY1 *= boost;
                Instantiate(dashs);
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


        if ((Kjh_Monster.CountZ >= 6) && Input.GetKey(KeyCode.Z))
        {
            anim.SetBool("Zkey", true);
            Kjh_Monster.CountZ = 0;

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
        if (!Input.GetKey(KeyCode.LeftShift))
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




    }


    void TimeAcction()// 애니메이션 누름  구별
    {
        //  if (stopTimes == true)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ENEMY"))
        {
            Destroy(gameObject);
            Debug.Log(collision.gameObject);
        }
    }

}

