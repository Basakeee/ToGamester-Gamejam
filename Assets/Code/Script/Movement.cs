using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region PlayerModifier
    private float xAxis => Input.GetAxisRaw("Horizontal");
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private bool isGrounded => Physics2D.Raycast(transform.position, Vector2.down, groundCheckRange, layerCheck);
    private bool isFacingRight = true;
    [Header("GroundCheck")] 
    public LayerMask layerCheck;
    public float groundCheckRange;
    [Header("Dash")]
    private bool canDash = true;
    private bool isDashing;
    private float dashingTime = 0.2f;
    public float dashingPower = 50f;
    public float dashingCooldown = 1f;
    [SerializeField] private ParticleSystem dust;

    [Header("PlayerStats")]
    public float speed;
    public float jumpPower;
    public float fallMultipier = 2.5f;
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
        Flip();
        if (rb.velocity.y < 0f)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultipier - 1) * Time.deltaTime;
        }
    }

    private void Flip()
    {
        if (isFacingRight && Input.GetKeyDown(KeyCode.A) || !isFacingRight && Input.GetKeyDown(KeyCode.D))
        {
            isFacingRight = !isFacingRight;
            Vector3 LC = transform.localScale;
            LC.x *= -1;
            transform.localScale = LC;
            
        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.velocity = new Vector2(xAxis * speed, rb.velocity.y);
        if(rb.velocity.x > 0f && isGrounded )
            dust.Play();
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        Combat combat = GetComponent<Combat>();
        combat.iFrame();
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
