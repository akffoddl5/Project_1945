using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_selected_img : MonoBehaviour
{
	[Header("�߾��� character info")]
	public Image img_selected;
	public Text characterName;
	public Text character_info;

	// �Ʒ� ��ư 6�� GameObject �迭�� ���� �ε����� �����ϱ� ���� int�� ����
	[Header("�Ʒ��� 6�� ��ư �迭")]
	public GameObject[] Obj_characterBtn = new GameObject[6];
	int now_characterNum = 0;

	// �̹��� �̵� ���� ���� �̹����� ���ʿ��� �߾����� �̵��ǰ� ��
	float leftX = -800f;
	float middleX = -220f;
	float rightX = 800f;
	Vector3 leftImgVec;
	Vector3 middleImgVec;
	Vector3 rightImgVec;

	// ���� �Է��ؼ� ĳ���� ���� ����
	bool isSelected = false;

	// �ؽ�Ʈ ������ ���� ����
	Coroutine Co_textVisible;
	Coroutine Co_textInvisible;
	Color invisibleBlack = new Color(0f, 0f, 0f, 0f);
	bool isStartCoTextInfoVisible = false;
	bool isStartCoTextInfoInbisible = false;

	// ��ư�� ���õǾ��� �� �÷�, ��ư�� �⺻ �÷�
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
		// �̹��� �������� ������ ���� ����(1, 1, 1, 0)�ϰ� �ٲ�
		img_selected.gameObject.transform.localPosition = leftImgVec;
		characterName.color = invisibleBlack;
		character_info.color = invisibleBlack;

		// 0��° characterBtn�� �̹���, �̸�, ����
		// ��ư�� �θ� UI_characterBtn ��ũ��Ʈ�� ��� ����
		img_selected.sprite = Obj_characterBtn[0].transform.parent.GetComponent<UI_characterBtn>().characterSpr[now_characterNum];
		characterName.text = Obj_characterBtn[0].transform.parent.GetComponent<UI_characterBtn>().characterName[now_characterNum];
		character_info.text = Obj_characterBtn[0].transform.parent.GetComponent<UI_characterBtn>().characterInfo[now_characterNum];

		// ��ư�� ���� selectedColor�� ����
		Obj_characterBtn[now_characterNum].GetComponent<Image>().color = selectedBtnColor;

		// false�� �� startCoroutine �� �� �����ϱ� false�� �ʱ�ȭ���ֱ�
		isStartCoTextInfoVisible = false;
	}

	void Update()
	{
		if (!isSelected)
		{
			SelectWithKey();
			MakeInfo();
		}
		// ���� ������ isSelected = true
		else
		{
			if (!isStartCoTextInfoInbisible)
			{
				Co_textInvisible = StartCoroutine(TextInfoinisible());
				isStartCoTextInfoInbisible = true;
			}

			ReMoveInfo();

		}

		// ���͸� ������ �� => �̰͵� �Լ��� ���� Update�� ����ϰ� �����
		// Ȥ�� �𸣴ϱ� ���þ� �� ���¿����� ���ͷ� ĳ���͸� ������ �� �ֵ��� ����
		if (Input.GetKeyDown(KeyCode.Return)&& !isSelected) 
		{
			// ���õ� üũ
			isSelected = true;

			if(now_characterNum ==1 )
			{
				GameObject.Find("Button").transform.GetChild(1).GetComponent<AudioSource>().Play();
			}
			// 0���� �����̴ϱ� ���� ������ �� 0�� �ε������ �������� �ٲ��ֱ�
			if (now_characterNum == 0) now_characterNum = Random.Range(1, 6);

			// ������ ĳ���͸� 1�� ���������� �ִ� �� ��� ����??
			// ¦���� ���� ������ �Ŵ����� �ϸ� ��
			ITEM_MANAGER.instance.ItemSetting((Charactor)now_characterNum);
			
			//Debug.Log((Charactor)now_characterNum);

			// ���̵� �ƿ� �Լ� ����
			UI_Manager.instance.CharacterSelect_FadeOut((Charactor)now_characterNum);
			// 1�� �������� �ҷ�����
			//SceneManager.LoadScene("");
		}

	}

	void SelectWithKey()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			// ���� �ε��� ��ư�� �÷��� defaultColor
			Obj_characterBtn[now_characterNum].GetComponent<Image>().color = defaultBtnColor;

			// ��ư�� �ε���++;
			now_characterNum++;
			if (now_characterNum > 5) now_characterNum = 0;

			// ��ư �� Ƣ�� �� AddForce
			Obj_characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);

			// ���� �ʱ�ȭ
			InitInfo();
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			// ���� �ε��� ��ư�� �÷��� defaultColor
			Obj_characterBtn[now_characterNum].GetComponent<Image>().color = defaultBtnColor;

			// ��ư �ε���--;
			now_characterNum--;
			if (now_characterNum < 0) now_characterNum = 5;

			// ��ư �� Ƣ�� �� AddForce
			Obj_characterBtn[now_characterNum].GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400, ForceMode2D.Impulse);

			// ���� �ʱ�ȭ
			InitInfo();
		}
	}

	void MakeInfo()
	{
		//�̹��� ���ƿ���
		img_selected.gameObject.transform.localPosition = Vector3.Lerp(img_selected.gameObject.transform.localPosition, middleImgVec, 0.1f);

		// ������Ʈ�� MakeInfo()�� ���µ� startCo ������ ���� �ʵ���
		if (!isStartCoTextInfoVisible)
		{
			Co_textVisible = StartCoroutine(TextInfoVisible());
			isStartCoTextInfoVisible = true;
		}
		// �ؽ�Ʈ�� ���̰� �Ǹ� �ڷ�ƾ stop���ֱ�
		if (characterName.color.a >= 1){
			StopCoroutine(Co_textVisible);
		}
	}
	IEnumerator TextInfoVisible()
	{
		// ���� alpha 0->1
		Color tmp = new Color(0, 0, 0, 0.1f);

		while (characterName.color.a < 1)
		{
			// ���� ����(alpha value) 0->1 �� ����
			characterName.color += tmp;
			character_info.color += tmp;
			yield return new WaitForSeconds(0.02f);
		}
	}

	void ReMoveInfo()
	{
		// ���ڰ� ������ ��
		if(characterName.color.a <= 0)
		{
			StopCoroutine(Co_textInvisible);
			// �̹��� ���ư���
			img_selected.gameObject.transform.localPosition = Vector3.Lerp(img_selected.gameObject.transform.localPosition, rightImgVec, 0.1f);
		}
	}
	IEnumerator TextInfoinisible()
	{
		Color tmp = new Color(0, 0, 0, 0.1f);

		while (characterName.color.a > 0)
		{
			// ���� ����(alpha value) 1->0 �� ����
			characterName.color -= tmp;
			character_info.color -= tmp;
			yield return new WaitForSeconds(0.02f);
		}
		yield return new WaitForSeconds(1f);
	}

}