using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour
{
    public GameObject background;
	public GameObject btn_restart;
	public GameObject btn_close;

	Vector2 vec_moveBg = Vector2.down;
	float moveBgSpeed = 3f;

	private void Start()
	{
		// ó���� ��ư �� ���̰� SetActive(false)
		btn_restart.SetActive(false);
		btn_close.SetActive(false);
	}
	private void Update()
	{
		if (background.transform.position.y > -75f)
		{
			background.transform.Translate(vec_moveBg * moveBgSpeed *Time.deltaTime);
		}
		// �� �ö����� else ����
		else
		{
			btn_restart.SetActive(true);
			btn_close.SetActive(true);
		}

	}
	public void RestertBtn()
	{
		// ĳ���� ���� �� �ε�ǵ��� �����
		//SceneManager.LoadScene("ĳ���� ���� ��");
	}
	public void CloseBtn()
	{
		Application.Quit();
	}
}
