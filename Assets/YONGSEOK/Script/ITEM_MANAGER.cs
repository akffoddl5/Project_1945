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
    public Charactor current_character;

	//���� ������ ������ּ���.
	public  List<GameObject> SJ_item_list = new List<GameObject>(); // ���ؾ� ������ ����Ʈ
    public  List<GameObject> JW_item_list = new List<GameObject>(); // ������ ������ ����Ʈ
    public  List<GameObject> JH_item_list = new List<GameObject>(); // ������ ������ ����Ʈ
    public  List<GameObject> DW_item_list = new List<GameObject>(); // ��� ������ ����Ʈ
    public  List<GameObject> YS_item_list = new List<GameObject>(); // �뼮�� ������ ����Ʈ

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
        current_character = a;
	}


    

    /// <summary>
    /// �ش� �÷��̾� �������� ������ �������� �ҷ��ɴϴ�.
    /// �������� ������ null ��ȯ
    /// </summary>
    /// <returns></returns>
    public GameObject GetItem(Vector3 position, Quaternion q)
    {
        int count = self_item_list.Count;
        int ran_idx = 0;
        if (count == 0) {
            return null;
        } 
        else {
            ran_idx = Random.Range(0, count - 1);
            return Instantiate(self_item_list[ran_idx], position, q);
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
