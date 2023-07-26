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

        anim= GetComponent<Animator>();

    }


    void Update()
    {

        runAll();


        Move();
        
       


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
       

                if (Input.GetKeyDown(KeyCode.Z) && (moveX >= -0.5))
                {
                    moveX *= boost;
                        
                }

                else if (Input.GetKeyDown(KeyCode.Z) && (moveX <= 0.5))
                {

                    moveX *= boost;
                   
                }


                if (Input.GetKeyDown(KeyCode.Z) && (moveY >= -0.5))
                {
                    moveY *= boost;

                }


                else if (Input.GetKeyDown(KeyCode.Z) && (moveY <= 0.5))
                {

                    moveY *= boost;
                   
                }
           
    }
   


    void runAll()
    {
        run1();
        run2();
        runGB();

    }

    void run1()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) && Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("runB", true);

            if (Input.GetKeyUp(KeyCode.UpArrow) && Input.GetKeyUp(KeyCode.DownArrow))
            {
                anim.SetBool("runB", false);


            }
        }
        

    }

    void run2()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetFloat("runF",-0.5f);
            anim.SetBool("runB", true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetFloat("runF", 0.5f);
            anim.SetBool("runB", true);
        }
    }

    void runGB()
    {
        if ((Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow)) && Input.GetKeyDown(KeyCode.Z))// 궁극기
        {
            anim.SetBool("runGB", true);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("runGB", false);
        }
                
    }

}
