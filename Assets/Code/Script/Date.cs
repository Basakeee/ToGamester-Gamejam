using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Date : MonoBehaviour
{
    private int Month;
    private int Year;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Month % 12 == 0)
        {
            Year++;
            Month %= 12;
        }
    }

    public void AddMonth()
    {
        Month++;
    }
    public void RemoveMonth()
    {
        Month--;
    }
}
