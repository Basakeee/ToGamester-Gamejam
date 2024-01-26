using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class JumpEffect : MonoBehaviour
{
    public VisualEffect _jump;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startJump()
    {

    }

    public void endJump()
    {
        VisualEffect newJump = Instantiate(_jump, transform.position, transform.rotation);
        newJump.Play();
        Destroy(newJump, 1.5f);
    }
}
