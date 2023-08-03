using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class kjh_BackGround : MonoBehaviour
{
	Material myMaterial;
	float scrollSpeed = 0.03f;
	void Start()
	{
		myMaterial = GetComponent<Renderer>().material;

	}
	void Update()
	{
		float offsety = myMaterial.mainTextureOffset.y;
		Vector2 offset = new Vector2(0, offsety + scrollSpeed * Time.deltaTime);
		myMaterial.mainTextureOffset = offset;

		if (Kjh_fish.CountDeF >= 25)
		{
			if (GameObject.Find("Monstermanager") != null)
			{
				Destroy(GameObject.Find("Monstermanager"),3);
			}
		}

	}



}
