using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int HP;
    public ParticleSystem ParticleSystem;
    public float KnockbackForce;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDMG(int dmg,Combat.WeaponType type,Transform player)
    {
        TakeWeaponDMG(dmg, type);
        int Direction = transform.position.x > player.position.x ? 1 : -1;
        rb.AddForce(transform.right * Direction * KnockbackForce,ForceMode2D.Impulse);
        if (HP <= 0)
        {
            Destroy(gameObject);
            Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        }
    }

    private void TakeWeaponDMG(int dmg, Combat.WeaponType type)
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
                HP -= dmg * 2;
            else // Attack Enemy
                HP -= dmg;
        }
    }
}
