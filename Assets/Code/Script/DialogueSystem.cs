using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public Dialogue next;
    public TMP_Text textBox;
    public float TypeSpeed;
    public Dialogue dialogue;
    public Dialogue currentDialogue;
    public bool isTyping;
    // Start is called before the first frame update
    void Start()
    {
        next = dialogue;
        currentDialogue = dialogue;
        ClearDialogue();
        StartCoroutine(TypeDialogue(dialogue));
    }

    // Update is called once per frame
    void Update()
    {
        if(next != null)
            if (!currentDialogue.choice)
                PlayNormalDialogue();
    }

    private void PlayNormalDialogue()
    {   
        // No Yes
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            ClearDialogue();
            if (next != null)
            {
                StartCoroutine(TypeDialogue(next));
            }
        }
        Debug.Log(next.choice);
    }
    public void PlayChoiceDialogue()
    {   // Yes Yes
        if (currentDialogue.choice)
            if (!isTyping)
            {
                ClearDialogue();
                if (next != null)
                    StartCoroutine(TypeDialogue(next));
            }
        Debug.Log(currentDialogue.choice);
    }

    public void ClearDialogue()
    {
        textBox.text = string.Empty;
    }
    public Dialogue CheckType(Dialogue check)
    {
        if (check != null)
        {
            return check;
        }
        return null;
    }
    IEnumerator TypeDialogue(Dialogue _dia)
    {
        isTyping = true;
        foreach (char c in _dia._DialogueText.ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(TypeSpeed);
        }
        isTyping = false;
        if(next != null)
            currentDialogue = next;
        next = CheckType(_dia.nextDialogue) != null ? _dia.nextDialogue : null;
    }
}
