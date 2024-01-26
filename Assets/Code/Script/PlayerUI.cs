using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Combat player => GameObject.FindWithTag("Player").GetComponent<Combat>();
    public Image HPslider => GetComponentInChildren<Image>();
    // Update is called once per frame
    void Update()
    {
        float HPValue = (float)player.curHP/(float)player.maxHP;
        HPslider.fillAmount = HPValue;
    }
}
