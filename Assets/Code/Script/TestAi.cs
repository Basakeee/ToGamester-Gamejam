using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAi : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform CurrentPoint;
    public Rigidbody2D rb => GetComponent<Rigidbody2D>();
    public float speed;
    public LayerMask mask,wall,ground;
    public float PlayerDetectRange;
    public float WallDetectRange;
    public float GroundDetectRange;
    public float JumpPower;
    public float jumpcooldown;
    private bool canJump = true;
    private Collider2D playerDetect => Physics2D.OverlapCircle(transform.position, PlayerDetectRange, mask);
    private int isFacingRight => transform.localScale.x > 0 ? -1 : 1;

    private bool wallDetect => Physics2D.Raycast(transform.position, transform.right * isFacingRight, WallDetectRange, wall);
    private bool groundDetect => Physics2D.Raycast(transform.position, -transform.up,GroundDetectRange, ground);
    private bool isPatrol = true;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPoint = pointB;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrol)
            Route();

        FindPlayer();
        ShowRay();
    }

    private void ShowRay()
    {
        Debug.DrawRay(transform.position, transform.right * isFacingRight * WallDetectRange,Color.red);
        Debug.DrawRay(transform.position, -transform.up * GroundDetectRange,Color.magenta);
    }

    private void FindPlayer()
    {
        if (playerDetect != null) // If Found the player
        {
            isPatrol = false;
            if (transform.position.x > playerDetect.transform.position.x)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
            if (transform.position.x < playerDetect.transform.position.x)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            if (wallDetect && groundDetect && canJump)
            {
                rb.AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
                StartCoroutine(jumpCD(jumpcooldown));
            }
        }
        else
            isPatrol = true;
    }

    private void Route()
    {
        if (CurrentPoint == pointB)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (Vector2.Distance(transform.position, pointA.position) < 1f)
            {
                CurrentPoint = pointA;
                Flip();
            }
        }
        if (CurrentPoint == pointA)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (Vector2.Distance(transform.position, pointB.position) < 1f)
            {
                CurrentPoint = pointB;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 LC = transform.localScale;
        LC.x *= -1f;
        transform.localScale = LC;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectRange);
    }
    IEnumerator jumpCD(float CD)
    {
        canJump = false;
        yield return new WaitForSeconds(CD);
        canJump = true;
    }
}
