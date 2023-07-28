using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jiwon_CrossRazer : MonoBehaviour
{
    float speedUpTime = 0f; // ���ǵ尡 3�ʿ� �� �� �ö�
    float minSpeed = 20;
    float speed; // ���� ���ǵ�

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

		// 3�ʸ��� ���� ���������� ����
		if (speedUpTime > 3f)
        {
			speedUpTime = 0f; // speedUpTime �ʱ�ȭ
            speed += 10;
		}
        // 7�ʸ��� ���� ���ǵ� �������� ����ǵ���
        if (moveChangeTime > 7f)
        {
			moveChangeTime = 0f;
			RandomMove();
        }

    }
    void RazerRotate()
    {
		// ��� speed�� �ӵ��� ���ۺ��� ���ư���
		if (isClockWise) transform.Rotate(0, 0, -speed * Time.deltaTime);
		else transform.Rotate(0, 0, speed * Time.deltaTime);
	}
    
    void RandomMove()
    {

        // ���� ����
        int randomDir = Random.Range(0, 2);
        switch (randomDir)
        {
			// 0 �ð����
			case 0:
                if (!isClockWise)
                {
                    isClockWise = true;
					minSpeed = 20;
					speed = Random.Range(minSpeed, speed);
				}
                break;

			// 1 �ݽð����
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


	// �ڷ�ƾ���� (Color.g ���Ѽ� ~ Color.g ���Ѽ�) �Դٰ��� �ϰ� �����
	IEnumerator GColor()
    {
		float changeRate = 0.02f;
        while (true)
        {

			// ���� razer1�� �÷��� c�� ������
			Color c = razer1.color;

            // ���� ���� �ݺ��ϰ� �ؾ� ��
            // c�� ���� Glow~Gheight���� �ݺ��ϰ� ������ �Ѵ�.
            // �׷��� if (c.g <= GMax) c.g +=30;
            // else if (c.g >= GMin) c.g -= 30;
                // �̷��� �ϸ� ��ġ�� �������� �� �ؾ����� �𸣰ԵǴϱ� bool isUp �̳� isDown�� ����
                // �� �ö󰡰ų� �� ���������� ���弼��
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
