using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EventManager_Dowoon : MonoBehaviour
{
    public GameObject Wall;
    public GameObject WindowIcon;
    public GameObject Boss_mouse;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEvent());
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }


    IEnumerator SpawnEvent()
    {
        yield return new WaitForSeconds(1);
        //첫 웨이브
        CreateWall(Wall, new Vector3(-10f, 1.5f, 0), 4, Direction.Right);

        yield return new WaitForSeconds(5);
        // 둘째 웨이브 폴더 8
        CreateWall(Wall, new Vector3(8, 1.5f, 0), 8, Direction.Left);



        yield return new WaitForSeconds(10);
        // 셋째 웨이브 윈도우 아이콘
        CreateAndSetGoal(WindowIcon, new Vector3(8, 3.5f, 0), new Vector3(2f, 3.5f, 0));
       

        yield return new WaitForSeconds(4);
        CreateAndSetGoal(WindowIcon, new Vector3(-4, 2.5f, 0), new Vector3(-1, 2.5f, 0));
        yield return new WaitForSeconds(2);

        CreateWall(Wall, new Vector3(-8f, 4.5f, 0), 12, Direction.Right);


        yield return new WaitForSeconds(7);


        CreateWall(Wall, new Vector3(10f, 3.5f, 0), 6, Direction.Left);
        CreateAndSetGoal(WindowIcon, new Vector3(-3, 2.5f, 0), new Vector3(0, 2.5f, 0));


        yield return new WaitForSeconds(12f);
        CreateAndSetGoal(Boss_mouse, Boss_mouse.GetComponent<Boss_Mouse_Dowoon>().SpawnPos, Boss_mouse.GetComponent<Boss_Mouse_Dowoon>().Spawn_arrivePos);



        StopCoroutine(SpawnEvent());
    }


    void CreateAndSetGoal(GameObject go, Vector3 startPos, Vector3 goalPos)
    {
        var obj = Instantiate(go, startPos, Quaternion.identity);

        if(obj.GetComponent<Enemy_Dowoon>() != null)
        obj.GetComponent<Enemy_Dowoon>().SetGoalPos(goalPos);
    }
    void CreateWall(GameObject go, Vector3 startPos,int enemyCount, Direction dir)
    {
        var obj = Instantiate(go, startPos, Quaternion.identity);
        obj.GetComponent<EnemyPanel_Dowoon>().SetPanel(enemyCount, dir);

    }
}
