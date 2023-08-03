using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class BombObject : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 Direction;
    public GameObject bombSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction * 10.5f * Time.deltaTime);
    }



    public IEnumerator InstantiateBoom()
    {
        yield return new WaitForSeconds(0.25f);

        var bom1 = Instantiate(bombSprite, transform.position, Quaternion.identity);
        var col = Physics2D.OverlapCircleAll(transform.position, 5f);

        for(int i=0; i< col.Length;++i)
        {
            if (!col[i].gameObject.CompareTag("Player") && !col[i].gameObject.CompareTag("Wall") && !col[i].gameObject.CompareTag("Obstacle") && !col[i].gameObject.CompareTag("ENEMY")
                && !col[i].gameObject.CompareTag("Player_bullet"))
            {
                Destroy(col[i].gameObject);
                continue;
            }
    
        }
        Destroy(bom1, 0.4f);

        yield return new WaitForSeconds(0.18f);

        var bom2 = Instantiate(bombSprite, transform.position, Quaternion.identity);
        Destroy(bom2, 0.55f);
        yield return new WaitForSeconds(0.18f);

        var bom3 = Instantiate(bombSprite, transform.position, Quaternion.identity);
        Destroy(bom3, 0.55f);


        Destroy(gameObject, 0.01f);

    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
