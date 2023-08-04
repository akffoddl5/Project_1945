using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class June_PlayerGuidBullet : MonoBehaviour
{
    public Transform startPoint;
    public Transform endPoint;
    public Transform controlPoint1;
    public Transform controlPoint2;
    public float duration = 2.0f; // 이동에 걸리는 시간

    private float timeElapsed = 0f;
    private Vector3 initialPosition;

    public void Start()
    {
        initialPosition = transform.position;
        startPoint = transform;

    }

    private void Update()
    {
        endPoint = GameObject.FindGameObjectWithTag("ENEMY").transform;

        if (GameObject.FindGameObjectWithTag("ENEMY") != null)
        {

            if (timeElapsed < duration)
            {
                // 경과 시간에 따른 t 값을 계산
                float t = timeElapsed / duration;

                // 4차 베지어 곡선 함수를 사용하여 새로운 위치를 계산
                Vector3 newPos = CalculateBezierPoint(startPoint.position, startPoint.position + new Vector3(0, -0.5f, 0), startPoint.position + new Vector3(0, 1, 0), endPoint.position, t);

                // 오브젝트를 새로운 위치로 이동
                transform.position = newPos;

                // 경과 시간을 증가
                timeElapsed += Time.deltaTime;
            }
        }
        else
        {
            transform.Translate(Vector3.up * 3 * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("ENEMY"))
        {

            Destroy(gameObject);

        }
    }

    public void ResetPosition()
    {
        timeElapsed = 0f;
        transform.position = initialPosition;
    }
    public Vector3 CalculateBezierPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 point = uuu * p0; // (1 - t)^3 * p0
        point += 3 * uu * t * p1; // 3 * (1 - t)^2 * t * p1
        point += 3 * u * tt * p2; // 3 * (1 - t) * t^2 * p2
        point += ttt * p3; // t^3 * p3

        return point;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}





