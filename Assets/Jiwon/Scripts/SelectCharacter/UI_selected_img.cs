using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_selected_img : MonoBehaviour
{
    float leftX;
    float middleX;
    float rightX;

    public Image img_selected;
    public Text characterName;
    public Text character_info;

    public GameObject[] characterBtn = new GameObject[6];
    int now_characterNum = 0;
    

    void Start()
    {
		// ���۵Ǹ� 0��° characterBtn�� �̹���, �̸�, ������ �߾ӿ� ���.
		img_selected = characterBtn[now_characterNum].GetComponent<UI_characterBtn>().characterImg;
        characterName.text = characterBtn[now_characterNum].GetComponent<UI_characterBtn>().characterName;
        character_info.text = characterBtn[now_characterNum].GetComponent<UI_characterBtn>().characterInfo;
		
	}


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            now_characterNum++;
            // ���ߴµ� 5���� ũ�� 5
			if (now_characterNum > 5)  now_characterNum = 5;
			characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);
			//Debug.Log(now_characterNum);
		}
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            now_characterNum--;
            // ���µ� 0���� ������ 0
            if (now_characterNum < 0) now_characterNum = 0;
			characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);
			//Debug.Log(now_characterNum);
		}

    }

    // �ڷ�ƾ
    IEnumerator ShowCharacterInfo()
    {
		// ���õ� ĳ������ ��ư�� ���� �� Ƣ����
		characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);

		// �̹����� �߾����� ���� �̵�
		Img_Anim();
		// ĳ������ �̸��� ������ ���� 0->1 ��Ÿ��
        
		yield return null; // ���� �� ���� �Ϸ��� �׳� ���� �� 
	}
    IEnumerator StartAnim()
    {
        // ĳ���� �̸��� ������ ���� 1->0 ���� �پ���
        // �̹����� �߾ӿ��� ���������� �����

        // ĳ���� ���� �� ���̵� �ƿ� (+ ���� �� ���̵���: �̰Ŵ� �� ���� ������ ���̵��� �Ǹ鼭 ���۵ǵ��� �ؾ��ҵ�?)

        yield return null; // ���� �� ���� �Ϸ��� �׳� ���� �� 
    }

    void Img_Anim()
    {
		// img move from left to middle
        
		// when it move from left to middle, its alpha value change 0 to 1

	}
    

}
