using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Bullet_Type
{
    NORMAL = 1,
    GUIDE = 2,
}

public class KYS_ObjectPool : MonoBehaviour
{
    public static KYS_ObjectPool instance;

    public GameObject guideBullet;
    public GameObject normalBullet;

    public int guideBullet_num = 5;
    public int normalBullet_num = 5;

    Dictionary<Bullet_Type, Queue<GameObject>> dic;

    public Queue<GameObject> guideBullets = new();
    public Queue<GameObject> normalBullets = new();

	private void Awake()
	{
        if (instance != null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

	void Start()
    {
        for (int i = 0; i < guideBullet_num; i++)
        {
            guideBullets.Enqueue(Instantiate(guideBullet, transform));
        }

        for (int i = 0; i < guideBullet_num; i++)
        {
            guideBullets.Enqueue(Instantiate(guideBullet, transform));
        }

        dic.Add(Bullet_Type.NORMAL, normalBullets);
        dic.Add(Bullet_Type.GUIDE, guideBullets);

        Initialize();
    }

    void Initialize()
    {
        foreach(var a in dic.Values){
            foreach (var b in a)
            {
                b.SetActive(false);
            }
        }
    }

    public GameObject Instantiate(Bullet_Type type, Vector3 p, Quaternion q)
    {
        if (dic[type].Count <=0)
        {
            GameObject target_obj;
            if (type == Bullet_Type.NORMAL)
            {
                target_obj = normalBullet;
            }
            else
            {
                target_obj = guideBullet;
            }

            dic[type].Enqueue(Instantiate(target_obj));
        }

        var a = dic[type].Dequeue();
        a.SetActive(true);
        a.transform.position = p;
        a.transform.rotation = q;

        return a;
    }


}
