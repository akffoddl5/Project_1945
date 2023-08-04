using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Ending_light : MonoBehaviour
{
    public GameObject pan;

    Vector3 middlePosition;
    bool isUsed = false;
    void Start()
    {
        middlePosition = new Vector3(transform.position.x, 0, 0);
	}
    void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, middlePosition, 0.2f);

        // 이거 둘의 차이가 몇 이하면 이렇게 하라고 도운씨가 이야기했는데 어떻게 하더라
        // 아무튼 목표 지점에 도착했는데 사용된 적 없으면 섬광 터트리기

        float dist = Vector2.Distance(transform.position, middlePosition);

		if ((dist <= 0.1f) && !isUsed)
        {
            StartCoroutine(Flash());
            
        }
        // 사용됐는데, 화면이 다시 돌아왔으면 
        if(isUsed && pan.GetComponent<Image>().color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Flash()
    {
        // 패널 하얗게 만들 tmp 변수
        Color tmp = new Color(1f, 1f, 1f, 0.9f);
        pan.GetComponent<Image>().color = tmp;

		// 화면이 다시 서서히 원래대로 돌아오게
        // alpha 값이 0이 되어야 화면이 원래대로 돌아옴
        while(pan.GetComponent<Image>().color.a > 0f)
        {
            tmp.a -= 0.03f;
          
			pan.GetComponent<Image>().color = tmp;
            yield return new WaitForEndOfFrame();
		}

        isUsed = true;
	}
}
