# Project_1945
Project_1945


![image](https://github.com/akffoddl5/Project_1945/assets/44525847/4f3667a7-ac0e-48aa-af01-f74679bf1c00)


## **프로젝트 소개**

- **캐릭터를 선택하여 각자 고유능력을 이용하여 미사일을 피해 적을 물리치는 슈팅게임**
- **캐릭터마다 고유 스킬이나 능력이 있으며 죽은 시점에서 캐릭터 체인지 가능**
- **본인은 마지막 캐릭터를 맡았으며 음악 컨셉의 스테이지를 구현**
- **N차 베지에 곡선을 이용해 유도미사일 구현**
- **각자 캐릭터가 먹을 아이템이 다른 스테이지에서도 나와야 하므로 아이템 매니저 구현**
- **조장을 맡아 기획, 소스 병합, 오류 수정 및 전체적인 시스템 구현**

## 시연영상

https://youtu.be/WEQBIUWTluk

## 목차

1. **캐릭터 선택**
    
    **1-1. 캐릭터 선택**
    
    **1-2. 게임중 캐릭터 선택**
    
2. **캐릭터 소개**
    
    **2-1. 아야**
    
    **2-2. 외계인**
    
    **2-3. 제트**
    
    **2-4. 삼각**
    
    **2-5. 레드**
    
3. **스테이지 소개**
    
    **3-1. 씬 전환**
    
    **3-2. 윈도우 스테이지**
    
    **3-3. 행성 스테이지**
    
    **3-4. 음악 스테이지**
    
    **3-5. 동방프로젝트 스테이지**
    
    **3-6. 엔딩**
    

### **1. 캐릭터 선택**

1-1. 캐릭터 선택

- 게임 시작 시 캐릭터 선택 가능
- 캐릭터에 맞는 조작 키 설명도 같이 노출

1-2. 게임중 캐릭터 선택

- 게임 중 죽었을 때 캐릭터 선택 가능

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/ce98df65-5bc9-4359-b559-ab246832481b)


- 캐릭터를 선택하여 리스폰 시 잠시 무적 상태로 리스폰 자리로 이동

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/9e98ef6d-9313-4d60-87ae-bbeae0d9657a)


```csharp
public void Revive() { StartCoroutine(PlayerRevive()); }

	IEnumerator PlayerRevive()
    {
        if (now_Player_Instance != null)
        {
            Destroy(now_Player_Instance);
        }

        if (now_Player_Instance == null)
        {
            now_Player_Instance = Instantiate(now_Player, new Vector3(0, -6, 0), Quaternion.identity);
        }

        now_Player_Instance.transform.position = new Vector3(0, -6, 0);

        //다형성으로 2D콜라이더 전부 Disable
        if (now_Player_Instance.GetComponent<Collider2D>() != null)
        {
            now_Player_Instance.GetComponent<Collider2D>().enabled = false;
        }

         StartCoroutine(PlayerMove(1.0f,1.5f));
        yield return StartCoroutine(PlayerBlink());

        now_Player_Instance.GetComponent<Collider2D>().enabled = true;
	}

IEnumerator PlayerBlink()
    {
        SpriteRenderer spr = null;
        if (now_Player_Instance)
        {
            if (now_Player_Instance.GetComponent<SpriteRenderer>())
            {
                spr = now_Player_Instance.GetComponent<SpriteRenderer>();
               
            }
        }
        int blinkCount = 0;
        while (blinkCount++ <= 25)
        {
           
            yield return new WaitForSeconds(0.1f);
            var c = spr.color;
        
            c.a = spr.color.a >= 1f ? 0.3f : 1f;
           
            spr.color = c;
        }
        var c2 = spr.color;
        c2.a = 1f;
        spr.color = c2;

        yield return new WaitForSeconds(0.1f);
    }
```

### 2**. 캐릭터 소개**

**2-1. 아야**

- **필살기로 적의 모든 탄막 제거 후 컷신 등장**
    
    ![image](https://github.com/akffoddl5/Project_1945/assets/44525847/4045aa96-c2b9-4192-a43c-990aaabfc9e7)

    
- **좌 Shift로 천천히 이동하며 세밀한 컨트롤 가능**

**2-2. 외계인**

- C, X로 공격 방향 전환 가능, 우상단에 공격 방향 표시

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/99a66b93-57c4-4fb2-8376-ed91d2afb84a)


