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

        CreateWall(Wall, new Vector3(-13f, 1.5f, 0), 4, Direction.Right);

        yield return new WaitForSeconds(5);

        CreateWall(Wall, new Vector3(11, 1.5f, 0), 8, Direction.Left);



        yield return new WaitForSeconds(5);

        CreateAndSetGoal(WindowIcon, new Vector3(10, 6.5f, 0), new Vector3(7, 6.5f, 0));
       

        yield return new WaitForSeconds(1);
        CreateAndSetGoal(WindowIcon, new Vector3(-3, 5.5f, 0), new Vector3(1, 5.5f, 0));
        yield return new WaitForSeconds(2);

        CreateWall(Wall, new Vector3(11f, 4.5f, 0), 12, Direction.Left);


        yield return new WaitForSeconds(5);
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
