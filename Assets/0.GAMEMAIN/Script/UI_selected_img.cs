using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_selected_img : MonoBehaviour
{
	[Header("중앙의 character info")]
	public Image img_selected;
	public Text characterName;
	public Text character_info;

	// 아래 버튼 6개 GameObject 배열과 현재 인덱스를 저장하기 위한 int형 변수
	[Header("아래의 6개 버튼 배열")]
	public GameObject[] Obj_characterBtn = new GameObject[6];
	int now_characterNum = 0;

	// 이미지 이동 관련 변수 이미지가 왼쪽에서 중앙으로 이동되게 함
	float leftX = -800f;
	float middleX = -220f;
	float rightX = 800f;
	Vector3 leftImgVec;
	Vector3 middleImgVec;
	Vector3 rightImgVec;

	// 엔터 입력해서 캐릭터 선택 여부
	bool isSelected = false;

	// 텍스트 투명도를 위한 변수
	Coroutine Co_textVisible;
	Coroutine Co_textInvisible;
	Color invisibleBlack = new Color(0f, 0f, 0f, 0f);
	bool isStartCoTextInfoVisible = false;
	bool isStartCoTextInfoInbisible = false;

	// 버튼이 선택되었을 때 컬러, 버튼의 기본 컬러
	Color selectedBtnColor = new Color(1f, 0.95f, 0.75f, 1f);
	Color defaultBtnColor = new Color(1f, 1f, 1f, 1f);


	void Awake()
	{
		InitInfo();
	}

	void InitInfo()
	{
		leftImgVec = new Vector3(leftX, img_selected.gameObject.transform.localPosition.y, 0f);
		middleImgVec = new Vector3(middleX, img_selected.gameObject.transform.localPosition.y, 0f);
		rightImgVec = new Vector3(rightX, img_selected.gameObject.transform.localPosition.y, 0f);
		// 이미지 왼쪽으로 보내고 글자 투명(1, 1, 1, 0)하게 바꿈
		img_selected.gameObject.transform.localPosition = leftImgVec;
		characterName.color = invisibleBlack;
		character_info.color = invisibleBlack;

		// 0번째 characterBtn의 이미지, 이름, 설명
		// 버튼의 부모가 UI_characterBtn 스크립트를 들고 있음
		img_selected.sprite = Obj_characterBtn[0].transform.parent.GetComponent<UI_characterBtn>().characterSpr[now_characterNum];
		characterName.text = Obj_characterBtn[0].transform.parent.GetComponent<UI_characterBtn>().characterName[now_characterNum];
		character_info.text = Obj_characterBtn[0].transform.parent.GetComponent<UI_characterBtn>().characterInfo[now_characterNum];

		// 버튼의 색을 selectedColor로 변경
		Obj_characterBtn[now_characterNum].GetComponent<Image>().color = selectedBtnColor;

		// false일 때 startCoroutine 할 수 있으니까 false로 초기화해주기
		isStartCoTextInfoVisible = false;
	}

	void Update()
	{
		if (!isSelected)
		{
			SelectWithKey();
			MakeInfo();
		}
		// 엔터 누르면 isSelected = true
		else
		{
			if (!isStartCoTextInfoInbisible)
			{
				Co_textInvisible = StartCoroutine(TextInfoinisible());
				isStartCoTextInfoInbisible = true;
			}

			ReMoveInfo();

		}

		// 엔터를 눌렀을 때 => 이것도 함수로 만들어서 Update를 깔끔하게 만들기
		// 혹시 모르니까 선택안 됨 상태에서만 엔터로 캐릭터를 선택할 수 있도록 만듦
		if (Input.GetKeyDown(KeyCode.Return)&& !isSelected) 
		{
			// 선택됨 체크
			isSelected = true;

			if(now_characterNum ==1 )
			{
				GameObject.Find("Button").transform.GetChild(1).GetComponent<AudioSource>().Play();
			}
			// 0번은 랜덤이니까 엔터 눌렀을 때 0번 인덱스라면 랜덤으로 바꿔주기
			if (now_characterNum == 0) now_characterNum = Random.Range(1, 6);

			// 선택한 캐릭터를 1번 스테이지에 넣는 건 어떻게 하지??
			// 짝꿍이 만든 아이템 매니저로 하면 됨
			ITEM_MANAGER.instance.ItemSetting((Charactor)now_characterNum);
			
			//Debug.Log((Charactor)now_characterNum);

			// 페이드 아웃 함수 실행
			UI_Manager.instance.CharacterSelect_FadeOut((Charactor)now_characterNum);
			// 1번 스테이지 불러오기
			//SceneManager.LoadScene("");
		}

	}

	void SelectWithKey()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			// 이전 인덱스 버튼의 컬러값 defaultColor
			Obj_characterBtn[now_characterNum].GetComponent<Image>().color = defaultBtnColor;

			// 버튼의 인덱스++;
			now_characterNum++;
			if (now_characterNum > 5) now_characterNum = 0;

			// 버튼 통 튀는 거 AddForce
			Obj_characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);

			// 정보 초기화
			InitInfo();
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			// 이전 인덱스 버튼의 컬러값 defaultColor
			Obj_characterBtn[now_characterNum].GetComponent<Image>().color = defaultBtnColor;

			// 버튼 인덱스--;
			now_characterNum--;
			if (now_characterNum < 0) now_characterNum = 5;

			// 버튼 통 튀는 거 AddForce
			Obj_characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);

			// 정보 초기화
			InitInfo();
		}
	}

	void MakeInfo()
	{
		//이미지 날아오기
		img_selected.gameObject.transform.localPosition = Vector3.Lerp(img_selected.gameObject.transform.localPosition, middleImgVec, 0.1f);

		// 업데이트에 MakeInfo()가 들어가는데 startCo 여러번 되지 않도록
		if (!isStartCoTextInfoVisible)
		{
			Co_textVisible = StartCoroutine(TextInfoVisible());
			isStartCoTextInfoVisible = true;
		}
		// 텍스트가 보이게 되면 코루틴 stop해주기
		if (characterName.color.a >= 1){
			StopCoroutine(Co_textVisible);
		}
	}
	IEnumerator TextInfoVisible()
	{
		// 글자 alpha 0->1
		Color tmp = new Color(0, 0, 0, 0.1f);

		while (characterName.color.a < 1)
		{
			// 글자 투명도(alpha value) 0->1 로 변경
			characterName.color += tmp;
			character_info.color += tmp;
			yield return new WaitForSeconds(0.02f);
		}
	}

	void ReMoveInfo()
	{
		// 글자가 지워진 후
		if(characterName.color.a <= 0)
		{
			StopCoroutine(Co_textInvisible);
			// 이미지 날아가기
			img_selected.gameObject.transform.localPosition = Vector3.Lerp(img_selected.gameObject.transform.localPosition, rightImgVec, 0.1f);
		}
	}
	IEnumerator TextInfoinisible()
	{
		Color tmp = new Color(0, 0, 0, 0.1f);

		while (characterName.color.a > 0)
		{
			// 글자 투명도(alpha value) 1->0 로 변경
			characterName.color -= tmp;
			character_info.color -= tmp;
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1f);
	}

}