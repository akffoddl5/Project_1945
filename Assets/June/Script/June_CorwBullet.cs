using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class June_CrowBullet : MonoBehaviour
{
    public GameObject target = null;
    Vector3 dir;
    Vector3 dirNo;
    public float Speed = 4.0f;
   
    // Start is called before the first frame update
    void Start()
    {

        target = transform.GetChild(0).GetComponent<June_CorwBulletDirection>().target;
        //�÷��̾� ã��
        //�÷��̾� ã��
        if (target != null)
        {
            dir = target.transform.position - transform.position; //a-b == a�� �ٶ󺸴�  b�� ����
            dirNo = dir.normalized;//���⸸ ����ֱ�
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dirNo * Speed * Time.deltaTime);
        //.Translate(Vector3.right * Speed * Time.deltaTime);
    }


   

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

}