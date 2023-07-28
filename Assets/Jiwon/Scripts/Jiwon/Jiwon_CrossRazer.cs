using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiwon_CrossRazer : MonoBehaviour
{
    float speedUpTime = 0f; // 스피드가 3초에 한 번 올라감
    float minSpeed = 20;
    float speed; // 현재 스피드

    float moveChangeTime = 0f;
    bool isClockWise = true;

    // G color
    public SpriteRenderer razer1, razer2;
	IEnumerator Co_GUpDown;
    bool isGUp = false;
    float GMin = 0f;
    float GMax = 1f;
    
    void Start()
    {
        speed = minSpeed;
        Co_GUpDown = GColor();
        StartCoroutine(Co_GUpDown);
	}
	private void FixedUpdate()
	{
        RazerRotate();
	}

	void Update()
    {
		speedUpTime += Time.deltaTime;
		moveChangeTime += Time.deltaTime;

		// 3초마다 점점 빨라지도록 설정
		if (speedUpTime > 3f)
        {
			speedUpTime = 0f; // speedUpTime 초기화
            speed += 10;
		}
        // 7초마다 랜덤 스피드 방향으로 변경되도록
        if (moveChangeTime > 7f)
        {
			moveChangeTime = 0f;
			RandomMove();
        }

    }
    void RazerRotate()
    {
		// 계속 speed의 속도로 빙글빙글 돌아가게
		if (isClockWise) transform.Rotate(0, 0, -speed * Time.deltaTime);
		else transform.Rotate(0, 0, speed * Time.deltaTime);
	}
    
    void RandomMove()
    {

        // 랜덤 방향
        int randomDir = Random.Range(0, 2);
        switch (randomDir)
        {
			// 0 시계방향
			case 0:
                if (!isClockWise)
                {
                    isClockWise = true;
					minSpeed = 20;
					speed = Random.Range(minSpeed, speed);
				}
                break;

			// 1 반시계방향
			case 1:
                if (isClockWise)
                {
				    isClockWise = false;
					minSpeed = 20;
					speed = Random.Range(minSpeed, speed);
				}
				break;
        }

    }


	// 코루틴으로 (Color.g 하한선 ~ Color.g 상한선) 왔다갔다 하게 만들기
	IEnumerator GColor()
    {
		float changeRate = 0.02f;
        while (true)
        {

			// 현재 razer1의 컬러를 c에 저장함
			Color c = razer1.color;

            // 상한 하한 반복하게 해야 함
            // c의 값이 Glow~Gheight까지 반복하게 만들어야 한다.
            // 그러면 if (c.g <= GMax) c.g +=30;
            // else if (c.g >= GMin) c.g -= 30;
                // 이렇게 하면 겹치는 구간에서 뭘 해야할지 모르게되니까 bool isUp 이나 isDown을 만들어서
                // 쭉 올라가거나 쭉 내려가도록 만드세요
            //GUp
            if (isGUp && c.g <= GMax)
            {
			    c.g += 0.05f;
                if (c.g >= GMax) isGUp = false;
		    }
            // GDown
            if (!isGUp && c.g >= GMin)
            {
                c.g -= 0.05f;
                if (c.g <= GMin) isGUp = true;
            }
            razer1.color = c;
            razer2.color = c;

            //Debug.Log(c.g+ ", gameObject" + gameObject.GetComponent<SpriteRenderer>().color.g);
            yield return new WaitForSeconds(changeRate);
        }
    }
}
