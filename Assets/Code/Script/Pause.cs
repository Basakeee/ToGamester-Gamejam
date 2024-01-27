using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject optionPanel;
    public static bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            optionPanel.SetActive(true);
            isPaused = true;
        }
    }
    public void CloseButton()
    {
        Time.timeScale = 1f;
        optionPanel.SetActive(false);
        isPaused = false;
    }
}
