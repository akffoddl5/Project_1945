using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class June_PlayerGuidBullet : MonoBehaviour
{
    public Transform target; // ������ Ÿ�� (�� ������Ʈ)
    public float speed = 5f; // ����ź�� �ӵ�
    public float rotateSpeed = 200f; // ȸ�� �ӵ�

    private float t = 0f; // ������ � ���� ��ġ�� ��Ÿ���� ����
    private Vector2 p0, p1, p2; // ������ ��� ��Ʈ�� ����Ʈ��

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("ENEMY").transform;
        if (target == null)
        {
            Debug.LogError("Target is not assigned!");
            return;
        }

        // ���� ��ġ, �߰� ��ġ, ��ǥ ��ġ�� ����
        p0 = transform.position;
        p1 = (transform.position + target.position) * 0.5f + Vector3.left * 2f; // �߰� ��ġ�� ���� ��ġ�� ��ǥ ��ġ�� �߰� + �ణ ���� �̵�
        p2 = target.position;

        // ����ź�� �߻��� �� ��ǥ�� �����ϱ� ���� InvokeRepeating ���
        InvokeRepeating("TrackTarget", 0f, 0.05f);
    }

    private void TrackTarget()
    {
        BezierCurve bezierCurve = new BezierCurve();
        // ������ ��� ���� ������ ��ġ ���
        Vector2 position = bezierCurve.GetPoint(p0, p1, p2, t);

        // ����ź�� ���� ��� (��ǥ ������ ���� ȸ��)
        Vector2 direction = (Vector2)target.position - position;
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        GetComponent<Rigidbody2D>().angularVelocity = -rotateAmount * rotateSpeed;

        // ����ź�� ��ġ�� ����
        transform.position = position;

        // t ���� �������� ����ź�� ��� ���� �����̰� ��
        t += Time.deltaTime * speed;

        // t ���� 1���� ũ�� ����ź�� �ı�
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
        // t ���� �޾� ������ � ���� ���� ����ϴ� �Լ�
        public  Vector2 GetPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1.0f - t;
            return oneMinusT * oneMinusT * p0 + 2.0f * oneMinusT * t * p1 + t * t * p2;
        }
    }


}





