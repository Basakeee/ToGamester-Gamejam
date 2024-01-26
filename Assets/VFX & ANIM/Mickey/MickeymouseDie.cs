using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MickeymouseDie : MonoBehaviour
{
    public VisualEffect _Die;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startDie()
    {
    }

    public void endDie()
    {
        VisualEffect newDie = Instantiate(_Die, transform.position, transform.rotation);
        newDie.Play();
        Destroy(newDie, 1.5f);
    }
}

