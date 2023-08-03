using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Revive : MonoBehaviour
{
    public GameObject[] SelectBtn;
    public GameObject[] Character;
    public GameObject SelectPanel;
    public GameObject SelectArrow;


    public Vector3 originArrowPos;
    public bool _isSlectAble;

    public int index = 0;

    public int beforeIndex = 0;

    Coroutine arrow;

    private void Start()
    {
      
        
    }
    public void SetSelectUI(bool b)
    {
        
  
        SelectPanel.SetActive(b);

        _isSlectAble = b;
        SetArrowPos();

        if (b)
        {

            if(arrow == null)
            arrow = StartCoroutine(MoveArrow());


        }

		if (!b)
            StopAllCoroutines();
    }


    public void Update()
    {



        if (_isSlectAble)
        {

            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                index++;
                
                if(index >=5)
                {
                    index = 0;
                }

                SetArrowPos();
             
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                index--;

                if (index < 0)
                {
                  index =  SelectBtn.Length - 1;

                }

                SetArrowPos();
            }

            


            if(Input.GetKeyDown(KeyCode.Return))
            {
                GameObject instanceObject = null;
                switch(index)
                {
                    case 0:
                        instanceObject = UI_Manager.instance.SJ_Player;
                        break;
                    case 1:
                        instanceObject = UI_Manager.instance.JW_Player;
                        break;
                    case 2:
                        instanceObject = UI_Manager.instance.JH_Player;
                        break;
                    case 3:
                        instanceObject = UI_Manager.instance.DW_Player;
                        break;
                    case 4:
                        instanceObject = UI_Manager.instance.YS_Player;
                        break;

                }
                UI_Manager.instance.now_Player = instanceObject;
                UI_Manager.instance.Revive();
                Debug.Log("ºÎÈ°µÊ");
                
                SetSelectUI(false);
            }

        }
    }

    public void SetArrowPos()
    {
        var pos = SelectArrow.transform.position;
        pos.x = SelectBtn[index].transform.position.x;
        SelectArrow.transform.position = pos;

        SelectBtn[beforeIndex].GetComponent<Image>().color = Color.white;
        SelectBtn[index].GetComponent<Image>().color = Color.green;
   

        beforeIndex = index;

    }

    IEnumerator MoveArrow()
    {

        bool b_up = true;

        while (true)
        {
            if (b_up)
            {
                SelectArrow.transform.Translate(Vector3.up * 25.0f * Time.deltaTime);

                if (SelectArrow.transform.localPosition.y >= -150f)
                {
                    b_up = false;
                }


            }
            else
            {
                SelectArrow.transform.Translate(Vector3.up * -25.0f * Time.deltaTime);

                if (SelectArrow.transform.localPosition.y <= -180f)
                {
                    b_up = true;
                }

            }

            yield return new WaitForEndOfFrame();

        }

    }




}
