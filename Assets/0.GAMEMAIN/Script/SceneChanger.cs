using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public string nextScene;

    public void ClickStart()
    {
        SceneManager.LoadScene(nextScene);
    }
    public void ClickEnd()
    {
        Application.Quit();
    }
}
