using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class June_PlayerBossTalk : MonoBehaviour
{

    public Text dialogText;
    public string[] dialogue;
    int June_Index;

    public float wordSpeed;
    public bool playerisclose;
    public GameObject Txttime;

    public GameObject Aya;
    public GameObject dncmgh;

    public GameObject Jiwon;
    public GameObject jung;
    public GameObject Yong;
    public GameObject Do;


    GameObject playerselection;

    public void Start()
    {
        June_Index = 0;


        Aya = GameObject.Find("June_Canvas").transform.GetChild(4).gameObject;
        dncmgh = GameObject.Find("June_Canvas").transform.GetChild(5).gameObject;
        Jiwon = GameObject.Find("June_Canvas").transform.GetChild(6).gameObject;
        jung = GameObject.Find("June_Canvas").transform.GetChild(7).gameObject;
        Yong = GameObject.Find("June_Canvas").transform.GetChild(8).gameObject;
        Do = GameObject.Find("June_Canvas").transform.GetChild(9).gameObject;



        if (ITEM_MANAGER.instance.current_character == Charactor.신준)
        { playerselection = Aya; }
        if (ITEM_MANAGER.instance.current_character == Charactor.지원)
        { playerselection = Jiwon; }
        if (ITEM_MANAGER.instance.current_character == Charactor.정현)
        { playerselection = jung; }
        if (ITEM_MANAGER.instance.current_character == Charactor.도운)
        { playerselection = Do; }
        if (ITEM_MANAGER.instance.current_character == Charactor.용석)
        { playerselection = Yong; }



        StopAllCoroutines();
        StartCoroutine(FadeInStart());
        StartCoroutine(Typing());




    }



    private void Update()
    {





        if (June_Index % 2 == 0)
        {
            StartCoroutine(CharactorFadeInStart(1f, playerselection));
            StartCoroutine(CharactorFadeOutStart(0.5f, dncmgh));


        }
        if (June_Index % 2 != 0)
        {

            StartCoroutine(CharactorFadeInStart(1f, dncmgh));
            StartCoroutine(CharactorFadeOutStart(0.5f, playerselection));
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            NextLine();
        }

    }

    public void NextLine()
    {
        if (June_Index < dialogue.Length - 1)
        {
            June_Index++;
            GameObject.Find("PlayerTxt").GetComponent<Text>().text = " ";
            StartCoroutine(Typing());


        }
        if (June_Index == dialogue.Length - 1)
        {


            Invoke("DestroyTheTxt", 2f);



        }


    }
    private void OnEnable()
    {

    }
    void DestroyTheTxt()
    {
        StartCoroutine(FadeOutStart());
        gameObject.GetComponent<June_BossBullet>().enabled = true;
        GameObject.Find("June_Canvas").transform.GetChild(3).gameObject.SetActive(false);

        gameObject.GetComponent<CircleCollider2D>().enabled = true;

        StartCoroutine(CharactorFadeOutStart(0f, playerselection));
        StartCoroutine(CharactorFadeOutStart(0f, dncmgh));
        Aya.SetActive(false);
        dncmgh.SetActive(false);
        Jiwon.SetActive(false);
        jung.SetActive(false);
        Yong.SetActive(false);
        Do.SetActive(false);


    }

    IEnumerator Typing()
    {
        foreach (char letter in dialogue[June_Index].ToCharArray())
        {
            GameObject.Find("PlayerTxt").GetComponent<Text>().text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }




    public IEnumerator CharactorFadeInStart(float i, GameObject charactor)
    {
        for (float f = 0f; f < i; f += 0.2f)
        {
            Color c3 = charactor.GetComponent<Image>().color;
            c3.a = f;
            charactor.GetComponent<Image>().color = c3;
            yield return null;
        }
    }
    public IEnumerator CharactorFadeOutStart(float i, GameObject charactor)
    {

        for (float f = 1f; f > i; f -= 0.2f)
        {
            Color c3 = charactor.GetComponent<Image>().color;
            c3.a = f;
            charactor.GetComponent<Image>().color = c3;
            yield return new WaitForSeconds(0);
        }
    }




    //페이드 아웃
    public IEnumerator FadeOutStart()
    {
        GameObject.Find("June_Canvas").transform.GetChild(3).gameObject.SetActive(true);
        for (float f = 0.5f; f > 0; f -= 0.02f)
        {

            Color c1 = GameObject.Find("June_Canvas").transform.GetChild(2).gameObject.GetComponent<Image>().color;
            c1.a = f;
            Color c2 = GameObject.Find("June_Canvas").transform.GetChild(3).gameObject.GetComponent<Image>().color;
            c2.a = f;

            GameObject.Find("June_Canvas").transform.GetChild(2).gameObject.GetComponent<Image>().color = c1;
            GameObject.Find("June_Canvas").transform.GetChild(3).gameObject.GetComponent<Image>().color = c2;
            yield return new WaitForSeconds(0);
            GameObject.Find("June_Canvas").transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("June_Canvas").transform.GetChild(3).gameObject.SetActive(false);
        }
    }
    //페이드 인
    public IEnumerator FadeInStart()
    {
        GameObject.Find("June_Canvas").transform.GetChild(3).gameObject.SetActive(true);
        for (float f = 0f; f < 0.5; f += 0.02f)
        {

            Color c1 = GameObject.Find("June_Canvas").transform.GetChild(2).gameObject.GetComponent<Image>().color;
            c1.a = f;
            Color c2 = GameObject.Find("June_Canvas").transform.GetChild(3).gameObject.GetComponent<Image>().color;
            c2.a = f;

            GameObject.Find("June_Canvas").transform.GetChild(2).gameObject.GetComponent<Image>().color = c1;
            GameObject.Find("June_Canvas").transform.GetChild(3).gameObject.GetComponent<Image>().color = c2;
            yield return null;
        }
    }

}
