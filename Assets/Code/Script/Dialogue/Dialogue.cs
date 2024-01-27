using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
[CreateAssetMenu(menuName = "Dialogue/newDialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField, TextArea(3, 5)]
    public string _DialogueText;
    public Dialogue nextDialogue;
    public bool choice;
    public AudioClip audio;
    
}
