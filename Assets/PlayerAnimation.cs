using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Movement moveMent => GetComponent <Movement>();
    public Animator animator => GetComponent <Animator>();
    private Combat comBat => GetComponent <Combat>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (comBat.currentWeapon == Combat.WeaponType.Hoe)
        {
            Debug.Log(moveMent.rb.velocity.magnitude);
            animator.SetBool("Jump", !moveMent.isGrounded);
            animator.SetFloat("Walk", Mathf.Abs(moveMent.xAxis));
            if (comBat.isATK)
                animator.SetTrigger("Attack");
            if (comBat.curHP <= 0)
            {
                animator.SetTrigger("Die");
            }
        }

        if (comBat.currentWeapon == Combat.WeaponType.HolyWeapons)
        {
            
            animator.SetBool("HOLYJUMP", !moveMent.isGrounded);
            animator.SetFloat("HOLYWALK", Mathf.Abs(moveMent.xAxis));
            if (comBat.isATK)
                animator.SetTrigger("HOLYATTACK");
            if (comBat.curHP <= 0)
            {
                animator.SetTrigger("Die");
            }
        }
    }
}
