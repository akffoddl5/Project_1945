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
  
    public Direction _dir
        = Direction.Left;

    Vector2 offset = new Vector3(2, 1.5f);
    [SerializeField]
    public GameObject spawnPos;
    [SerializeField]
    public GameObject monsterPrefab;

    public Vector3 rotatePos = Vector3.zero;


    List<GameObject> enemyArray = new List<GameObject>();
    bool b_isEnemyActive = false;
    int maxCol = 4;

    float circleR = 2;
    float deg;
    float objSpeed = 35;

   // IEnumerator IE_SetEnemyGoalPos = SetEnemyGoalPos();
    // Start is called before the first frame update
    void Start()
    {
       // SetPanel(9);
    }

    // Update is called once per frame
    void Update()
    {
        switch(_dir)
        {
            case Direction.Left:

                break;
            case Direction.Right:
                transform.Translate(new Vector2(1, 0) * 2.5f * Time.deltaTime);
                break;
        }

        if(transform.position.x >= 0f && transform.position.x <= 1.5f && !b_isEnemyActive)
        {    
                b_isEnemyActive = true;
                rotatePos = transform.position;
                rotatePos.x += 0.55f;

            StartCoroutine(SetEnemyIsAttack());

        }

        if(b_isEnemyActive)
        {
            MoveEnemy();
        }
    }

    IEnumerator SetEnemyIsAttack()
    {
        int count = 0;
        while (count < enemyArray.Count)
        {
            enemyArray[count].GetComponent<Enemy_Dowoon>().isAttackAble = true;
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
            for (int i = 0; i < enemyArray.Count; i++)
            {
                if (enemyArray[i].GetComponent<Enemy_Dowoon>().isArrive)
                {
                    var rad = Mathf.Deg2Rad * (deg + (i * (360 / enemyArray.Count)));
                    var x = circleR * Mathf.Sin(rad);
                    var y = circleR * Mathf.Cos(rad);
                    enemyArray[i].transform.position = rotatePos + new Vector3(x, y);
                }
                else
                {
                    var rad = Mathf.Deg2Rad * (deg + (i * (360 / enemyArray.Count)));
                    var x = circleR * Mathf.Sin(rad);
                    var y = circleR * Mathf.Cos(rad);
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
            enemyArray.Add(monster);

            var offsetPos = spawnPos.transform.position;
            offsetPos.x += offset.x * offsetCount;
            offsetPos.y += yOffset;

            monster.transform.position = offsetPos;
            offsetCount++;
        }

    }

   void LaunchEnemy()
    {
        for(int i=0; i<enemyArray.Count; ++i)
        {
            var monster = enemyArray[i].GetComponent<Enemy_Dowoon>();
        }

    }
}
