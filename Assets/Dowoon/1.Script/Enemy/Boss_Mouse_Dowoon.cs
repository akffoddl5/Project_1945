using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Mouse_Dowoon : MonoBehaviour
{

    public enum BossState
    {
        Idle,
        Pattern1,
        Pattern2,
        Pattern3,


    }

    public BossState currPattern = BossState.Idle;
    
    // 패턴 3개

    // 각 패턴은 패턴 시작 좌표를 받아서 그좌표로 이동 후 패턴 시작.
    // 패턴은 일정시간마다 랜덤으로 하되, 전의패턴은 하지않는다.

    // 패턴 1:  왼쪽위 부터 오른쪽아래로 네모를 그려서 그 네모가 방화벽이 떨어짐 

    // 패턴 2 : 오른쪽위 대각에서 왼쪽아래 대각으로 이동하며 폭발하고 양옆으로 탄막생성 .


    [SerializeField]
    Vector3 Pattern1_Start;
    [SerializeField]
    Vector3 Pattern1_End;

    [SerializeField]
    Vector3 Pattern2_Start;
    [SerializeField]
    Vector3 Pattern2_End;

    [SerializeField]
    Vector3 Pattern3_Start;


    [SerializeField]
    Vector3 SpawnPos;

    [SerializeField]
    Vector3 Spawn_arrivePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Pattern_1()
    {

        yield return new WaitForSeconds(0);
    }
}
