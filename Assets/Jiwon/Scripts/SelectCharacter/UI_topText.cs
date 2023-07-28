using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UI_topText : MonoBehaviour
{
	float blinkTime;
	bool isAUp = false;

	private void FixedUpdate()
	{
		ChangeColor();
	}

	void ChangeColor()
	{
		Color tmp = transform.GetComponent<Text>().color;
		if (!isAUp)
		{
			tmp.a -= 0.05f;
			if (tmp.a <= 0.5)
			{
				tmp.a = 0.5f;
				isAUp = true;
			} 
			transform.GetComponent<Text>().color = tmp;
		}
		else
		{
			tmp.a += 0.05f;
			if (tmp.a >= 1)
			{
				tmp.a = 1f;
				isAUp = false;
			}
			transform.GetComponent<Text>().color = tmp;
		}

	}
}
