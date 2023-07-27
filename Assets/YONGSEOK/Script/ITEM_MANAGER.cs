using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Charactor
{
        ���� = 1,
        ���� = 2,
        ���� = 3,
        ���� = 4,
        �뼮 = 5,
}

public class ITEM_MANAGER : MonoBehaviour
{
    public static ITEM_MANAGER instance;
    public static List<GameObject> self_item_list = new List<GameObject>(); // ���� ������ ����Ʈ

    //���� ������ ������ּ���.
    public static List<GameObject> SJ_item_list = new List<GameObject>(); // ���ؾ� ������ ����Ʈ
    public static List<GameObject> JW_item_list = new List<GameObject>(); // ������ ������ ����Ʈ
    public static List<GameObject> JH_item_list = new List<GameObject>(); // ������ ������ ����Ʈ
    public static List<GameObject> DW_item_list = new List<GameObject>(); // ��� ������ ����Ʈ
    public static List<GameObject> YS_item_list = new List<GameObject>(); // �뼮�� ������ ����Ʈ

    public Dictionary<Charactor, List<GameObject>> dict2 = new Dictionary<Charactor, List<GameObject>>();

    private void Start()
    {
        dict2.Add(Charactor.����, SJ_item_list);
        dict2.Add(Charactor.����, JW_item_list);
        dict2.Add(Charactor.����, JH_item_list);
        dict2.Add(Charactor.����, DW_item_list);
        dict2.Add(Charactor.�뼮, YS_item_list);
    }

    public void ItemSetting(Charactor a)
    {
        self_item_list = dict2[a].ToList<GameObject>();

    }

    /// <summary>
    /// �ش� �÷��̾� �������� ������ �������� �ҷ��ɴϴ�.
    /// �������� ������ �� ������Ʈ�� ��ȯ
    /// </summary>
    /// <returns></returns>
    public GameObject GetItem()
    {
        int count = self_item_list.Count;
        int ran_idx = 0;
        if (count == 0) {
            GameObject a = new GameObject("empty_item");
            
            //Destroy(a, 10f);
            return a;
        } 

        else {
            ran_idx = Random.Range(0, count - 1);
            return self_item_list[ran_idx];
        }
    }
    

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
