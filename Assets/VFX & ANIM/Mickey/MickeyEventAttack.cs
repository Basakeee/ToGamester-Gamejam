using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MickeyEventAttack : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAttack()
    {
        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
        collider2D.gameObject.layer = 9;
    }

    public void endAttack()
    {
        BoxCollider2D collider2D = GetComponent<BoxCollider2D>();
        collider2D.gameObject.layer = 8;
    }
}

