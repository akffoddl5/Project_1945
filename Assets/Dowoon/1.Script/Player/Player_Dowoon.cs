using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Dowoon : MonoBehaviour
{
    float moveSpeed = 10.5f;
    public float PowerGauge = 0f;
    public const float MAX_POWER_GAUGE = 100f;
    public GameObject BombGameObject;
    public GameObject BombEffectSprite;



    Animator anim;
    [SerializeField]    
    GameObject bulletPos;
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject Gauge_slider;

    [SerializeField]
    GameObject Gauge_slider_fill;

    // Start is called before the first frame update
    void Start()
    {

        PowerGauge = MAX_POWER_GAUGE;
        ChangeGaugeValue();
        anim = GetComponent<Animator>();
        //bulletPos = GetComponentInChildren<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        transform.Translate( new Vector3(1,0,0) * h * moveSpeed * Time.deltaTime);
        transform.Translate( new Vector3(0,1,0)* v * moveSpeed * Time.deltaTime);

        anim.SetFloat("h", h);
        anim.SetFloat("v", v);


        if(Input.GetKeyDown(KeyCode.Space))
        {
            ShotBullet();
            PowerGauge += 3f;
            if (PowerGauge >= MAX_POWER_GAUGE)
                PowerGauge = MAX_POWER_GAUGE;
            ChangeGaugeValue();
        }       

        if(PowerGauge >= 50)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                PowerGauge -= 50;
               var cour=  StartCoroutine(UseBoom());
                ChangeGaugeValue();

            }
        }

        SetGaugePos();
    }

    public void SetGaugePos()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.y -= 60f;
     
        Gauge_slider.transform.position = pos;
        
    }
    public void ShotBullet()
    {
        var pos = bulletPos.transform.position;
        var b1 = Instantiate(bulletPrefab, bulletPos.transform.position, Quaternion.identity);
        var b2 = Instantiate(bulletPrefab, bulletPos.transform.position, Quaternion.identity);
        var b3 = Instantiate(bulletPrefab, bulletPos.transform.position, Quaternion.identity);
        //    var b1 = ObjectPool_Dowoon.GetBullet(BulletType.Player);
        b1.transform.position = pos;

        pos.x -= 0.6f;
        pos.y -= 0.4f;
      //  var b2 = ObjectPool_Dowoon.GetBullet(BulletType.Player);
        b2.transform.position = pos;
        pos.x += 1.2f;
       // var b3 = ObjectPool_Dowoon.GetBullet(BulletType.Player);
        b3.transform.position = pos;
       
    }

    public void ChangeGaugeValue()
    {
        var val = (float)PowerGauge / MAX_POWER_GAUGE;

        if( val < 0.5f)
        {
            Gauge_slider_fill.GetComponent<Image>().color = Color.red;
        }
        else if ( val >= 0.5f)
        {
            Gauge_slider_fill.GetComponent<Image>().color = Color.green;
        }


        Gauge_slider.GetComponent<Slider>().value = val;

    }
    IEnumerator UseBoom()
    {
        

        GameObject[] BombObj = new GameObject[3];
  
        

            float intervalangle = 180 / BombObj.Length; // 발사체 사이의 각도

        for (int i = 0; i < BombObj.Length; i++)
        {
            BombObj[i] = Instantiate(BombGameObject, transform.position, Quaternion.identity);

            // 발사체가 각도 구하기 x, y
            float angle = 60 + i * 30;
         
                // 각도를 이용해서 x, y 좌표를 구하기
                float x = Mathf.Cos(angle * Mathf.PI / 180.0f); // 라디안 각도 까먹었음. 다음에는 까먹지 말기
                float y = Mathf.Sin(angle * Mathf.PI / 180.0f);

                Vector2 dir = new Vector2(x, y);
                BombObj[i].GetComponent<BombObject>().Direction = dir;
            BombObj[i].GetComponent<BombObject>().StartCoroutine(BombObj[i].GetComponent<BombObject>().InstantiateBoom());
                
            }

        yield return new WaitForSeconds(0.2f);



            
        
      
    }
}
