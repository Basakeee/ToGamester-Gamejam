using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    public float CheckRange;
    public LayerMask playerLayer;
    private Collider2D[] hit;
    public Dialogue startDialogue;
    private void Update()
    {
        hit = Physics2D.OverlapBoxAll(transform.position,new Vector2(CheckRange, CheckRange),0f,playerLayer);
        if (hit.Length > 0)
        {
            Debug.Log("hit info" + hit.Length);
            if (Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("Pressing");
                TalkToPlayer(startDialogue);
            }
        }
    }

    public void TalkToPlayer(Dialogue _dialogue)
    {
        DialogueSystem _ds = GameObject.Find("DialogueSystem").GetComponent<DialogueSystem>();
        if(!_ds.isTyping && _ds.endDialogue)
            _ds.StartDialogue(_dialogue);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position,new Vector3(CheckRange,CheckRange,CheckRange));
    }

}
