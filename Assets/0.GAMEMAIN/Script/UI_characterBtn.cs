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
		characterInfo[1] = "이동 방향키\n\n공격 Z(키다운 충전)\n\n자동공격 X\n\n유도공격 C\n\n충돌지점표시 및 천천히 이동 Shift\n\n";
		characterInfo[2] = "이동 → ← ↑ ↓\n\n공격 z\n\n공격방향전환\n(시계) x\n\n공격방향전환\n(반시계) c";
		characterInfo[3] = "이동 → ← ↑ ↓\n\nZ:궁극기 칼\r\n\nLeftshift: 대쉬\r\n\nContral+방향키\r\n\nSpace:공격";
		characterInfo[4] = "이동 → ← ↑ ↓\n\n공격 : Space\n폭탄 : X \n\n 폭탄 사용시 \n 게이지 소모";
		characterInfo[5] = "이동 → ← ↑ ↓\n\n자동공격\n\n대쉬 Space";

	}

}
