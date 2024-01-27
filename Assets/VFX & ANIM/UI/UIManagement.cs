using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagement : MonoBehaviour
{
    
    public void Playgame(int sceneNum)
    {
        SceneManager.LoadScene(sceneNum);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
