using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PoohBoss : Boss
{
    public float chargeSpeed;
    private Animator anim => GetComponent<Animator>();
    public float _Time;
    public Vector2 _area;
    public LayerMask _layerMask;
    public Vector2 offset;
    public int BossDamage;
    private Vector3 off => offset;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Transform player => GameObject.FindWithTag("Player").transform;
    public bool Charging;
    private bool isFacingRight;
    private Collider2D[] hit;

    public AudioClip[] audioClips;
    public AudioMixer audioMixer;
    public AudioSource audiodie;
    public AudioSource audioCharge;
    
    public override void TakeDMG(int dmg, Combat.WeaponType type, Transform player)
    {
        TakeWeaponDMG(dmg, type);
        if (HP <= 0)
            audiodie.clip = audioClips[0];
        audiodie.Play();
        {
            Destroy(gameObject);
            Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        }
    }
    private void Update()
    {
        if(!Charging)
        _Time += Time.deltaTime;
        if(_Time > Cooldown[0] && !Charging)
        {
            Skill();
            _Time = 0;
        }
        if (Charging && anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Dash"))
        {
            hit = Physics2D.OverlapBoxAll(transform.position + off, _area, 0, _layerMask);
            if(hit != null) 
            { 
                foreach (Collider2D col in hit)
                {
                    if (col.TryGetComponent<Combat>(out Combat _player))
                    {
                        _player.TakeDMG(BossDamage);
                        hit = null;
                        break;
                    }
                }
                Debug.Log(hit);
                anim.Play("Base Layer.pooh", 0, 0f);
                rb.velocity = Vector2.zero;
                Charging = false;
            }
        }
    }
    public override void Skill()
    {
        Charging = true;
        anim.SetTrigger("Charge");
        StartCoroutine(waitBeforeDash());
    }
    IEnumerator waitBeforeDash()
    {
        audiodie.clip = audioClips[1];
        audiodie.Play();
        yield return new WaitForSeconds(1.5f);
        float Direct = transform.position.x > player.transform.position.x ? -1 : 1;
        
        if (transform.position.x < player.transform.position.x && !isFacingRight)
            Flip();
        else if (transform.position.x > player.transform.position.x && isFacingRight)
            Flip();
        rb.velocity = new Vector2(chargeSpeed * Direct, rb.velocity.y);
        
    }
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 LC = transform.localScale;
        LC.x *= -1;
        transform.localScale = LC;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position + off, _area);
    }
}
