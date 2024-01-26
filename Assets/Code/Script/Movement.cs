using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region PlayerModifier
    private float xAxis => Input.GetAxisRaw("Horizontal");
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    public bool isGrounded => Physics2D.Raycast(transform.position, Vector2.down, groundCheckRange, layerCheck);
    [Header("GroundCheck")] 
    public LayerMask layerCheck;
    public float groundCheckRange;
    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.2f;
    public float dashingPower = 50f;
    public float dashingCooldown = 1f;
    //[SerializeField] private TrailRenderer tr;

    [Header("PlayerStats")]
    public float speed;
    public float jumpPower;
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpPower, ForceMode2D.Impulse);
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(xAxis * speed, rb.velocity.y);
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        //tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        //tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
