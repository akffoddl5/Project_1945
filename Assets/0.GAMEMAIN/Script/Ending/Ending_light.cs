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

        // �̰� ���� ���̰� �� ���ϸ� �̷��� �϶�� ����� �̾߱��ߴµ� ��� �ϴ���
        // �ƹ�ư ��ǥ ������ �����ߴµ� ���� �� ������ ���� ��Ʈ����

        float dist = Vector2.Distance(transform.position, middlePosition);

		if ((dist <= 0.1f) && !isUsed)
        {
            StartCoroutine(Flash());
            
        }
        // ���ƴµ�, ȭ���� �ٽ� ���ƿ����� 
        if(isUsed && pan.GetComponent<Image>().color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Flash()
    {
        // �г� �Ͼ�� ���� tmp ����
        Color tmp = new Color(1f, 1f, 1f, 0.9f);
        pan.GetComponent<Image>().color = tmp;

		// ȭ���� �ٽ� ������ ������� ���ƿ���
        // alpha ���� 0�� �Ǿ�� ȭ���� ������� ���ƿ�
        while(pan.GetComponent<Image>().color.a > 0f)
        {
            tmp.a -= 0.03f;
          
			pan.GetComponent<Image>().color = tmp;
            yield return new WaitForEndOfFrame();
		}

        isUsed = true;
	}
}
