using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class June_PlayerMovement : MonoBehaviour
{
    Animator ani; //애니메이터 가져올 변수
    public float moveSpeed = 5;
    


    [SerializeField]


    bool IsPause;

    void Start()
    {
        ani = GetComponent<Animator>();
        IsPause = false;
    }


    private void FixedUpdate()
    {
        Movement();
        if (Input.GetKeyDown(KeyCode.T))
        {
            /*일시정지 활성화*/
            if (IsPause == false)
            {
                Time.timeScale = 0;
                IsPause = true;
                return;
            }

            /*일시정지 비활성화*/
            if (IsPause == true)
            {
                Time.timeScale = 1;
                IsPause = false;
                return;
            }
        }


    }


    void Movement()
    {
        float moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveX = (moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal")) / 2;
            moveY = (moveSpeed * Time.deltaTime * Input.GetAxis("Vertical")) / 2;
            transform.GetChild(1).gameObject.SetActive(true); //자식객체 0번 불러와서 켜주기


        }
        else
        {
            moveX = moveSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
            moveY = moveSpeed * Time.deltaTime * Input.GetAxis("Vertical");
            transform.GetChild(1).gameObject.SetActive(false);

        }

        if (Input.GetAxis("Horizontal") >= 0.1f)
        {
            ani.SetBool("MoveRight", true);
        }
        else
        {
            ani.SetBool("MoveRight", false);
        }

        if (Input.GetAxis("Horizontal") <= -0.1f)
        {
            ani.SetBool("MoveLeft", true);
        }
        else
        {
            ani.SetBool("MoveLeft", false);
        }


      
        transform.Translate(moveX, moveY, 0);
    }



}