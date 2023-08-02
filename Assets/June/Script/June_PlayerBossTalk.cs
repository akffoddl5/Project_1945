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
    int index;

    public float wordSpeed;
    public bool playerisclose;
    public GameObject Txttime;

    public GameObject Aya;
    public GameObject dncmgh;


   

    public void Start()
    {
        index= 0;
       
        

        Aya = GameObject.Find("Canvas").transform.GetChild(4).gameObject;
        dncmgh = GameObject.Find("Canvas").transform.GetChild(5).gameObject;
       

        StopAllCoroutines();
        StartCoroutine(FadeInStart());
        StartCoroutine(Typing());
    }
    private void Update()
    {
        
        if (index % 2 == 0)
        {
            StartCoroutine(CharactorFadeInStart(1f, Aya));
            StartCoroutine(CharactorFadeOutStart(0.5f, dncmgh));


        }
        if (index % 2 != 0 )
        {

            StartCoroutine(CharactorFadeInStart(1f, dncmgh));
            StartCoroutine(CharactorFadeOutStart(0.5f, Aya));
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            NextLine();
        }
        
    }

    public void NextLine()
    {
        if(index < dialogue.Length-1) 
        {   
            index++;
            GameObject.Find("PlayerTxt").GetComponent<Text>().text = " ";
            StartCoroutine(Typing());


        }
        if(index == dialogue.Length-1) 
        {


            Invoke("DestroyTheTxt", 2f);



        }


    }
    private void OnEnable()
    {
      //  GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerMovement>().enabled = false;
       // GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerShooting>().enabled = false;
    }
    void DestroyTheTxt()
    {
        StartCoroutine(FadeOutStart());
        gameObject.GetComponent<June_BossBullet>().enabled = true;
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
        StartCoroutine(CharactorFadeOutStart(0f, Aya));
        StartCoroutine(CharactorFadeOutStart(0f, dncmgh));
       Aya.SetActive(false);
       dncmgh.SetActive(false);
     //  GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerMovement>().enabled = true;
      //  GameObject.FindGameObjectWithTag("Player").GetComponent<June_PlayerShooting>().enabled = true;
       
    }

     IEnumerator Typing()
    {
        foreach (char letter in dialogue[index].ToCharArray())
        {
            GameObject.Find("PlayerTxt").GetComponent<Text>().text += letter;
            yield return new WaitForSeconds(wordSpeed); 
        }
    }

  
   

    public IEnumerator CharactorFadeInStart(float i,GameObject charactor)
    {
        for (float f = 0f; f < i; f += 0.2f)
        {
            Color c3 = charactor.GetComponent<Image>().color;
            c3.a = f;
            charactor.GetComponent<Image>().color = c3;
            yield return null;
        }
    }
    public IEnumerator CharactorFadeOutStart(float i,GameObject charactor)
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
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
        for (float f = 0.5f; f >0; f -= 0.02f)
        {
            
            Color c1 = GameObject.Find("Canvas").transform.GetChild(2).gameObject.GetComponent<Image>().color;
            c1.a = f; 
            Color c2 = GameObject.Find("Canvas").transform.GetChild(3).gameObject.GetComponent<Image>().color;
            c2.a = f;

            GameObject.Find("Canvas").transform.GetChild(2).gameObject.GetComponent<Image>().color = c1;
            GameObject.Find("Canvas").transform.GetChild(3).gameObject.GetComponent<Image>().color = c2;
            yield return new WaitForSeconds(0);
            GameObject.Find("Canvas").transform.GetChild(2).gameObject.SetActive(false);
            GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(false);
        }
    }
    //페이드 인
    public IEnumerator FadeInStart()
    {
        GameObject.Find("Canvas").transform.GetChild(3).gameObject.SetActive(true);
        for (float f = 0f; f < 0.5; f += 0.02f)
        {
            
            Color c1 = GameObject.Find("Canvas").transform.GetChild(2).gameObject.GetComponent<Image>().color;
            c1.a = f;
            Color c2 = GameObject.Find("Canvas").transform.GetChild(3).gameObject.GetComponent<Image>().color;
            c2.a = f;

            GameObject.Find("Canvas").transform.GetChild(2).gameObject.GetComponent<Image>().color = c1;
            GameObject.Find("Canvas").transform.GetChild(3).gameObject.GetComponent<Image>().color = c2;
            yield return null;
        }
    }

}
