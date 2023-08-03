using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class June_SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
           
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.A))
        {
            AudioSource Charge = transform.GetChild(0).GetComponent<AudioSource>();
            Charge.Play();

        }
    }
}
