using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ENDING : MonoBehaviour
{
    public void Load_Character_Scene()
    {
        UI_Manager.instance.current_stage = 1;
        AsyncOperation oper = SceneManager.LoadSceneAsync(0);
    }

    public void Application_Exit()
    {
        Application.Quit();
    }

}
