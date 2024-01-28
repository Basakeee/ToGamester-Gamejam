using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteBlurshaderController : MonoBehaviour
{
    [SerializeField] private Material material;
    public SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        material = sr.GetComponent<Material>();
    }

    private void Update()
    {
        if (Date.Calc() < 202401)
        material.SetFloat("_BlurAmount", 0.1f);
    }
}
