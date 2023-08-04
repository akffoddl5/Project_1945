using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UI_Manager : MonoBehaviour
{
    public static UI_Manager instance;
    public bool b_isGameStart = false;
    public GameObject obj_panel;

    public Text gameStatus;
    public GameObject obj_nextBtn;
    public GameObject obj_reBtn;

    public string nextSceneName;
    public string nowSceneName;

    [Header("�ݶ��̴� �����")]
    public ColiderChecker _colliderChecker;

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

    bool b_first = true;

    Coroutine co_fadeIn;
    Coroutine co_fadeOut;
    Coroutine co_revive;
    Coroutine co_reverseMove;
    Coroutine co_move;
    private void Awake()
    {
        SetResolutionScreen();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        prefab_dict.Add(Charactor.����, SJ_Player);
        prefab_dict.Add(Charactor.����, JW_Player);
        prefab_dict.Add(Charactor.����, JH_Player);
        prefab_dict.Add(Charactor.����, DW_Player);
        prefab_dict.Add(Charactor.�뼮, YS_Player);

		scene_dict.Add(1, "Dowoon");
		scene_dict.Add(2, "YONGSEOK");
		scene_dict.Add(3, "Jiwon");
		//scene_dict.Add(1, "Jiwon");
		scene_dict.Add(4, "kjh_sceen1");
		scene_dict.Add(5, "June_Scene");
		scene_dict.Add(6, "GameEnd");
		scene_dict.Add(7, "GameEnd");
		scene_dict.Add(0, "CharacterSelect 1");


		current_stage = 1;
	}

    
    void Start()
    {
        _colliderChecker = GetComponentInChildren<ColiderChecker>();

        SetResolutionScreen();
        _Init();
        co_fadeIn = StartCoroutine(FadeIn());
    }

    public void _Init()
    {
        if (GetComponent<UI_Revive>()._isSlectAble == true)
            GetComponent<UI_Revive>().SetSelectUI(false);

		obj_panel.GetComponent<Image>().color = Color.black; // ���������� �����ؼ� ���̵��� �Ǿ�� ��
        gameStatus.gameObject.SetActive(false);
        obj_nextBtn.SetActive(false);
        obj_reBtn.SetActive(false);

        _colliderChecker.BoxHide();


        if (b_first)
        {
            SceneManager.sceneLoaded += test;
            b_first = false;
        }
	}


    public void SetResolutionScreen()
    {

        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Screen.SetResolution(540, 960, true);
       
    }

	public IEnumerator Sound_Kill()
	{
		if (Camera.main.gameObject.GetComponent<AudioSource>() == null) yield break;
		
		while (true)
		{
			Debug.Log(Camera.main.gameObject.GetComponent<AudioSource>().volume);
			Camera.main.gameObject.GetComponent<AudioSource>().volume -= 0.2f;
			yield return new WaitForSeconds(1f);

            if (Camera.main)
            {
                if (Camera.main.gameObject.GetComponent<AudioSource>().volume <= 0f)
                {
                    Camera.main.gameObject.GetComponent<AudioSource>().volume = 0f;
                    break;
                }
            }
		}
		yield return new WaitForSeconds(0.5f);
		//audioSource_clear.Play();

	}



	public void test(Scene arg0, LoadSceneMode arg1)
	{
		StartCoroutine(FadeIn());

        //Debug.Log(arg0 + "   �ε�� " + arg0.name + " " + arg1.ToString());
        if (arg0.name != "GameEnd")
        {


         
            StartCoroutine(Playerspawn());

        }
		//Debug.Log("���� ����, ���̵��� ����");
	}

	public void Update()
	{
      
            //Debug.Log("check ��..");
        if (b_isGameStart)
        {
            //Debug.Log("check ��22..");
            if (now_Player_Instance == null)
            {
                //Debug.Log("check ��33..");
                //Debug.Log("��Ȱ ui ����");
                GetComponent<UI_Revive>().SetSelectUI(true);


            }
        }

        if(Input.GetKeyDown(KeyCode.F3))
        {
            _colliderChecker.b_isVisible = _colliderChecker.b_isVisible == true ? false : true;

            if (_colliderChecker.b_isVisible == false)
                _colliderChecker.BoxHide();

        }
        
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
        StartCoroutine(GameClear());

	
		

	}

    public IEnumerator GameClear()
    {
        Debug.Log("clear ui");
        StartCoroutine(PlayerReverseSpawn());
        StartCoroutine(Sound_Kill());
        yield return StartCoroutine(FadeOut(2f));

        gameStatus.text = "Game Clear";
        obj_nextBtn.GetComponent<Button>().interactable = true;
        obj_reBtn.GetComponent<Button>().interactable = false;

        gameStatus.gameObject.SetActive(true);
        obj_nextBtn.gameObject.SetActive(true);
        obj_reBtn.gameObject.SetActive(true);

        _colliderChecker.BoxHide();
    }

    //void GameOver_UI()
    //{
    //    co_fadeOut = StartCoroutine(FadeOut());

    //    gameStatus.text = "Game Over";
    //    obj_nextBtn.GetComponent<Button>().interactable = false;
    //    obj_reBtn.GetComponent<Button>().interactable = true;

    //    gameStatus.gameObject.SetActive(true);
    //    obj_nextBtn.gameObject.SetActive(true);
    //    obj_reBtn.gameObject.SetActive(true);

    //}

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

    public void PlayerDie()
    {
        GetComponent<UI_Revive>().SetSelectUI(true);
    }

	public void Revive() { StartCoroutine(PlayerRevive()); }

	IEnumerator PlayerRevive()
    {
        if (now_Player_Instance != null)
        {
            Destroy(now_Player_Instance);
        }


        if (now_Player_Instance == null)
        {
            now_Player_Instance = Instantiate(now_Player, new Vector3(0, -6, 0), Quaternion.identity);
        }

        now_Player_Instance.transform.position = new Vector3(0, -6, 0);

        //���������� 2D�ݶ��̴� ���� Disable
        if (now_Player_Instance.GetComponent<Collider2D>() != null)
        {
            now_Player_Instance.GetComponent<Collider2D>().enabled = false;
        }

         StartCoroutine(PlayerMove(1.0f,1.5f));
        yield return StartCoroutine(PlayerBlink());

        now_Player_Instance.GetComponent<Collider2D>().enabled = true;
	}
    IEnumerator Playerspawn()
    {
        _Init();

        if (GameObject.FindGameObjectWithTag("Player") != now_Player_Instance)
        {
            Debug.Log("�÷��̾� ������");
            Destroy(GameObject.FindGameObjectWithTag("Player"));
        }

        if (now_Player_Instance == null)
        {
            now_Player_Instance = Instantiate(now_Player, new Vector3(0, -6, 0), Quaternion.identity);
        }

        //���� ��ó��
        now_Player_Instance.transform.position = new Vector3(0, -6, 0);

        //���������� 2D�ݶ��̴� ���� Disable
        if (now_Player_Instance.GetComponent<Collider2D>() != null)
        {
            now_Player_Instance.GetComponent<Collider2D>().enabled = false;
        }


        //������..
        StartCoroutine(PlayerMove(1.0f,1.5f));
        yield return StartCoroutine(PlayerBlink());
      

		b_isGameStart = true;	
        //������ó�� (�ݶ��̴� �ٽ� ų�� ����)
        now_Player_Instance.GetComponent<Collider2D>().enabled = true;
        



    }
    IEnumerator PlayerBlink()
    {
        SpriteRenderer spr = null;
        if (now_Player_Instance)
        {
            if (now_Player_Instance.GetComponent<SpriteRenderer>())
            {
                spr = now_Player_Instance.GetComponent<SpriteRenderer>();
               
            }
        }
        int blinkCount = 0;
        while (blinkCount++ <= 25)
        {
           
            yield return new WaitForSeconds(0.1f);
            var c = spr.color;
        
            c.a = spr.color.a >= 1f ? 0.3f : 1f;
           
            spr.color = c;
        }
        var c2 = spr.color;
        c2.a = 1f;
        spr.color = c2;

        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator PlayerMove(float time, float speed)
    {
        float t = 0;
        now_Player_Instance.GetComponent<Collider2D>().isTrigger = true;
        while (true)
        {
            //1�ʸ���
            yield return new WaitForFixedUpdate(); //����

            now_Player_Instance.transform.Translate(0, speed * Time.deltaTime, 0); //�÷��̾� ���� ��ġ�κ��� ������ ���� ����.

            t += Time.deltaTime;
            if (t >= time)
            {
               
                now_Player_Instance.GetComponent<Collider2D>().isTrigger = false;
                 break;
            }
        }
    }

    IEnumerator PlayerReverseSpawn()
    {


        //���������� 2D�ݶ��̴� ���� Disable
        if (now_Player_Instance.GetComponent<Collider2D>() != null)
        {
            now_Player_Instance.GetComponent<Collider2D>().enabled = false;
        }


        //������ ������..
        co_reverseMove  = StartCoroutine(PlayerMove(2.0f,4.0f));

        //yield return new WaitForSeconds(1);

        yield return new WaitForSeconds(0.1f);

    }



    // ��ο� ȭ�鿡�� ���� ȭ������ ��ȯ��
    IEnumerator FadeIn()
    {
        Color tmp = new Color(0f, 0f, 0f, 0.05f);
        while (obj_panel.GetComponent<Image>().color.a > 0)
        {
            yield return new WaitForSeconds(0.02f);
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

    public IEnumerator LoadScene(int stage)
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