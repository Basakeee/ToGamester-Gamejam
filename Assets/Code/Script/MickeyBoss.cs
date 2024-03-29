using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class MickeyBoss : Boss
{
    public Rigidbody2D WaveSkill;
    public Rigidbody2D BulletSkill;
    public Transform pos;
    public float skillSpeed;
    public float bulletSpeed;
    public float _Time;
    public Vector2 velocity = Vector2.zero;
    public bool DoneSkill1 = true;
    public Vector2 offsetBossDetect;
    private Vector3 offBossDetect => offsetBossDetect;
    public Vector2 detectRange;
    public LayerMask playerLayer;
    public bool isPlayerinRange => Physics2D.OverlapBox(transform.position, detectRange, 0, playerLayer) != null ? true : false;
    private bool DoneSkill2 = true;
    private Animator anim => GetComponent<Animator>();
    private bool oneTime;
    public float range;
    public LayerMask _layerMask;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private bool faceRight;
    public bool StartSkill;
    public AudioSource audioBoom;
    public AudioSource audiogun;
    private Transform player => GameObject.FindWithTag("Player").transform;

    private void Update()
    {
        if (isPlayerinRange)
        {

            StartSkill = Physics2D.Raycast(transform.position, Vector2.down, range, _layerMask) && !DoneSkill1;
            _Time += Time.deltaTime;
            if (_Time > Cooldown[0] && DoneSkill1 && !oneTime)
            {
                Skill();
                audioBoom.clip = audioClips[0];
                audioBoom.Play();
            }
            if (_Time > Cooldown[1] + Cooldown[0] && DoneSkill2)
            {
                Skill2();
                audiogun.clip = audioClips[1];
                audiogun.Play();
                _Time = 0;
                DoneSkill2 = true;
                oneTime = false;
            }
            Debug.DrawRay(transform.position, Vector2.down * range, Color.red);
        }
    }
    public override void TakeDMG(int dmg, Combat.WeaponType type, Transform player)
    {
        TakeWeaponDMG(dmg, type);
        if (HP <= 0)
        {
            audioBoom.clip = audioClips[2];
            audioBoom.Play();
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
    }
    public void EndSkill2()
    {
        Rigidbody2D bullet = Instantiate(BulletSkill,pos.position, Quaternion.identity);
        bullet.AddForce((new Vector2(player.transform.position.x - pos.position.x, player.transform.position.y - pos.position.y).normalized) * bulletSpeed, ForceMode2D.Impulse);

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
        range = 5;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + offBossDetect, detectRange);
    }
}
