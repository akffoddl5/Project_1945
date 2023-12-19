using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBox : MonoBehaviour
{
    public GameObject targetObject;
   // public bool b_isDeQueue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (targetObject != null)
        {
            transform.localPosition = Vector3.zero;
           
        }
    }

    public bool isMyTarget(Collider2D[] col)
    {

        for(int i=0; i < col.Length;++i)
        {
            if (col[i].gameObject == targetObject)
                return true;
           
                
        }

        return false;
    }
}
