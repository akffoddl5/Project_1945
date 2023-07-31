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
		// 시작되면 0번째 characterBtn의 이미지, 이름, 설명이 중앙에 뜬다.
		img_selected = characterBtn[now_characterNum].GetComponent<UI_characterBtn>().characterImg;
        characterName.text = characterBtn[now_characterNum].GetComponent<UI_characterBtn>().characterName;
        character_info.text = characterBtn[now_characterNum].GetComponent<UI_characterBtn>().characterInfo;
		
	}


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            now_characterNum++;
            // 더했는데 5보다 크면 5
			if (now_characterNum > 5)  now_characterNum = 5;
			characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);
			//Debug.Log(now_characterNum);
		}
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            now_characterNum--;
            // 뺐는데 0보다 작으면 0
            if (now_characterNum < 0) now_characterNum = 0;
			characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);
			//Debug.Log(now_characterNum);
		}

    }

    // 코루틴
    IEnumerator ShowCharacterInfo()
    {
		// 선택된 캐릭터의 버튼이 위로 통 튀도록
		characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);

		// 이미지가 중앙으로 먼저 이동
		Img_Anim();
		// 캐릭터의 이름과 설명이 투명도 0->1 나타남
        
		yield return null; // 에러 안 나게 하려고 그냥 넣은 거 
	}
    IEnumerator StartAnim()
    {
        // 캐릭터 이름과 설명이 투명도 1->0 으로 줄어들고
        // 이미지가 중앙에서 오른쪽으로 사라짐

        // 캐릭터 선택 씬 페이드 아웃 (+ 게임 씬 페이드인: 이거는 걍 게임 씬들이 페이드인 되면서 시작되도록 해야할듯?)

        yield return null; // 에러 안 나게 하려고 그냥 넣은 거 
    }

    void Img_Anim()
    {
		// img move from left to middle
        
		// when it move from left to middle, its alpha value change 0 to 1

	}
    

}
