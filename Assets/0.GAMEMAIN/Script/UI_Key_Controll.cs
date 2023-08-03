using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Key_Controll : MonoBehaviour
{
    public bool isPause = false;
    public bool isSuperPower = false;
    public GameObject pause_panel;
    public GameObject superPower_panel;
    void Update()
    {

        //���� ���� ���. �ÿ��� ���� ���İ��� ���� ���� ����
        if(Input.GetKeyDown(KeyCode.F1)){
            if (isPause)
            {
                Time.timeScale = 1;
                pause_panel.SetActive(false);
                isPause = false;
            }
            else
            {
                Time.timeScale = 0;
                pause_panel.SetActive(true);
                isPause = true;
            }

        }
        //�ݶ��̴� ���ֱ� ���.
        if (Input.GetKeyDown(KeyCode.F2))
        {

            if (UI_Manager.instance.now_Player_Instance != null && UI_Manager.instance.now_Player_Instance.GetComponent<Collider2D>() != null)
            {
                if (isSuperPower)
                {
                    UI_Manager.instance.now_Player_Instance.GetComponent<Collider2D>().enabled = true;
                    superPower_panel.SetActive(false);
                    isSuperPower = false;
                }
                else
                {
                    UI_Manager.instance.now_Player_Instance.GetComponent<Collider2D>().enabled = false;
                    superPower_panel.SetActive(true);
                    isSuperPower = true;
                }
            }
        }

        if (UI_Manager.instance.now_Player_Instance != null && UI_Manager.instance.now_Player_Instance.GetComponent<Collider2D>() != null)
        {
            if (UI_Manager.instance.now_Player_Instance.GetComponent<Collider2D>().enabled == true)
            {
                superPower_panel.SetActive(false);
                isSuperPower = false;
            }
        }


    }
}
