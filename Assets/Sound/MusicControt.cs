using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicControt : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider slider;
    public Slider sliderMusic;
    public Slider sliderSfx;
    public Slider sliderNPC;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
