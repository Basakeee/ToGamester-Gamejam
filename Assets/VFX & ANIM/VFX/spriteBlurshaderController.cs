using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteBlurshaderController : MonoBehaviour
{
    [SerializeField] private Material material;

    private float blurAmount;
    private bool blurActive;

    private void Start()
    {
        blurAmount = 0;
    }

    private void Update()
    {
        if (Date.Calc() < 202401)
        {
            blurActive = true;
        }

        float blurSpeed = 10f;
        if (blurActive)
        {
            blurAmount += blurSpeed * Time.deltaTime;
        }
        else
        {
            blurAmount -= blurSpeed * Time.deltaTime;
        }

        blurAmount = Mathf.Clamp(blurAmount, 0, 0.1f);
        material.SetFloat("_BlurAmount", 0.1f);
    }
}
