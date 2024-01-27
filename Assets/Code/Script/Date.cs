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
        Month++;
    }
    public void MoveYear()
    {
        Month++;
    }
    public int Calc()
    {
        return (Year*100) + Month;
    }
}
