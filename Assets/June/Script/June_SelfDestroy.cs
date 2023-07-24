using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class June_SelfDestroy : MonoBehaviour
{
    public float destroyTime = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SelfDie");

    }

    // Update is called once per frame
    IEnumerator SelfDie()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
