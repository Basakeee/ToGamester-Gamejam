using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicControt : MonoBehaviour
{
    public AudioClip[] audioClips;
    public AudioSource audioSource;
    public AudioMixer audioMixer;
    public AudioSource audioSfx;
    public AudioSource NPC;
    public Slider slider;
    public Slider sliderMusic;
    public Slider sliderSfx;
    public Slider sliderNPC;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // เปลี่ยนค่าของ audioSfx.clip เป็นคลิปเสียง audioClips[1] เมื่อกดแป้น E อีกครั้ง

                audioSfx.clip = audioClips[1];
            
            audioSfx.Play();

        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            audioSfx.clip = audioClips[2];
            audioSfx.Play();

        }

    }
    public void MasterMusicslider()
    {
        audioMixer.SetFloat("MasterVolume", slider.value);
    }
    public void Musicslider()
    {
        audioMixer.SetFloat("MusicVolume", sliderMusic.value);
    }
    public void SFXslider()
    {
        audioMixer.SetFloat("sfxVolume", sliderSfx.value);
    }
    public void NPCslider()
    {
        audioMixer.SetFloat("NPCVolume", sliderNPC.value);
    }
}
