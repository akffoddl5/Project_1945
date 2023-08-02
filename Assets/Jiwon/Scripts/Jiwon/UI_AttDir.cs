using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_AttDir : MonoBehaviour
{
    void Update()
    {
		AttDir_UI();

	}

	void AttDir_UI()
	{
		if (Input.GetKeyDown(KeyCode.X))
		{
			Vector3 rVec = new Vector3(0f, 0f, -90f);
			transform.Rotate(rVec);

			// ���� 0���ϰ� �Ǹ� z���� 360���������� (0~360���� �����̰� �ϱ� ����)
			if (transform.rotation.z <= 0f)
			{
				Vector3 v = new Vector3(0f, 0f, 360f);
				transform.Rotate(v);
			}
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			Vector3 rVec = new Vector3(0f, 0f, 90f);
			transform.Rotate(rVec);
			// ���� 360�� �Ѿ�� z������ 360�� ���� (0~360���� �����̰� �ϱ� ����)
			if (transform.rotation.z >= 360f)
			{
				Vector3 v = new Vector3(0f, 0f, 360f);
				transform.Rotate(v);
			}
		}
	}

}
