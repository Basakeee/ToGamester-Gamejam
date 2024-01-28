using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private Combat player => GameObject.FindWithTag("Player").GetComponent<Combat>();
    private TMP_Text healText => GameObject.Find("HealGroup").GetComponentInChildren<TMP_Text>();
    public Image HPslider => GetComponentInChildren<Image>();
    // Update is called once per frame
    void Update()
    {
        float HPValue = (float)player.curHP/(float)player.maxHP;
        HPslider.fillAmount = HPValue;
        healText.text = player.healLeft.ToString();
    }
}
