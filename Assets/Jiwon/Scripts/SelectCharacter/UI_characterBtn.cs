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

		// ĳ���� �̸� �迭 �ʱ�ȭ
		characterName[0] = "����";
		characterName[1] = "�ƾ�";
		characterName[2] = "�ܰ���";
		characterName[3] = "��Ʈ";
		characterName[4] = "�ﰢ";
		characterName[5] = "����";

		// ĳ���� ���� �迭 �ʱ�ȭ
		characterInfo[0] = "������ ĳ���� ����";
		characterInfo[1] = "�̵� �� �� �� ��\n\n���� z(Ű�ٿ� ����)\n\n�浹����ǥ�� �� õõ�� �̵� Shift\n\n";
		characterInfo[2] = "�̵� �� �� �� ��\n\n���� z\n\n���ݹ�����ȯ\n(�ð�) x\n\n���ݹ�����ȯ\n(�ݽð�) c";
		characterInfo[3] = "��Ʈ ĳ���� ����";
		characterInfo[4] = "�ﰢ ĳ���� ����";
		characterInfo[5] = "�̵� �� �� �� ��\n\n�ڵ�����\n\n�뽬 Space";

	}

}
