using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Ending : MonoBehaviour
{
    public GameObject background;
	public GameObject btn_restart;
	public GameObject btn_close;

	// 보스 프리팹 훔쳐오기
	[Header("보스 프리팹")]
	public GameObject pref_DW_Boss;
	public GameObject pref_YS_Poo;
	public GameObject pref_YS_Toong;
	public GameObject pref_JW_Boss;
	public GameObject pref_JH_fish;
	public GameObject pref_JH_light;
	public GameObject pref_SJ_Boss;

	bool isSpawnDW = false;
	bool isSpawnJW = false;
	bool isSpawnJH = false;
	bool isSpawnYS = false;
	bool isSpawnSJ = false;

	Vector2 bg_startPosition = new Vector2(0f, 17f);
	Vector2 vec_moveBg = Vector2.down;
	float moveBgSpeed = 2f;

	private void Start()
	{
		// 배경 처음 위치 설정
		background.transform.position = bg_startPosition;
		// 처음에 버튼 안 보이게 SetActive(false)
		btn_restart.SetActive(false);
		btn_close.SetActive(false);
		UI_Manager.instance._Init();
		UI_Manager.instance.b_isGameStart = false;
		UI_Manager.instance.GetComponent<UI_Revive>().SetSelectUI(false);
	}
	private void Update()
	{
		MakeBoss();
		BackgroundMove();
	}
	void BackgroundMove()
	{
		if (background.transform.position.y > -72f)
		{
			background.transform.Translate(vec_moveBg * moveBgSpeed * Time.deltaTime);
		}
		// 다 올라갔으면 else 실행
		else
		{
			btn_restart.SetActive(true);
			btn_close.SetActive(true);
		}
	}
	void MakeBoss()
	{
		// pref_SJ_Boss
		if (background.transform.position.y <= 16f && !isSpawnSJ)
		{
			GameObject sj = Instantiate(pref_SJ_Boss, new Vector2(0, 3), Quaternion.identity);
			Destroy(sj, 8f);
			isSpawnSJ = true;
		}
		//isSpawnJH
		if (background.transform.position.y <= 1f && !isSpawnJH)
		{
			StartCoroutine(Monster_jh());
			isSpawnJH = true;
		}
		// pref_JW_Boss
		if (background.transform.position.y < -16f && !isSpawnJW)
		{
			GameObject jw = Instantiate(pref_JW_Boss, new Vector2(0, 3.5f), Quaternion.identity);
			Destroy(jw, 8f);
			isSpawnJW = true;
		}
		// YS_Boss
		if (background.transform.position.y < -33f && !isSpawnYS)
		{
			GameObject poo = Instantiate(pref_YS_Poo, new Vector2(1.5f, 3), Quaternion.identity);
			GameObject toong = Instantiate(pref_YS_Toong, new Vector2(-1.5f, 3), Quaternion.identity);

			Destroy(poo, 7f);
			Destroy(toong, 7f);

			isSpawnYS = true;
		}
		// DW_Boss
		if (background.transform.position.y < -49f && !isSpawnDW)
		{
			GameObject dw = Instantiate(pref_DW_Boss, new Vector2(2, 3), Quaternion.identity);
			Destroy(dw, 13.5f);
			isSpawnDW = true;
		}
	}
	IEnumerator Monster_jh()
	{
		GameObject fish1 = Instantiate(pref_JH_fish, new Vector2(0f, 2.5f), Quaternion.identity);
		yield return new WaitForSeconds(3f);
		GameObject li = Instantiate(pref_JH_light, new Vector2(0f, 3f), Quaternion.identity);
		yield return new WaitForSeconds(3f);
		GameObject fish2 = Instantiate(pref_JH_fish, new Vector2(0f, 2.5f), Quaternion.identity);
	}

	public void RestertBtn()
	{
		// 캐릭터 선택 씬 로드되도록 만들기
		//SceneManager.LoadScene("캐릭터 선택 씬");
	}
	public void CloseBtn()
	{
		Application.Quit();
	}
}