**2-3. 제트**

- 궁극기 사용 시 단검 소환

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/dc130b86-46e1-45a9-930b-40b33b7b865d)


**2-4. 삼각**

- 일정 시간마다 궁극기 사용 가능 (적 탄막 제거)

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/49c8eb3e-3dc7-4b65-a0d6-521132e8b390)


**2-5. 레드**

- 잔상과 함께 대쉬기능

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/a0ea876f-6897-4710-b0d1-681cbe26ffa2)


- 적의 수에 따라 N차 베지에 곡선 움직임으로 이동하는 자동유도탄 생성

```csharp
//N차 벨지에
public List<Vector3> Belzier_recursive(List<Vector3> _points, float t)
{
    List<Vector3> next_list = new List<Vector3>();
    for (int i = 0; i < _points.Count; i++)
    {
        if (i == _points.Count - 1) break;
        Vector3 v1 = _points[i];
        Vector3 v2 = _points[i + 1];
        next_list.Add(Vector3.Lerp(v1, v2, t));
        //next_list.add(Vector3.Lerp(v1, v2, t));
        
    }

    Debug.Log(next_list.Count);
    if (next_list.Count <= 1)
    {
        return next_list;
    }

    return Belzier_recursive(next_list, t);
}
```

### 3**. 스테이지 소개**

3**-1. 씬 전환**

- 씬 전환시 현재 배경음 서서히 줄이기

```csharp
public IEnumerator Sound_Kill()
{
	if (Camera.main.gameObject.GetComponent<AudioSource>() == null) yield break;
	
	while (true)
	{
		Debug.Log(Camera.main.gameObject.GetComponent<AudioSource>().volume);
		Camera.main.gameObject.GetComponent<AudioSource>().volume -= 0.2f;
		yield return new WaitForSeconds(1f);

        if (Camera.main)
        {
            if (Camera.main.gameObject.GetComponent<AudioSource>().volume <= 0f)
            {
                Camera.main.gameObject.GetComponent<AudioSource>().volume = 0f;
                break;
            }
        }
	}
	yield return new WaitForSeconds(0.5f);
	//audioSource_clear.Play();

}
```

- 씬 전환 성공시 이벤트 처리

```csharp
SceneManager.sceneLoaded += SceneLoadFunc;

public void SceneLoadFunc(Scene arg0, LoadSceneMode arg1)
{
	StartCoroutine(FadeIn());

    if (arg0.name != "GameEnd")
    {
        StartCoroutine(Playerspawn());
    }
}
```

3**-2. 윈도우 스테이지**

- 윈도우 아이콘들이 적으로 나와 플레이

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/5ece277d-60df-4756-9e22-13aebbe9f3c6)


**3-3. 행성 스테이지**

- 보스 하나만 있는 스테이지
- 회전하는 태양의 광선을 회피하며 공략

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/e699a536-f7a1-450f-97e6-8372ad6967f0)


**3-4. 음악 스테이지**

- 보스 푸린
    - 여러가지 탄막 패턴
    
    ![image](https://github.com/akffoddl5/Project_1945/assets/44525847/6251c33e-16b9-44fb-bf52-b181e8378b7d)

    
- 보스 퉁퉁이
    - 회피가 불가능 해보이지만 빨간색 총알은 안전한 총알로 설정하여 패턴 구현

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/c0c7cd74-5b35-4d10-883d-18caeb4d49ec)


**3-5. 동방프로젝트 스테이지**

- 일정 몹을 제거 시 컷씬과 함께 보스 등장

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/1ae00a61-e3c1-4906-90ec-b571cf85d187)


**3-6. 엔딩**

- 모든 스테이지 클리어시 모든 보스와 스테이지를 역순으로 보여주며 엔딩크레딧

![image](https://github.com/akffoddl5/Project_1945/assets/44525847/fe73598b-3100-43d8-ab62-784c75d8543e)
