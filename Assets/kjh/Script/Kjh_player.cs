using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Kjh_player : MonoBehaviour
{

    float moveX;
    float moveY;
    public float movespeed = 1.3f;
    Vector3 move;
    public float boost = 50.0f;
    
    Animator anim;
   

    void Start()
    {

        anim = GetComponent<Animator>();

    }


    void Update()
    {
        Move();
        Key();
        Movetree();





    }

    public void Move()
    {

        moveX = Input.GetAxis("Horizontal") * Time.deltaTime * movespeed;//x,y버튼누르면 각각 숫자+ -1 나옴
        moveY = Input.GetAxis("Vertical") * Time.deltaTime * movespeed;

        Boost();

        move = new Vector3(moveX, moveY, 0);
        

        transform.Translate(move);
      


    }

    public void Boost()//부스트(주는 함수
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



    void Movetree()//총을 쏘는방향
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        float movefact = (Input.GetAxis("Horizontal") * Input.GetAxis("Vertical"));
        if ((Input.GetAxis("Vertical")) >= 0.5f)
        {

            anim.SetFloat("Vertical", moveX);
            
        }
        else if ((Input.GetAxis("Vertical")) <= -0.5f)
        {

            anim.SetFloat("Vertical", moveX);

        }

        if ((Input.GetAxis("Horizontal")) >= 0.5f)
        {

            anim.SetFloat("Horizontal", moveY);

        }
        else if ((Input.GetAxis("Horizontal")) <= -0.5f)
        {

            anim.SetFloat("Horizontal", moveY);

        }



        if (movefact != 0)
        {
            anim.SetBool("Arrow", true);
        }
        else
        {
            anim.SetBool("Arrow", false);
        }




    }
    void Key()//스페셜 키

             
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetBool("Zkey", true);

        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            anim.SetBool("Zkey", false);

        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Space", true);

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetBool("Space", false);


        }


        if (Input.GetKeyDown(KeyCode.LeftShift))//대쉬상채 좌우반전시
        {
           
            if (Input.GetAxis("Horizontal")>=0.5)
            {
                anim.SetBool("Swhich", true);
            }
           else if (Input.GetAxis("Horizontal") <= -0.5)
            {
                anim.SetBool("Swhich", true);
            }
            if (Input.GetAxis("Horizontal") == 0)
            {
                anim.SetBool("Swhich", false);
            }

            if (Input.GetAxis("Vertical") !=0)
            {
                anim.SetBool("Swhich", true);
            }
            if (Input.GetAxis("Vertical") == 0)
            {
                anim.SetBool("Swhich", false);
            }



        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("Swhich", false);


        }

    }
}

 

