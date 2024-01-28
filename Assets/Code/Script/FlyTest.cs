using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTest : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public Transform CurrentPoint;
    public Rigidbody2D rb => GetComponent<Rigidbody2D>();
    public float speed;
    public LayerMask mask,player;
    public float PlayerDetectRange;
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
    private Collider2D playerDetect => Physics2D.OverlapCircle(transform.position, PlayerDetectRange, mask);
    private Collider2D playerinAttackRange => Physics2D.OverlapCircle(transform.position + (off * -transform.localScale.x), range, player);
    private int isFacingRight => transform.localScale.x > 0 ? 1 : -1;

    private bool isPatrol = true;
    private bool faceRight;
    private bool isATK;
    // Start is called before the first frame update
    void Start()
    {
        CurrentPoint = pointB;
    }

    // Update is called once per frame
    void Update()
    {
        if (!es.isKnockback)
        {
            if (isPatrol)
                Route();

            FindPlayer();

        }
    }

    private void FindPlayer()
    {
        if (playerDetect != null) // If Found the player
        {
            isPatrol = false;
            ChasePlayer();
            enemyState = State.Chase;
            
            if (playerinAttackRange != null) // in range can attack
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
        Vector2 direction = new Vector2(playerDetect.transform.position.x - transform.position.x, playerDetect.transform.position.y - transform.position.y).normalized;
        rb.velocity = direction * speed;
        if (faceRight && transform.position.x > playerDetect.transform.position.x)
                Flip();
            if (!faceRight && transform.position.x < playerDetect.transform.position.x)
                Flip();
    }
    private void Route()
    {
        enemyState = State.Patrol;
        if (CurrentPoint == pointB)
        {
            if (faceRight)
            {
                Flip();
            }
            Vector2 direction = new Vector2(pointA.position.x - transform.position.x, pointA.position.y - transform.position.y).normalized;
            rb.velocity = direction * speed;
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
            Vector2 direction = new Vector2(pointB.position.x - transform.position.x, pointB.position.y - transform.position.y).normalized;
            rb.velocity = direction * speed;
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
        faceRight = !faceRight;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, PlayerDetectRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (off * -transform.localScale.x), range);
    }
    IEnumerator AttackCD(float CD)
    {
        isATK = true;
        yield return new WaitForSeconds(CD);
        isATK = false;

    }
}
