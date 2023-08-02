using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
	public static UI_Manager instance;

	public GameObject obj_panel;

	public Text gameStatus;
	public GameObject obj_nextBtn;
	public GameObject obj_reBtn;

	public string nextSceneName;
	public string nowSceneName;


	Coroutine co_fadeIn;
	Coroutine co_fadeOut;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);

		}
	}

	void Start()
    {
		_Init();
		co_fadeIn = StartCoroutine(FadeIn());
	}

	void _Init()
	{
		obj_panel.GetComponent<Image>().color = Color.black; // 검정색으로 시작해서 페이드인 되어야 함
		gameStatus.gameObject.SetActive(false);
		obj_nextBtn.SetActive(false);
		obj_reBtn.SetActive(false);
	}

	public void CharacterSelect_FadeOut()
	{
		co_fadeOut = StartCoroutine(FadeOut());

		gameStatus.gameObject.SetActive(false);
		obj_nextBtn.SetActive(false);
		obj_reBtn.SetActive(false);


		if (obj_panel.GetComponent<Image>().color.a >= 1)
		{
			StopCoroutine(co_fadeOut);
		}
	}


	void GameClear_UI()
	{
		co_fadeOut = StartCoroutine(FadeOut());

		gameStatus.text = "Game Clear";
		obj_nextBtn.GetComponent<Button>().interactable = true;
		obj_reBtn.GetComponent<Button>().interactable = false;

		gameStatus.gameObject.SetActive(true);
		obj_nextBtn.gameObject.SetActive(true);
		obj_reBtn.gameObject.SetActive(true);

		if (obj_panel.GetComponent<Image>().color.a >= 1)
		{
			StopCoroutine(co_fadeOut);
		}
	}

	void GameOver_UI()
	{
		co_fadeOut = StartCoroutine(FadeOut());

		gameStatus.text = "Game Over";
		obj_nextBtn.GetComponent<Button>().interactable = false;
		obj_reBtn.GetComponent<Button>().interactable = true;

		gameStatus.gameObject.SetActive(true);
		obj_nextBtn.gameObject.SetActive(true);
		obj_reBtn.gameObject.SetActive(true);

		if (obj_panel.GetComponent<Image>().color.a >= 1)
		{
			StopCoroutine(co_fadeOut);
		}
	}

	public void NextBtn()
	{
		SceneManager.LoadScene(nextSceneName);
	}
	public void RestartBtn()
	{
		SceneManager.LoadScene(nowSceneName);
	}

	// 어두운 화면에서 게임 화면으로 전환됨
	IEnumerator FadeIn()
	{
		Color tmp = new Color(0f, 0f, 0f, 0.05f);
		while (obj_panel.GetComponent<Image>().color.a > 0)
		{
			yield return new WaitForSeconds(0.05f);
			//Debug.Log(obj_panel.GetComponent<Image>().color.a);
			obj_panel.GetComponent<Image>().color -= tmp;
		}
	}

	IEnumerator FadeOut()
	{
		Color tmp = new Color(0f, 0f, 0f, 0.05f);
		while (obj_panel.GetComponent<Image>().color.a < 1)
		{
			yield return new WaitForSeconds(0.05f);
			//Debug.Log(obj_panel.GetComponent<Image>().color.a);
			obj_panel.GetComponent<Image>().color += tmp;
		}
	}

}
