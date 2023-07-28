using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Mouse_Dowoon : MonoBehaviour
{

    public enum BossState
    {
        Idle = 0,
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

    float patternDelay;
    Coroutine CurrPattern_Coroutine; 
    // Start is called before the first frame update
    void Start()
    {
        ChoosePattern();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Pattern_1()
    {
        // ����Ʈ 1�� �̵�
        var dist = 100f;
        while(dist >= 0.2f)
        {
            Vector2.Lerp(Pattern1_Start, transform.position, 0.5f);
            dist = Vector3.Distance(Pattern1_Start, transform.position);

        }

        yield return new WaitForSeconds(0);
    }

    IEnumerator Pattern_2()
    {

        yield return new WaitForSeconds(0);
    }

    IEnumerator Pattern_3()
    {

        yield return new WaitForSeconds(0);
    }

    void ChangePattern()
    {
        IEnumerator IE_coroutine = null;
        switch(currPattern)
        {
            case BossState.Idle:
                if(patternDelay <=0)
                {
                    ChoosePattern();
                }
                break;
            case BossState.Pattern1:
                IE_coroutine = Pattern_1();
                break;
                case BossState.Pattern2:
                IE_coroutine = Pattern_2();              
                break;
                case BossState.Pattern3:
                 IE_coroutine = Pattern_3();
                break;
            default:
                break;

        }

        if(IE_coroutine != null)
        {
            CurrPattern_Coroutine = StartCoroutine(IE_coroutine);
        }
    }

    private  void ChoosePattern()
    {
        int randomNum = Random.Range(0, (int)BossState.Pattern3);

         switch(randomNum)
         {
            case 0:
                currPattern = BossState.Pattern1;
                break;
            case 1:
                currPattern = BossState.Pattern2;
                break;
            case 2:
                currPattern = BossState.Pattern3;
                break;
         }

        ChangePattern();
    }
}

 