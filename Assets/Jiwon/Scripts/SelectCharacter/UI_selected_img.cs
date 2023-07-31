using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_selected_img : MonoBehaviour
{
    float leftX;
    float middleX;
    float rightX;

    Image img_selected;
    
    // Start is called before the first frame update
    void Start()
    {
        img_selected = gameObject.transform.Find("Player_img").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Img_Anim()
    {
        // img move from left to middle

        // when it move from left to middle, its alpha value change 0 to 1
        
    }
}
