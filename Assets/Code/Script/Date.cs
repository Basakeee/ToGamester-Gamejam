using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Date : MonoBehaviour
{
    private int Month;
    private int Year;

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
