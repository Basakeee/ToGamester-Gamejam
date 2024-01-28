using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FakeEnding : MonoBehaviour
{
    void Update()
    {
        if (Date.Calc() < 201003)
        {
            SceneManager.LoadScene("FakeEndGame");
        }
    }
}
