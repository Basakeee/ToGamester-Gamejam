using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PoohCharge : MonoBehaviour
{
    public VisualEffect _Charge;
    public VisualEffect _Dash;
    public VisualEffect _Died;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startDash()
    {
        VisualEffect newDash = Instantiate(_Dash, transform.position, transform.rotation);
        newDash.Play();
        Destroy(newDash, 2.5f);
        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
        collider2D.gameObject.layer = 9;
    }

    public void endDash()
    {
        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
        collider2D.gameObject.layer = 8;
    }

    public void startCharge()
    {
        VisualEffect newCharge = Instantiate(_Charge, transform.position, transform.rotation);
        newCharge.Play();
        Destroy(newCharge, 2.5f);
    }

    public void endCharge()
    {
        
    }

    public void startDied()
    {

    }

    public void endDied()
    {
        VisualEffect newDied = Instantiate(_Died, transform.position,transform.rotation);
        newDied.Play();
        Destroy(newDied, 1.5f);
    }
}
