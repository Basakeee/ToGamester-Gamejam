using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class calendar : MonoBehaviour
{
    public GameObject calenDar;
    public bool isOn = true;
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
        calenDar.SetActive(isOn);
        isOn = !isOn;
    }

    
}
