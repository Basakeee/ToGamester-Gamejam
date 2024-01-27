using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{
    public Vector2 Area;
    public Vector2 offset;
    public LayerMask mask;
    private Vector3 off => offset;
    private Collider2D hit;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hit = Physics2D.OverlapBox(transform.position + (off * transform.localScale.x), Area, 0, mask);
        if(hit != null)
        {
            if (hit.TryGetComponent<Combat>(out Combat player))
            {
                player.TakeDMG(2);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position + (off * transform.localScale.x), Area);
    }
}
