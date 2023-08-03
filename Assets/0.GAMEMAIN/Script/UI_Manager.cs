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


	public AudioSource audioSource_slide;
	public AudioSource audioSource_select;
	public AudioSource audioSource_clear;
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
		scene_dict.Add(2, "YONGSEOK");
		scene_dict.Add(5, "Jiwon");
		scene_dict.Add(1, "kjh_sceen1");
		scene_dict.Add(4, "June_Scene");
		scene_dict.Add(6, "GameEnd");
		scene_dict.Add(7, "GameEnd");
		scene_dict.Add(8, "GameEnd");


		current_stage = 1;
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

		SceneManager.sceneLoaded += test;
	}


	public IEnumerator Sound_Kill()
	{
		if (Camera.main.gameObject.GetComponent<AudioSource>() == null) yield break;
		
		while (true)
		{
			Debug.Log(Camera.main.gameObject.GetComponent<AudioSource>().volume);
			Camera.main.gameObject.GetComponent<AudioSource>().volume -= 0.2f;
			yield return new WaitForSeconds(1f);
			if (Camera.main.gameObject.GetComponent<AudioSource>().volume <= 0f)
			{
				Camera.main.gameObject.GetComponent<AudioSource>().volume = 0f;
				break;
			}
		}
		yield return new WaitForSeconds(0.5f);
		//audioSource_clear.Play();

	}



	public void test(Scene arg0, LoadSceneMode arg1)
	{
		StartCoroutine(FadeIn());
		StartCoroutine(Playerspawn());
		//Debug.Log("스폰 시작, 페이드인 시작");
	}

	

	

	public void CharacterSelect_FadeOut(Charactor a)
	{
		//StartCoroutine(FadeOut());
		//Debug.Log(current_stage);

		now_Player = prefab_dict[a];
		nowSceneName = scene_dict[current_stage];
		
		StartCoroutine(LoadScene(1));
		
	}


	public void GameClear_UI()
	{
		Debug.Log("clear ui");
		StartCoroutine(PlayerReverseSpawn());
		
		StartCoroutine(FadeOut(2f));

		gameStatus.text = "Game Clear";
		obj_nextBtn.GetComponent<Button>().interactable = true;
		obj_reBtn.GetComponent<Button>().interactable = false;

		gameStatus.gameObject.SetActive(true);
		obj_nextBtn.gameObject.SetActive(true);
		obj_reBtn.gameObject.SetActive(true);

		StartCoroutine(Sound_Kill());
		

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

	}

	public void NextBtn()
	{
		current_stage++;

		StartCoroutine(LoadScene(current_stage));
		audioSource_select.Play();
	}
	public void RestartBtn()
	{
		SceneManager.LoadScene(current_stage);
		audioSource_select.Play();
	}

	IEnumerator Playerspawn()
	{
		_Init();
		if (GameObject.FindGameObjectWithTag("Player") != now_Player_Instance)
		{
			Debug.Log("플레이어 삭제됨");
			Destroy(GameObject.FindGameObjectWithTag("Player"));
		}

		if (now_Player_Instance == null)
		{
			now_Player_Instance = Instantiate(now_Player, new Vector3(0, -6, 0), Quaternion.identity);
		}

		//스폰 전처리
		now_Player_Instance.transform.position = new Vector3(0, -6, 0);

		//다형성으로 2D콜라이더 전부 Disable
        if (now_Player_Instance.GetComponent<Collider2D>() != null)
        {
           now_Player_Instance.GetComponent<Collider2D>().enabled =  false;
        }


        //스폰중..
        while (true)
		{
			//1초마다
			yield return new WaitForSeconds(0.01f); //지연
			now_Player_Instance.transform.Translate(0, 3 * Time.deltaTime, 0); //플레이어 생성 위치로부터 맵으로 끌고 오기.

			if (now_Player_Instance.transform.position.y > -4)  break;

		}

		//스폰후처리 (콜라이더 다시 킬지 말지)
		//now_Player_Instance.GetComponent<Collider2D>().enabled = true;
	}

	IEnumerator PlayerReverseSpawn()
	{

		
		//다형성으로 2D콜라이더 전부 Disable
		if (now_Player_Instance.GetComponent<Collider2D>() != null)
		{
			now_Player_Instance.GetComponent<Collider2D>().enabled = false;
		}


		//리버스 스폰중..
		while (true)
		{
			//1초마다
			yield return new WaitForSeconds(0.01f); //지연
			now_Player_Instance.transform.Translate(0, 9 * Time.deltaTime, 0); //플레이어 생성 위치로부터 맵으로 끌고 오기.

			if (now_Player_Instance.transform.position.y > 6) break;

		}

		//yield return new WaitForSeconds(1);

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

	IEnumerator FadeOut(float time_delay)
	{
		yield return new WaitForSeconds(time_delay);
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
			//Debug.Log(oper.progress);
		}
		//StartCoroutine(FadeIn());
		//Debug.Log("start cor");
		//SceneManager.LoadScene(scene_dict[stage]);


	}


	//private void OnLevelWasLoaded(int level)
	//{
	//	StopCoroutine(FadeOut());
	//	GameObject a = GameObject.FindGameObjectWithTag("Player");
	//	Destroy(a);
	//	StartCoroutine(FadeIn());

	//	now_Player_Instance = Instantiate(now_Player, new Vector3(0, -4, 0), Quaternion.identity);
	//	DontDestroyOnLoad(now_Player_Instance);
	//}


}