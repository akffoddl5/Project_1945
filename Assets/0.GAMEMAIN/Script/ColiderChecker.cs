using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColiderChecker : MonoBehaviour
{

    List<GameObject> Rect = new List<GameObject>();
    private List<GameObject> TargetRect = new List<GameObject>();
    [SerializeField]
    GameObject RectPrefab = null;


    int RectCount = 250;

    public bool b_isVisible = false;

    // Start is called before the first frame update
    void Awake()
    {


        DontDestroyOnLoad(this.gameObject);
    }

    public void BoxHide()
    {
        if (Rect.Count > 0)
        {
            for (int i=0; i< Rect.Count; ++i)
            {
                if (Rect[i] != null)
                {
                    Destroy(Rect[i]);
                }

            } 
        }
        Rect.Clear();
    
    }
    // Update is called once per frame

    void Update()
    {

        if (b_isVisible)
        {
            var box = Physics2D.OverlapBoxAll(new Vector2(0, 0), new Vector2(15, 15), 0f);
            for (int i = 0; i < box.Length; ++i)
            {
                if (box[i].GetComponentInChildren<CheckBox>() == null &&! box[i].CompareTag("Wall"))
                {
                    var b = Instantiate(RectPrefab, Vector3.zero, Quaternion.identity);
                    var RectLocalScale = b.transform.localScale;
                    RectLocalScale = box[i].gameObject.GetComponent<Collider2D>().bounds.size;
                    b.transform.localScale = RectLocalScale;

                    b.SetActive(true);
                    b.GetComponent<CheckBox>().targetObject = box[i].gameObject;
                    b.transform.parent = box[i].transform;

                    Rect.Add(b);
                }
            }


            if (Rect.Count > 0)
            {
                for (int i = 0; i < Rect.Count; ++i)
                {
                    if (Rect[i] != null)
                    {
                        if (Rect[i].GetComponent<CheckBox>().targetObject == null)
                        {
                            Rect.Remove(Rect[i]);
                            Destroy(Rect[i]);
                        }
                    }

                }
            }
        }
    }
}




        //    var box = Physics2D.OverlapBoxAll(new Vector2(0, 0), new Vector2(10, 10), 0f);
        //    GameObject checkbox = null;
        //    for (int i = 0; i < box.Length; ++i)

        //    {
        //        if (box[i].gameObject != null && !box[i].gameObject.CompareTag("Wall"))
        //        {

        //            if (!CheckIsOnTarget(box[i].gameObject))
        //            {
        //                for (int j = 0; j < RectCount; ++j)
        //                {
        //                    if (Rect[j].GetComponent<CheckBox>().targetObject == null)
        //                    {

        //                        checkbox = Rect[j];
        //                        checkbox.SetActive(true);
        //                        checkbox.GetComponent<CheckBox>().targetObject = box[i].gameObject;
        //                        checkbox.transform.parent = null;

        //                        TargetRect.Add(checkbox);
        //                        break;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    if (TargetRect.Count > 0)
        //    {
        //        for (int i = 0; i < TargetRect.Count; ++i)
        //        {            
        //            if (TargetRect[i].GetComponent<CheckBox>().targetObject == null)
        //            {
        //                TargetRect.Remove(TargetRect[i]);
        //                TargetRect[i].transform.parent = transform;
        //               TargetRect[i].SetActive(false);
        //            }
        //        }
        //    }

        //}


   

