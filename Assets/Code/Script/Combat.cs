using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Combat : MonoBehaviour
{
    #region PlayerStats
    [Header("Player Stats")]
    public int maxHP = 10;
    public int curHP;
    public int Damage;
    #endregion
    #region Attack
    [Header("Attack modifier")]
    public float cooldown;
    public Vector2 offset;
    public float range;
    public LayerMask enemyLayer;
    public LayerMask itemLayer;
    #endregion
    #region Special
    [Header("Special")]
    public int HealAmount;
    public int healLeft;
    public enum WeaponType
    {
        Hoe,
        HolyWeapons
    }
    public WeaponType currentWeapon;
    #endregion
    public bool isATK;
    private bool canTakeDMG = true;
    public Volume volume;
    public PlayerItem item;
    public PlayerVFX playerVFX => GetComponent<PlayerVFX>();
    private Vector3 off => offset;
    // Start is called before the first frame update
    void Start()
    {
        curHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        healLeft = item.HealAmount;
        CheckStats();
        PickUpItem();
        if (Input.GetKeyDown(KeyCode.E) && item.HealAmount > 0)
        {
            Heal(HealAmount);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    private void CheckStats()
    {
        if (curHP > maxHP) 
                curHP = maxHP;
    }

    private void Attack()
    {
        if(!isATK)
        {
            Debug.Log("Attack");
            StartCoroutine(AttackCooldown(cooldown));
            Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position + (off * transform.localScale.x), range,enemyLayer);
            if (hit != null) 
                foreach(Collider2D _en in hit)
                {
                    _en.TryGetComponent<EnemyStats>(out EnemyStats enemy);
                    enemy.TakeDMG(Damage,currentWeapon,transform);
                    Debug.Log(enemy.HP);
                    Debug.Log(_en.name);
                }
        }
    }
    private void PickUpItem()
    {
        Collider2D[] picks = Physics2D.OverlapBoxAll(transform.position, transform.localScale * 1.2f, 0, itemLayer);
        if(picks != null)
            foreach (Collider2D _item in picks)
            {
                if (_item.gameObject.layer == LayerMask.NameToLayer("Item"))
                {
                    item.HealAmount++;
                    Destroy(_item.gameObject);
                }
                if (_item.gameObject.layer == LayerMask.NameToLayer("Holy"))
                {
                    currentWeapon = Combat.WeaponType.HolyWeapons;
                    Destroy(_item.gameObject);
                }
            }

    }
    public void TakeDMG(int DMG)
    {
        if (canTakeDMG)
        {
            curHP -= DMG;
            iFrame();
            Debug.Log(curHP);
          // Particle
        }
    }
    public void Heal(int heal)
    {
        curHP += heal;
        item.HealAmount--;
        // haunted effect
        if(volume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.hueShift.value = -180;
            StartCoroutine(VolumeCooldown(colorAdjustments));
            playerVFX.CreateHorror();
        }
  
    }
    public void iFrame()
    {
        if(canTakeDMG)
        StartCoroutine(iFrameCooldown());
    }
    IEnumerator AttackCooldown(float CD)
    {
        isATK = true;
        yield return new WaitForSeconds(CD);
        isATK = false;
    }
    IEnumerator iFrameCooldown()
    {
        canTakeDMG = false;
        yield return new WaitForSeconds(0.2f);
        canTakeDMG = true;
    }

    IEnumerator VolumeCooldown(ColorAdjustments colorAdjustments)
    {
        yield return new WaitForSeconds(2f);
        colorAdjustments.hueShift.value = 0;
    }

    private void OnDrawGizmos()
    { 
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + (off * transform.localScale.x), range);
    }

}
