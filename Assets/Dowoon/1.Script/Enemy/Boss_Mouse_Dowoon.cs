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
    
    // ���� 3��

    // �� ������ ���� ���� ��ǥ�� �޾Ƽ� ����ǥ�� �̵� �� ���� ����.
    // ������ �����ð����� �������� �ϵ�, ���������� �����ʴ´�.

    // ���� 1:  ������ ���� �����ʾƷ��� �׸� �׷��� �� �׸� ��ȭ���� ������ 

    // ���� 2 : �������� �밢���� ���ʾƷ� �밢���� �̵��ϸ� �����ϰ� �翷���� ź������ .


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
