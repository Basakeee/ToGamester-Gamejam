using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Date : MonoBehaviour
{
    public TMP_Text monthText;
    public TMP_Text yearText;
    private int Month;
    private int Year;

    void Update()
    {
        monthText.text = Month.ToString();
        yearText.text = Year.ToString();
        if(Month % 12 == 0)
        {
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
    public void AddYear()
    {
        Year++;
    }
    public void RemoveYear()
    {
        Year--;
    }
    public int Calc()
    {
        return (Year*100) + Month;
    }
}
