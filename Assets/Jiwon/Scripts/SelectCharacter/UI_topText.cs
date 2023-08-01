using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class UI_topText : MonoBehaviour
{
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
			tmp.a -= 0.03f;
			if (tmp.a <= 0.2f)
			{
				tmp.a = 0.2f;
				isAUp = true;
			} 
			transform.GetComponent<Text>().color = tmp;
		}
		else
		{
			tmp.a += 0.03f;
			if (tmp.a >= 1)
			{
				tmp.a = 1f;
				isAUp = false;
			}
			transform.GetComponent<Text>().color = tmp;
		}

	}
}
