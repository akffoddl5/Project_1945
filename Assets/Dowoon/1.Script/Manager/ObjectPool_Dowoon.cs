using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BulletType
{
    Player,
    Normal,
    Normal_Yellow,
    Normal_Blue,
    Normal_Green,
    Normal_Orange,
}
public class ObjectPool_Dowoon : MonoBehaviour
{
    public static ObjectPool_Dowoon Instance;
    // Start is called before the first frame update

    [SerializeField]
    GameObject poolObject_enemyBullet;
    [SerializeField]
    GameObject poolObject_playerBullet;
    private Queue<Bullet_Dowoon> poolObjectQueue = new Queue<Bullet_Dowoon>();


    private void Awake()
    {

        if(Instance == null)
        Instance = this;
    }


    private Bullet_Dowoon CreateNewBullet(BulletType _bType)
    {
        Bullet_Dowoon bullet = null;
        switch(_bType)
        {
            case BulletType.Player:
                bullet = Instantiate(poolObject_playerBullet, transform).GetComponent<Bullet_Dowoon>();
                break;
               
            case BulletType.Normal:
            case BulletType.Normal_Yellow:
            case BulletType.Normal_Blue:
            case BulletType.Normal_Orange:
            case BulletType.Normal_Green:
               bullet = Instantiate(poolObject_enemyBullet, transform).GetComponent<Bullet_Dowoon>();
                break;

        }

        bullet.gameObject.SetActive(false);
        return bullet;
    }

    private void Init(int count, BulletType _btype)
    {
        for (int i=0; i< count; ++i)
        {
            poolObjectQueue.Enqueue(CreateNewBullet(_btype));
        }
    }

    public static Bullet_Dowoon GetBullet(BulletType _btype)
    {
        if(Instance.poolObjectQueue.Count >0)
        {
            var obj = Instance.poolObjectQueue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;


        }
        else
        {
            var obj = Instance.CreateNewBullet(_btype);
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
    }

    public static void ReturnBullet(Bullet_Dowoon bullet)
    {
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(Instance.transform);
        Instance.poolObjectQueue.Enqueue(bullet);
    }

    


}
