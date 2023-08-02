using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject background;
	public GameObject btn_restart;
	public GameObject btn_close;

	Vector2 vec_moveBg = Vector2.down;
	float moveBgSpeed = 3f;

	private void Start()
	{
		// 처음에 버튼 안 보이게 SetActive(false)
		btn_restart.SetActive(false);
		btn_close.SetActive(false);
	}
	private void Update()
	{
		if (background.transform.position.y > -75f)
		{
			background.transform.Translate(vec_moveBg * moveBgSpeed *Time.deltaTime);
		}
		// 다 올라갔으면 else 실행
		else
		{
			btn_restart.SetActive(true);
			btn_close.SetActive(true);
		}

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
