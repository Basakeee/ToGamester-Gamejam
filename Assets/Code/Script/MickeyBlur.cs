using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MickeyBlur : MonoBehaviour
{
    [SerializeField] private Material material;
    public SpriteRenderer sr;
 
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        material = sr.GetComponent<Material>();
        Debug.Log(sr.material);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"{Date.Month} month {Date.Year} year : {Date.Calc()}");
        if(Date.Calc() < 202401)
        material.SetFloat("_BlurAmount", 0.1f);
    }
}
