using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int HP;
    public ParticleSystem ParticleSystem;
    public float KnockbackForce;
    public bool isKnockback;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();

    public virtual void TakeDMG(int dmg,Combat.WeaponType type,Transform player)
    {
        TakeWeaponDMG(dmg, type);
        int Direction = transform.position.x > player.position.x ? 1 : -1;
        rb.AddForce(transform.right * Direction * KnockbackForce,ForceMode2D.Impulse);
        StartCoroutine(knockBack());
        if (HP <= 0)
        {
            Destroy(gameObject);
            Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        }
    }

    public virtual void TakeWeaponDMG(int dmg, Combat.WeaponType type)
    {
        if (type == Combat.WeaponType.HolyWeapons)
        {   // Attack Boss
            if (gameObject.CompareTag("Boss"))
                HP -= 1;
            else // Attack Enemy
                HP -= dmg * 2;
        }

        if (type == Combat.WeaponType.Hoe)
        {   // Attack Boss
            if (gameObject.CompareTag("Boss"))
                HP -= dmg * 200;
            else // Attack Enemy
                HP -= dmg;
        }
    }
    IEnumerator knockBack()
    {
        isKnockback = true;
        Debug.Log(isKnockback);
        yield return new WaitForSeconds(0.3f);
        isKnockback = false;
        Debug.Log(isKnockback); 
    }
}
