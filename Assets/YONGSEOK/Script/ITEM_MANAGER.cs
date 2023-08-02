using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Charactor
{
    신준 = 1,
    지원 = 2,
    정현 = 3,
    도운 = 4,
    용석 = 5,
}

public class ITEM_MANAGER : MonoBehaviour
{
    public static ITEM_MANAGER instance;
    public static List<GameObject> self_item_list = new List<GameObject>(); // 본인 아이템 리스트
    public Charactor current_character;

	//각자 아이템 등록해주세요.
	public  List<GameObject> SJ_item_list = new List<GameObject>(); // 신준씨 아이템 리스트
    public  List<GameObject> JW_item_list = new List<GameObject>(); // 지원씨 아이템 리스트
    public  List<GameObject> JH_item_list = new List<GameObject>(); // 정현씨 아이템 리스트
    public  List<GameObject> DW_item_list = new List<GameObject>(); // 도운씨 아이템 리스트
    public  List<GameObject> YS_item_list = new List<GameObject>(); // 용석씨 아이템 리스트

    public Dictionary<Charactor, List<GameObject>> dict2 = new Dictionary<Charactor, List<GameObject>>();

    private void Start()
    {
        dict2.Add(Charactor.신준, SJ_item_list);
        dict2.Add(Charactor.지원, JW_item_list);
        dict2.Add(Charactor.정현, JH_item_list);
        dict2.Add(Charactor.도운, DW_item_list);
        dict2.Add(Charactor.용석, YS_item_list);
    }

    public void ItemSetting(Charactor a)
    {
        self_item_list = dict2[a].ToList<GameObject>();
        current_character = a;
	}


    

    /// <summary>
    /// 해당 플레이어 아이템중 랜덤한 아이템을 불러옵니다.
    /// 아이템이 없을시 null 반환
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
