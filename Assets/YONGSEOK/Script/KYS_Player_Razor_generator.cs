using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KYS_Player_Razor_generator : MonoBehaviour
{

	public List<GameObject> bullets = new List<GameObject>();
	int curent_bullet = 0;

	float razor_cool_max = 270f;		//270프레임 마다 발사 
	float razor_cool = 0;
	float razor_delay = -10f;
	public GameObject obj_Razor;


	private void Update()
	{
		
		razor_cool -= Time.deltaTime;
		if (razor_cool <= 0)
		{
			
			Instantiate(obj_Razor, transform.position, Quaternion.identity);
			if(razor_cool < razor_delay*Time.deltaTime)
				razor_cool = razor_cool_max * Time.fixedDeltaTime;
		}
	}




}
