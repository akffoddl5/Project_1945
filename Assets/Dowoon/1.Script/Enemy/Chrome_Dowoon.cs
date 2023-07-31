using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chrome_Dowoon : MonoBehaviour
{
    public GameObject[] parts;
    public float circleR; //반지름
    float deg; //각도
    public float objSpeed; //원운동 속도

    public float speedOffset;
    public float radiusOffset;

    public bool isRotate = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            deg += Time.deltaTime * objSpeed;
            if (deg < 360)
            {

                for (int i = 0; i < parts.Length; i++)
                {
                    var rad = Mathf.Deg2Rad * (deg + (i * (360 / parts.Length)));
                    var x = circleR * Mathf.Sin(rad);
                    var y = circleR * Mathf.Cos(rad);
                    var offset = 0f;
                    parts[i].transform.position = transform.position + new Vector3(x, y);
                    parts[i].transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / parts.Length))) * -1);

                }
            }
            else
            {
                deg = 0;
            }
        }
    }

   public IEnumerator RotateStart()
    {
        yield return new WaitForSeconds(2.0f);

        isRotate = true;

        yield return new WaitForSeconds(0.5f);

        speedOffset = 10.4f;
        radiusOffset = 0.5f;
        bool _isIncrease = true;
        while(true)
        {
            if (_isIncrease)
            {
                var scale = transform.localScale;
                if (scale.x <= 1.5f)
                {
                    scale.x += 0.01f;
                    scale.y += 0.01f;
                    scale.z += 0.01f;

                    transform.localScale = scale;
                }
             
               

                
                    circleR += radiusOffset * Time.deltaTime;

                    if (objSpeed <= 250)
                        objSpeed += speedOffset * Time.deltaTime;

                    if(circleR >= 10)
                        _isIncrease = false;

                    yield return new WaitForEndOfFrame();

                

            }
            else
            {
                    circleR -= radiusOffset * Time.deltaTime;

                    if (objSpeed >=  10)
                        objSpeed -= speedOffset * Time.deltaTime;

                    if (circleR <= 1)
                        _isIncrease = true;
                yield return new WaitForEndOfFrame();
            }
        }

   
    }
}
