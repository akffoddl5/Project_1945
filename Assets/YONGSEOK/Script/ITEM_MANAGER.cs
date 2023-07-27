using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ITEM_MANAGER : MonoBehaviour
{
    public static ITEM_MANAGER instance;
    public static Dictionary<string,GameObject> item_dic = new Dictionary<string,GameObject>(); // ITEM MANGER 가 가지고 있는걸로

    private void Awake()
    {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } 
        else
        {
            Destroy(gameObject);
            
        }
    }

    

    
}
