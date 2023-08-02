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

	public GameObject DW_Player;
	public GameObject JW_Player;
	public GameObject YS_Player;
	public GameObject JH_Player;
	public GameObject SJ_Player;
	public GameObject now_Player;
	public GameObject now_Player_Instance;

	public Dictionary<Charactor, GameObject> prefab_dict = new Dictionary<Charactor, GameObject>();
	public Dictionary<int, string> scene_dict = new Dictionary<int, string>();

	

	public int current_stage = 1;

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

		prefab_dict.Add(Charactor.신준, SJ_Player);
		prefab_dict.Add(Charactor.지원, JW_Player);
		prefab_dict.Add(Charactor.정현, JH_Player);
		prefab_dict.Add(Charactor.도운, DW_Player);
		prefab_dict.Add(Charactor.용석, YS_Player);

		scene_dict.Add(3, "Dowoon");
		scene_dict.Add(1, "YONGSEOK");
		scene_dict.Add(2, "Jiwon");
		scene_dict.Add(4, "kjh_sceen1");
		scene_dict.Add(5, "June_Scene");

		current_stage = 1;
	}

	void Start()
    {
		_Init();
		co_fadeIn = StartCoroutine(FadeIn());
		SceneManager.LoadScene("Jiwon");
	}

	void _Init()
	{
		obj_panel.GetComponent<Image>().color = Color.black; // 검정색으로 시작해서 페이드인 되어야 함
		gameStatus.gameObject.SetActive(false);
		obj_nextBtn.SetActive(false);
		obj_reBtn.SetActive(false);
	}

	

	public void CharacterSelect_FadeOut(Charactor a)
	{
		//StartCoroutine(FadeOut());
		Debug.Log(current_stage);

		now_Player = prefab_dict[a];
		nowSceneName = scene_dict[current_stage];
		now_Player_Instance = Instantiate(now_Player, new Vector3(0, -2, 0), Quaternion.identity);
		
	
		//KYS_Player_move tmp1 = now_Player_Instance.GetComponent<KYS_Player_move>();
		//if (tmp1 != null) tmp1.enabled = false;
		
		DontDestroyOnLoad(now_Player_Instance);
		StartCoroutine(LoadScene(1));
		
	}


	void GameClear_UI()
	{
		StartCoroutine(FadeOut());

		gameStatus.text = "Game Clear";
		obj_nextBtn.GetComponent<Button>().interactable = true;
		obj_reBtn.GetComponent<Button>().interactable = false;

		gameStatus.gameObject.SetActive(true);
		obj_nextBtn.gameObject.SetActive(true);
		obj_reBtn.gameObject.SetActive(true);

	}

	void GameOver_UI()
	{
		StartCoroutine(FadeOut());

		gameStatus.text = "Game Over";
		obj_nextBtn.GetComponent<Button>().interactable = false;
		obj_reBtn.GetComponent<Button>().interactable = true;

		gameStatus.gameObject.SetActive(true);
		obj_nextBtn.gameObject.SetActive(true);
		obj_reBtn.gameObject.SetActive(true);

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
		yield break;
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
		yield break;
	}

	IEnumerator LoadScene(int stage)
	{
		StartCoroutine(FadeOut());
		yield return new WaitForSeconds(1);

		AsyncOperation oper = SceneManager.LoadSceneAsync(scene_dict[stage]);
		while (!oper.isDone)
		{
			yield return null;
			Debug.Log(oper.progress);
		}
		StartCoroutine(FadeIn());
		//Debug.Log("start cor");
		//SceneManager.LoadScene(scene_dict[stage]);


	}





}