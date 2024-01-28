using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class PlayerAnimation : MonoBehaviour
{
    public Movement moveMent => GetComponent<Movement>();
    public Animator animator => GetComponent<Animator>();
    private Combat comBat => GetComponent<Combat>();
    private bool attackSoundDone;

    public AudioClip[] audioClips;
    public AudioSource audioattack;
    public AudioSource audiojump;
    public AudioSource audiowalk;
    public AudioSource audiodie;


    // Start is called before the first frame update
    void Start()
    {
        attackSoundDone = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.Abs(moveMent.xAxis));
        if (comBat.currentWeapon == Combat.WeaponType.Hoe)
        {
                Debug.Log(moveMent.isGrounded && Input.GetKeyDown(KeyCode.Space));
            if (moveMent.isGrounded)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetBool("Jump", moveMent.isGrounded);
                }
                else
                    animator.SetBool("Jump",!moveMent.isGrounded);

                audiojump.clip = audioClips[0];

                audiojump.Play();

            }

            animator.SetFloat("Walk", Mathf.Abs(moveMent.xAxis));
            if (Mathf.Abs(moveMent.xAxis) > 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            {
                StartCoroutine(soundCD(0.5f));
                audiowalk.clip = audioClips[1];
                audiowalk.Play();
            }


            if (comBat.isATK && attackSoundDone)
            {
                audioattack.clip = audioClips[2];
                audioattack.Play();
                StartCoroutine(soundCD(1));

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
            if (moveMent.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    animator.SetBool("HOLYJUMP", moveMent.isGrounded);
                }
                else
                    animator.SetBool("HOLYJUMP", !moveMent.isGrounded);
                audiojump.clip = audioClips[0];

                audiojump.Play();

                animator.SetFloat("HOLYWALK", Mathf.Abs(moveMent.xAxis));
            }
            if (Mathf.Abs(moveMent.xAxis) > 0 && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
            {
                audiowalk.clip = audioClips[1];
                audiowalk.Play();
            }
            if (comBat.isATK && attackSoundDone)
            {
                audioattack.clip = audioClips[2];
                audioattack.Play();
                StartCoroutine(soundCD(1));
                animator.SetTrigger("HOLYATTACK");
            }
        }
    }
    IEnumerator soundCD(float CD)
    {
        attackSoundDone = false;
        yield return new WaitForSeconds(CD);
        attackSoundDone = true;
    }
}