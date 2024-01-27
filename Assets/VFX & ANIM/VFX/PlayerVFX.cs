using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerVFX : MonoBehaviour
{
    public VisualEffect[] HorrorEffect; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        

    }

    public void CreateHorror()
    {
        VisualEffect newHorrorEffect = Instantiate(HorrorEffect[Random.Range(0, HorrorEffect.Length)], transform.position, transform.rotation);
        newHorrorEffect.Play();
        Destroy(newHorrorEffect.gameObject, 0.5f);
    }
}
