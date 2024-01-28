using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private Animator anim => GetComponent<Animator>();
    private bool oneTime;
    public float range;
    public LayerMask _layerMask;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private bool faceRight;
    public bool StartSkill;
    private Transform player => GameObject.FindWithTag("Player").transform;
     
    private void Update()
    {
        StartSkill = Physics2D.Raycast(transform.position, Vector2.down, range, _layerMask) && !DoneSkill1;
        _Time += Time.deltaTime;
        if (_Time > Cooldown[0] && DoneSkill1 && !oneTime)
        {
            Skill();
        }
        if(_Time > Cooldown[1] + Cooldown[0] && DoneSkill2) 
        {
            Skill2();
            _Time = 0;
            DoneSkill2 = true;
            oneTime = false;
        }
        Debug.DrawRay(transform.position, Vector2.down * range, Color.red);
        if(HP <= 0)
        {
            SceneManager.LoadScene(5);
        }
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
        rb.AddForce(new Vector2(2,8), ForceMode2D.Impulse);
        CreateSkill();
        StartCoroutine(addRemoveRange());
    }
    public override void Skill2()
    {
        DoneSkill2 = false;
        anim.SetTrigger("Attack");
        DoneSkill2 = true;
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
