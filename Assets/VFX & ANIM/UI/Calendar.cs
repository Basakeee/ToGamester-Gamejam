using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calendar : MonoBehaviour
{
    public GameObject calenDar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open()
    {
        calenDar.SetActive(true);
    }

    public void Close()
    {
        calenDar?.SetActive(false);
    }
}
