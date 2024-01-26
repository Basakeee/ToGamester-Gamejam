using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public float TypeSpeed;
    [HideInInspector] public Dialogue dialogue;
    [HideInInspector] public bool isTyping;
    public TMP_Text textBox;
    public CanvasGroup dialogueGroup;
    public CanvasGroup button;
    public bool endDialogue = true;
    private Dialogue next;
    [HideInInspector] public Dialogue currentDialogue;
    private bool isEnd;
    public void StartDialogue(Dialogue nextDialogue)
    {
        next = nextDialogue;
        currentDialogue = nextDialogue;
        ClearDialogue();
        StartCoroutine(TypeDialogue(nextDialogue));
        dialogueGroup.alpha = 1;
        button.interactable = true;
        endDialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (next != null)
            if (!currentDialogue.choice)
                PlayNormalDialogue();

        if(currentDialogue != null)
            HideShowButton();

        if (Input.GetMouseButtonDown(0))
            if(isEnd && next == null)
                CloseDialogue();
    }

    private void HideShowButton()
    {
        if (currentDialogue.choice)
        {
            button.interactable = true;
            button.alpha = 1;

        }
        if (!currentDialogue.choice)
        {
            button.interactable = false;
            button.alpha = 0;
        }
    }

    private void PlayNormalDialogue()
    {   
        // No Yes
        if (Input.GetMouseButtonDown(0) && !isTyping)
        {
            ClearDialogue();
            // Hide Button
            if (next != null)
            {
                StartCoroutine(TypeDialogue(next));
            }
        }
    }
    public void PlayChoiceDialogue()
    {   // Yes Yes
        if (currentDialogue.choice)
            if (!isTyping)
            {
                ClearDialogue();
                dialogueGroup.interactable = true;
                button.interactable = true;
                button.alpha = 1;
                if (next != null)
                {
                    StartCoroutine(TypeDialogue(next));
                }
            }
    }

    private void ClearDialogue()
    {
        textBox.text = string.Empty;
    }
    private void CloseDialogue()
    {
        dialogueGroup.alpha = 0;
        dialogue = null;
        currentDialogue = null;
        endDialogue = true;
    }
    private Dialogue CheckType(Dialogue check)
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
        isEnd = false;
        if(next != null)
            currentDialogue = next;
        foreach (char c in _dia._DialogueText.ToCharArray())
        {
            textBox.text += c;
            yield return new WaitForSeconds(TypeSpeed);
        }
        isTyping = false;
        isEnd = true;
        next = CheckType(_dia.nextDialogue) != null ? _dia.nextDialogue : null;
    }
}
