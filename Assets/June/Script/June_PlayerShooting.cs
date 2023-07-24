using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class June_PlayerShooting : MonoBehaviour
{
    public GameObject bullet = null; //미사일 
    public Transform pos = null; //미사일 발사
    public float FireSpeed = 0.2f;

    public Image Gage;
    public Image Gage_Prefab;
    public float gValue = 0; //게이지 값

    public GameObject Lazer;
    void Start()
    {

        StartCoroutine("AutoFire");
        //Gage = Instantiate(Gage_Prefab, new Vector3(154, 14, 0), Quaternion.Euler(0, 0, 0));

        //var cv = GameObject.FindGameObjectWithTag("Canvas");

        //var childs = cv.GetComponentsInChildren<Image>();
        //for (int i = 0; i < childs.Length; ++i)
        //{
        //    if (childs[i].name == "EnergyBar")
        //    {
        //        Gage.transform.parent = childs[i].transform;
        //    }
        //}

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            Instantiate(bullet, pos.position, Quaternion.identity);
        }

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    gValue += 0.005f;

        //    Gage.fillAmount = gValue;

        //    if (gValue >= 1)
        //    {

        //        //레이저 나가기
        //        GameObject go = Instantiate(Lazer, pos.position, Quaternion.identity);
        //        Destroy(go, 3);
        //        gValue = 0;

        //    }
        //}
        //else
        //{
        //    gValue -= 0.005f;

        //    if (gValue <= 0)
        //        gValue = 0;

        //    Gage.fillAmount = gValue;
        //}

    }
    IEnumerator AutoFire()
    {
        for (; ; )
        {
            if (Input.GetKey(KeyCode.X) == true)
            {
                Instantiate(bullet, pos.position, Quaternion.identity);

            }
            yield return new WaitForSeconds(FireSpeed);

        }
    }
}
