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
    public LayerMask mask,ground,player;
    public float PlayerDetectRange;
    public float WallDetectRange;
    public float GroundDetectRange;
    public float JumpPower;
    public float jumpcooldown;
    public float range;
    public int dmg;
    public float atkCooldown;
    public Vector2 offset;
    private Vector3 off => offset;
    private EnemyStats es => GetComponent<EnemyStats>();
    public enum State
    {
        Patrol,
        Chase
    }
    public State enemyState;

    private bool canJump = true;
    private Collider2D playerDetect => Physics2D.OverlapCircle(transform.position, PlayerDetectRange, mask);
    private Collider2D playerinAttackRange => Physics2D.OverlapCircle(transform.position + (off * -transform.localScale.x), range, player);
    private int isFacingRight => transform.localScale.x > 0 ? -1 : 1;

    private bool wallDetect => Physics2D.Raycast(transform.position, transform.right * isFacingRight, WallDetectRange, ground);
    private bool groundDetect => Physics2D.Raycast(transform.position, -transform.up,GroundDetectRange, ground);
    private bool isPatrol = true;
    private bool faceRight;
    private bool isATK;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPoint = pointB;
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!es.isKnockback)
        {
            if (isPatrol)
                Route();

            FindPlayer();

        }
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
            ChasePlayer();
            enemyState = State.Chase;
            if(playerinAttackRange != null) // in range can attack
            {
                AttackPlayer();
            }
        }
        else
        {
            enemyState = State.Patrol;
            isPatrol = true;
        }
    }

    private void AttackPlayer()
    {
        Debug.Log(playerinAttackRange != null);
        if (playerinAttackRange.TryGetComponent<Combat>(out Combat _player))
            if (!isATK)
            {
                _player.TakeDMG(dmg);
                StartCoroutine(AttackCD(atkCooldown));
            }
    }

    private void ChasePlayer()
    {
        if (transform.position.x > playerDetect.transform.position.x)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (faceRight)
                Flip();
        }
        if (transform.position.x < playerDetect.transform.position.x)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            if (!faceRight)
                Flip();
        }
        if (wallDetect && groundDetect && canJump)
        {
            rb.AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
            StartCoroutine(jumpCD(jumpcooldown));
        }
    }

    private void Route()
    {
        enemyState = State.Patrol;
        if (CurrentPoint == pointB)
        {
            if(faceRight)
            {
                Flip();
            }
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            if (Vector2.Distance(transform.position, pointA.position) < 1f)
            {
                CurrentPoint = pointA;
                Flip();
            }
        }
        if (CurrentPoint == pointA)
        {
            if (!faceRight)
            {
                Flip();
            }
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
        faceRight = !faceRight;
        Vector3 LC = transform.localScale;
        LC.x *= -1f;
        transform.localScale = LC;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (off * -transform.localScale.x), range);
    }
    IEnumerator jumpCD(float CD)
    {
        canJump = false;
        yield return new WaitForSeconds(CD);
        canJump = true;
    }
    IEnumerator AttackCD(float CD)
    {
        isATK = true;
        yield return new WaitForSeconds(CD);
        isATK = false;

    }
}
