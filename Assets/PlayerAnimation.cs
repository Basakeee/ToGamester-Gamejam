using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAnimation : MonoBehaviour
{
    public Movement moveMent => GetComponent<Movement>();
    public Animator animator => GetComponent<Animator>();
    private Combat comBat => GetComponent<Combat>();

    public AudioClip[] audioClips;
    public AudioMixer audioMixer;
    public AudioSource audioattack;
    public AudioSource audiojump;
    public AudioSource audiowalk;
    public AudioSource audiodie;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (comBat.currentWeapon == Combat.WeaponType.Hoe)
        {
            if (moveMent.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("Jump", !moveMent.isGrounded);

                audiojump.clip = audioClips[0];

                audiojump.Play();

            }

            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                animator.SetFloat("Walk", Mathf.Abs(moveMent.xAxis));
                audiowalk.clip = audioClips[1];
                audiowalk.Play();
            }


            if (comBat.isATK)
            {
                audioattack.clip = audioClips[2];
                audioattack.Play();

                animator.SetTrigger("Attack");
            }



            if (comBat.curHP <= 0)
            {
                audiodie.clip = audioClips[3];
                audiodie.Play();

                animator.SetTrigger("Die");
                Destroy(gameObject, 4f);
                comBat.enabled = false;
                moveMent.enabled = false;
            }
        }

        if (comBat.currentWeapon == Combat.WeaponType.HolyWeapons)
        {
            if (moveMent.isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("HOLYJUMP", !moveMent.isGrounded);
                audiojump.clip = audioClips[0];

                audiojump.Play();

            }
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                animator.SetFloat("HOLYWALK", Mathf.Abs(moveMent.xAxis));
                audiowalk.clip = audioClips[1];
                audiowalk.Play();
            }
            if (comBat.isATK)

            {
                audioattack.clip = audioClips[2];
                audioattack.Play();
                animator.SetTrigger("HOLYATTACK");
            }
        }
    }
}