using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MickeyBoss : Boss
{
    public Rigidbody2D WaveSkill;
    public Transform pos;
    public float skillSpeed;
    public float _Time;
    public Vector2 velocity = Vector2.zero;
    public bool DoneSkill1 = true;
    private bool DoneSkill2 = true;
    private bool AllSkillDone = true;
    private bool oneTime;
    public float range;
    public LayerMask _layerMask;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Collider2D col;
    private bool faceRight;
    private bool StartSkill => Physics2D.Raycast(transform.position, Vector2.down, range, _layerMask) && !DoneSkill1;
    private Transform player => GameObject.FindWithTag("Player").transform;
     
    private void Update()
    {
        _Time += Time.deltaTime;
        if (_Time > Cooldown[0] && DoneSkill1 && AllSkillDone)
        {
            Skill();
        }
        if (StartSkill && !oneTime)
            CreateSkill();
        Debug.DrawRay(transform.position, Vector2.down * range, Color.red);
    }
    public override void TakeDMG(int dmg, Combat.WeaponType type, Transform player)
    {
        TakeWeaponDMG(dmg, type);
        if (HP <= 0)
        {
            Destroy(gameObject);
            Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        }
    }
    public override void Skill() // Jump
    {
        DoneSkill1 = false;
        rb.AddForce(Vector2.up * 8, ForceMode2D.Impulse);
        StartCoroutine(addRemoveRange());
    }

    private void CreateSkill()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down, range, _layerMask) && !DoneSkill1)
        {
            // Wave toward Player
            Rigidbody2D skill = Instantiate(WaveSkill, pos.position, Quaternion.identity);
            Destroy(skill.gameObject,5f);
            DoneSkill1 = true;
            oneTime = true;

            if (transform.position.x > player.transform.position.x)
            {
                skill.velocity = new Vector2(-skillSpeed, 0);
                if (faceRight)
                    Flip(skill);
            }
            if (transform.position.x < player.transform.position.x)
            {
                skill.velocity = new Vector2(skillSpeed, 0);
                if (!faceRight)
                    Flip(skill);
            }
        }
    }

    public override void Skill2()
    {

    }
    void Flip(Rigidbody2D _Skill)
    {
        faceRight = !faceRight;
        Vector3 LC = _Skill.transform.localScale;
        LC.x *= -1f;
        _Skill.transform.localScale = LC;
    }
    IEnumerator addRemoveRange()
    {
        range = 1;
        yield return new WaitForSeconds(1);
        range = 3;
    }
}
