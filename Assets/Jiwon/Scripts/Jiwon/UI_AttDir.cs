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

			// 값이 0이하가 되면 z값이 360더해지도록 (0~360도로 움직이게 하기 위함)
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
			// 값이 360을 넘어가면 z값에서 360을 빼기 (0~360도로 움직이게 하기 위함)
			if (transform.rotation.z >= 360f)
			{
				Vector3 v = new Vector3(0f, 0f, 360f);
				transform.Rotate(v);
			}
		}
	}

}
