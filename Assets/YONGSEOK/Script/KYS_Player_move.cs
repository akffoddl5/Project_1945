using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class KYS_Player_move : MonoBehaviour
{

    //이동 관련 변수, velocity 로 움직이자
    [SerializeField]
    private float speed = 2000f;
    private Rigidbody2D rb;


    //대시관련 변수
    private float dash_speed = 3.5f;
    private float dash_time = 0.085f;         //약 0.1초
    private float dash_cool_time = 3f;  // 약 3초
    private Vector2 pre_velocity;
    public GameObject dash_trail_h;              //잔상 오브젝트
    public GameObject dash_trail_u;              //잔상 오브젝트
    public GameObject dash_trail_d;              //잔상 오브젝트
    


    //애니메이터 관련 변수
    private Animator animator;
    private string anim_idle = "Player_IDLE";
    private string anim_left = "Player_left";
    private string anim_right = "Player_right";
    private string anim_down = "Player_down";
    private string anim_spin = "Player_spin";

    //레이저관련 변수
    private bool isRazor = false;
    public GameObject obj_Razor;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float axis_X = Input.GetAxisRaw("Horizontal");
        float axis_Y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyUp(KeyCode.P))
        {
            Razor();
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            isRazor = false;
        }

        if (Input.GetKey(KeyCode.Z) || isRazor)
        {
            isRazor = true;
            animator.Play(anim_spin);
            Razor();
        }else if(axis_Y < 0) animator.Play(anim_down);
        else if (axis_X > 0) animator.Play(anim_right);
        else if (axis_X < 0) animator.Play(anim_left);
        else animator.Play(anim_idle);


        rb.velocity = new Vector2(axis_X, axis_Y ) * speed* Time.deltaTime ;
        Dash(rb.velocity);
    }


    //z스킬 레이저
    public void Razor()
    {
        Instantiate(obj_Razor, transform.position, Quaternion.identity);


        

    }
    

    public void Dash(Vector2 preVelocity)
    {
        //Debug.Log($"dash cool : {dash_cool_time}  dash_time {dash_time } ");
        dash_cool_time -= Time.deltaTime;
        dash_time -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) )
        {
            Debug.Log("dash.." + rb.velocity);
            
            dash_cool_time = Time.deltaTime * 180;
            dash_time = Time.deltaTime * 6;


        }

        if (dash_time > 0)
        {
            rb.velocity = rb.velocity * dash_speed;
            GameObject tmp_dash_trail = null;
            //현재 프레임에 잔상 남기기
            if (rb.velocity.x > 0)
            {
                tmp_dash_trail = Instantiate(dash_trail_h, transform.position, Quaternion.identity);
            }
            else if (rb.velocity.x < 0)
            {
                tmp_dash_trail = Instantiate(dash_trail_h, transform.position, Quaternion.identity);
                tmp_dash_trail.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (rb.velocity.y > 0)
            {
                tmp_dash_trail = Instantiate(dash_trail_u, transform.position, Quaternion.identity);
            }else if(rb.velocity.y < 0){
                tmp_dash_trail = Instantiate(dash_trail_d, transform.position, Quaternion.identity);
            }
            Destroy(tmp_dash_trail, 0.3f);
        }
        else
        {
            rb.velocity = preVelocity;
        }
    }


    

}
