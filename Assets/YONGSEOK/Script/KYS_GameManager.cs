using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KYS_GameManager : MonoBehaviour
{
    public GameObject boss_Furin;
    public GameObject boss_toong;
    public GameObject warning_text;

    public static bool isFurin_die = false;
    public static bool isToong_die = false;


    private void Start()
    {
        //StartCoroutine(Furin_init());   
    }

    private void Update()
    {
        if (isFurin_die)
        {
            Debug.Log("toong init");
            StartCoroutine(Toong_init());
            isFurin_die = false;
        }
    }



    IEnumerator Boss_Furin_init()
    {
        yield return new WaitForSeconds(1);
        Instantiate(boss_Furin, new Vector2(0.2f, 3.2f), Quaternion.identity);

    }

    IEnumerator Boss_Toong_init()
    {
        yield return new WaitForSeconds(1);
        Instantiate(boss_toong, new Vector2(0.2f, 3.2f), Quaternion.identity);
    }

    IEnumerator Warning_repeat()
    {
        var tmpColor = warning_text.GetComponent<Text>().color;
        while (true)
        {
            Debug.Log("watning repeat");
            tmpColor.a = 0;
            warning_text.GetComponent<Text>().color = tmpColor;
            yield return new WaitForSeconds(0.1f);
            tmpColor.a = 255;
            warning_text.GetComponent<Text>().color = tmpColor;
            yield return new WaitForSeconds(0.1f);
            tmpColor.a = 0;
            warning_text.GetComponent<Text>().color = tmpColor;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Furin_init()
    {
        Debug.Log("watning init");
        warning_text.SetActive(true);
        var a = StartCoroutine(Warning_repeat());
        yield return new WaitForSeconds(3);
        StopCoroutine(a);
        warning_text.SetActive(false);
        Instantiate(boss_Furin, new Vector2(0.2f, 3.2f), Quaternion.identity);
    }

    IEnumerator Toong_init()
    {
        Debug.Log("watning init");
        warning_text.SetActive(true);
        var a = StartCoroutine(Warning_repeat());
        yield return new WaitForSeconds(3);
        StopCoroutine(a);
        warning_text.SetActive(false);
        Instantiate(boss_toong, new Vector2(0.2f, 3.2f), Quaternion.identity);
    }
}
