using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public string nextScene;
    public AudioSource audioSource;

    public void ClickStart()
    {
        StartCoroutine(Delay_load());

    }

    IEnumerator Delay_load()
    {
		audioSource.Play();
		yield return new WaitForSeconds(0.4f);
		SceneManager.LoadScene(nextScene);
        yield break;
	}
    public void ClickEnd()
    {
		
		Application.Quit();
    }
}
