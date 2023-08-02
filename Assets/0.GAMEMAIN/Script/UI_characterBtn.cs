using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_characterBtn : MonoBehaviour
{
    public Sprite[] characterSpr = new Sprite[6];
    public string[] characterName = new string [6];
    public string[] characterInfo = new string[6];

	private void Awake()
	{
		Init();
	}
	void Init()
	{

		// 캐릭터 이름 배열 초기화
		characterName[0] = "랜덤";
		characterName[1] = "아야";
		characterName[2] = "외계인";
		characterName[3] = "제트";
		characterName[4] = "삼각";
		characterName[5] = "레드";

		// 캐릭터 설명 배열 초기화
		characterInfo[0] = "랜덤한 캐릭터 선택";
		characterInfo[1] = "이동 → ← ↑ ↓\n\n공격 z(키다운 가능)\n\n충돌지점표시 및 천천히 이동 Shift\n\n";
		characterInfo[2] = "이동 → ← ↑ ↓\n\n공격 z\n\n공격방향전환\n(시계) x\n\n공격방향전환\n(반시계) c";
		characterInfo[3] = "제트 캐릭터 설명";
		characterInfo[4] = "삼각 캐릭터 설명";
		characterInfo[5] = "이동 → ← ↑ ↓\n\n자동공격\n\n대쉬 Space";

	}

}
