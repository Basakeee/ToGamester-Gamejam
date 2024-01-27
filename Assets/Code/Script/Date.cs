using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Date : MonoBehaviour
{
    public TMP_Text monthText;
    public TMP_Text yearText;
    public static int Month;
    public static int Year;
    private void Start()
    {
        Year = 2020;
    }

    void Update()
    {
        monthText.text = Month.ToString();
        yearText.text = Year.ToString();
        if(Month % 13 == 0)
        {
            Month %= 13;
        Month++;
        }
    }

    public void AddMonth()
    {
        Month++;
    }
    public void RemoveMonth()
    {
        if(Month > 0)
        Month--;
    }
    public void AddYear()
    {
        Year++;
    }
    public void RemoveYear()
    {
        if(Year > 0) 
        Year--;
    }
    public static int Calc()
    {
        return (Year*100) + Month;
    }
}
