using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Direction
{
    Right,
    Left,
}

public class EnemyPanel_Dowoon : MonoBehaviour
{

    public const int MAX_COUNT = 20;

    public Direction _dir
        = Direction.Left;

    Vector2 offset = new Vector3(1, 0.3f);
    [SerializeField]
    public GameObject spawnPos;
    [SerializeField]
    public GameObject monsterPrefab;

    public Vector3 rotatePos = Vector3.zero;


    GameObject[] enemyArray = new GameObject[MAX_COUNT];
    int currCount = 0;
    bool b_isEnemyActive = false;
    int maxCol = 4;

    float circleR = 2;
    float deg;
    float objSpeed = 35;

   // IEnumerator IE_SetEnemyGoalPos = SetEnemyGoalPos();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartPanel());
       // SetPanel(9);
    }

    // Update is called once per frame
    void Update()
    {
        switch(_dir)
        {
            case Direction.Left:
                transform.Translate(new Vector2(-1, 0) * 4.5f * Time.deltaTime);
                break;
            case Direction.Right:
                transform.Translate(new Vector2(1, 0) * 4.5f * Time.deltaTime);
                break;
        }

       

        if(b_isEnemyActive)
        {
            MoveEnemy();
        }
    }

    IEnumerator StartPanel()
    {
        yield return new WaitForSeconds(2.0f);
       
            b_isEnemyActive = true;
            rotatePos = transform.position;

    


            StartCoroutine(SetEnemyIsAttack());

        
    }
    IEnumerator SetEnemyIsAttack()
    {
        int count = 0;
        while (count < currCount)
        {
            enemyArray[count].GetComponent<Enemy_Dowoon>().isAttackAble = true;
            enemyArray[count].GetComponent<BoxCollider2D>().enabled = true;
            yield return new WaitForSeconds(0.15f);


            count++;
        }

        StopCoroutine(SetEnemyIsAttack());
    }
   
    public void MoveEnemy()
    {
        deg += Time.deltaTime * objSpeed;
        if (deg < 360)
        {
            for (int i = 0; i < currCount; i++)
            {
                if (enemyArray[i] && enemyArray[i].GetComponent<Enemy_Dowoon>().isArrive)
                {
                    var rad = Mathf.Deg2Rad * (deg + (i * (360 / currCount)));
                    var x = circleR * Mathf.Sin(rad);
                    var y = circleR * Mathf.Cos(rad);
                    if(enemyArray[i] != null)
                    enemyArray[i].transform.position = rotatePos + new Vector3(x, y);
                }
                else
                {
                    var rad = Mathf.Deg2Rad * (deg + (i * (360 / currCount)));
                    var x = circleR * Mathf.Sin(rad);
                    var y = circleR * Mathf.Cos(rad);
                    if (enemyArray[i] != null)
                        enemyArray[i].GetComponent<Enemy_Dowoon>().SetGoalPos(rotatePos + new Vector3(x, y));
                }

                //  enemyArray[i].transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / enemyArray.Count))) * -1);
            }

        }
        else
        {
            deg = 0;
        }
    }
    public void SetPanel(int enemyCount, Direction _di)
    {
        currCount = enemyCount;
        _dir = _di;
        float yOffset = 0;
        int offsetCount = 0;
        for (int i = 0; i < enemyCount; ++i)
        {

            if (i % maxCol == 0 && i != 0)
            {
                yOffset -= offset.y;
                offsetCount = 0;
            }
            var monster = Instantiate(monsterPrefab);
            monster.transform.parent = transform;
            monster.GetComponent<Enemy_Dowoon>().Panel = this.gameObject;
            enemyArray[i] = monster;
            

            var offsetPos = spawnPos.transform.position;
            offsetPos.x += offset.x * offsetCount;
            offsetPos.y += yOffset;

            monster.transform.position = offsetPos;
            offsetCount++;
        }

    }

   void LaunchEnemy()
    {
        for(int i=0; i<currCount; ++i)
        {
            var monster = enemyArray[i].GetComponent<Enemy_Dowoon>();
        }

    }

    int GetIndexToFind(GameObject enemy)
    {
        for (int i = 0; i < currCount; ++i)
        {
            if (enemyArray[i] == enemy)
                return i;
        }
        return 0;


    }

    public void DestroyEnemy(GameObject enemy)
    {
        int idx = GetIndexToFind(enemy);

        enemyArray[idx] = null;
        Destroy(enemy);
    }


}
