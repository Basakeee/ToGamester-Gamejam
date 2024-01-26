using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MickeyEventAttack1 : MonoBehaviour
{
    public VisualEffect _Gun;
    public Transform _Pos;
    public ParticleSystem _Bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAttackGun()
    {
        VisualEffect newGun = Instantiate(_Gun, _Pos.position, _Pos.rotation);
        _Bullet.Play();
        newGun.Play();
        Destroy(newGun.gameObject, 0.5f);
    }

    public void endAttackGun()
    {     

    }
    
    
}

