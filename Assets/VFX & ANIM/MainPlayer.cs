using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MainPlayer : MonoBehaviour
{
    public VisualEffect _Slash;
    public VisualEffect _SlashSword;
    public VisualEffect _Died;
    public Transform _SwordPos;
    public ParticleSystem _Damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hoeSlashStart()
    {
        VisualEffect newSlash = Instantiate(_Slash, _SwordPos.position, _SwordPos.rotation);
        _Damage.Play();
        newSlash.Play();
        Destroy(newSlash, 1.5f);
    }

    public void hoeSlashEnd()
    {
       
    }

    public void SwordSlashStart()
    {
        VisualEffect newSwordSlash = Instantiate(_SlashSword, _SwordPos.position, _SwordPos.rotation);
        _Damage.Play();
        newSwordSlash.Play();
        Destroy(newSwordSlash, 1.5f);
    }

    public void SwordSlashEnd()
    {

    }

    public void StartDied()
    {
        VisualEffect newDied = Instantiate(_Died, transform.position,transform.rotation);
        newDied.Play();
        Destroy(newDied, 1.5f);
    }

    public void EndDied()
    {

    }
  
}
