using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class June_CorwShooting : MonoBehaviour
{

    public GameObject bullet = null; //�̻��� 
    public Transform pos = null; //�̻��� �߻�
    public float CrowDamage;

    void Start()
    {
        CrowDamage = GameObject.FindWithTag("Player").GetComponent<June_PlayerShooting>().PlayerDamage/5;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {

            var _bullet = Instantiate(bullet, pos.position, Quaternion.identity);
            _bullet.GetComponent<Bullet_info>().att = CrowDamage;



        }
    }
}
