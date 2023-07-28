using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager_Dowoon : MonoBehaviour
{
    public GameObject Wall;
    public GameObject WindowIcon;

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


        var first = Instantiate(Wall);
        first.transform.position = new Vector3(-13.5f, 1.5f, 0);
        first.GetComponent<EnemyPanel_Dowoon>().SetPanel(4,Direction.Right);



        yield return new WaitForSeconds(5);

        var second = Instantiate(Wall);
        second.transform.position = new Vector3(11f, 4.5f, 0);
        second.GetComponent<EnemyPanel_Dowoon>().SetPanel(8, Direction.Left);


        StopCoroutine(SpawnEvent());
    }
}
