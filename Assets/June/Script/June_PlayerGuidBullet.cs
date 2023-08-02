using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class June_PlayerGuidBullet : MonoBehaviour
{
    public Transform target; // 추적할 타겟 (적 오브젝트)
    public float speed = 5f; // 유도탄의 속도
    public float rotateSpeed = 200f; // 회전 속도

    private float t = 0f; // 베지어 곡선 상의 위치를 나타내는 변수
    private Vector2 p0, p1, p2; // 베지어 곡선의 컨트롤 포인트들

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("ENEMY").transform;
        if (target == null)
        {
            Debug.LogError("Target is not assigned!");
            return;
        }

        // 시작 위치, 중간 위치, 목표 위치를 정의
        p0 = transform.position;
        p1 = (transform.position + target.position) * 0.5f + Vector3.left * 2f; // 중간 위치는 시작 위치와 목표 위치의 중간 + 약간 위로 이동
        p2 = target.position;

        // 유도탄을 발사한 후 목표를 추적하기 위해 InvokeRepeating 사용
        InvokeRepeating("TrackTarget", 0f, 0.05f);
    }

    private void TrackTarget()
    {
        BezierCurve bezierCurve = new BezierCurve();
        // 베지어 곡선을 따라 움직일 위치 계산
        Vector2 position = bezierCurve.GetPoint(p0, p1, p2, t);

        // 유도탄의 방향 계산 (목표 방향을 향해 회전)
        Vector2 direction = (Vector2)target.position - position;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        GetComponent<Rigidbody2D>().angularVelocity = -rotateAmount * rotateSpeed;

        // 유도탄의 위치를 갱신
        transform.position = position;

        // t 값을 증가시켜 유도탄을 곡선을 따라 움직이게 함
        t += Time.deltaTime * speed;

        // t 값이 1보다 크면 유도탄을 파괴
        if (t > 1.0f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == GameObject.FindGameObjectWithTag("ENEMY"))
        Destroy(this.gameObject);
    }
    public  class BezierCurve
    {
        // t 값을 받아 베지어 곡선 상의 점을 계산하는 함수
        public  Vector2 GetPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1.0f - t;
            return oneMinusT * oneMinusT * p0 + 2.0f * oneMinusT * t * p1 + t * t * p2;
        }
    }


}





