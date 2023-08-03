using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class KYS_Enemy_Razor : MonoBehaviour
{
	GameObject target = null;
	Transform my_coord;
	float speed = 0.4f;
	public float current_t = 0;
	List<Vector3> enemy_list = new List<Vector3>();

	private void Start()
	{
		float rand_X = Random.Range(-2.8f, 2.8f);
		float rand_Y = Random.Range(-5f, 5f);

		target = GameObject.FindGameObjectWithTag("Player");

		if (target != null)
		{
			enemy_list.Add(transform.position);
			enemy_list.Add(new Vector3(rand_X, rand_Y, 0));
			enemy_list.Add(target.transform.position);
		}

	}

	static public Quaternion GetRotFromVectors(Vector2 posStart, Vector2 posEnd)
	{
		return Quaternion.Euler(0, 0, -Mathf.Atan2(posEnd.x - posStart.x, posEnd.y - posStart.y) * Mathf.Rad2Deg);
	}
	Vector3 tmp_dir;
	private void Update()
	{
		current_t += speed * Time.deltaTime;

		//Debug.Log(target + " " + target.name + " " + enemy_list.Count);

		if (target != null && current_t <= 1)
		{
			List<Vector3> next_vector = Belzier_recursive(enemy_list, current_t);
			if (next_vector.Count > 0) {
				//tmp_dir = next_vector[0] - transform.position;
				transform.rotation = GetRotFromVectors(transform.position, next_vector[0]);

				transform.position = next_vector[0];

			}
			else
			{
				// Debug.Log("tag 바꿈");

				Destroy(gameObject, 0.1f);
				//Destroy(gameObject);
			}
			//Debug.Log(next_vector[0]);
			//float angle = Mathf.Atan2(next_vector[0].y, next_vector[0].x) * Mathf.Rad2Deg;

			//transform.rotation = Quaternion.AngleAxis(angle - 90 + 90 + 90, Vector3.forward);
		}
		else
		{
			//Research();
			//if(target == null) Destroy(gameObject);
			Destroy(gameObject);
		}
	}


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
			// next_list.Add(Vector3.Lerp(v1, v2, t  * t));
			//next_list.add(Vector3.Lerp(v1, v2, t));
		}

		//Debug.Log(next_list.Count);
		if (next_list.Count <= 1)
		{
			return next_list;
		}

		//Debug.Log("count : " + next_list.Count);

		return Belzier_recursive(next_list, t);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			//collision.gameObject.SetActive(false);
			Destroy(collision.gameObject);
		}
	}


}
