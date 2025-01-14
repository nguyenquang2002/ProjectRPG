using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Collision Check")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheckPosition;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheckPosition;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackVector;
    [SerializeField] protected float knockbackDuration = 0.05f;
    protected bool isKnockback;

    public int facingDir { get; private set; } = 1;
    [SerializeField] protected bool facingRight = true;

    #region Components
    public Animator animator;
    public Rigidbody2D rb;
    public ChararcterFX fx;
    #endregion
    // Start is called before the first frame update
    protected virtual void Start()
    {
        fx = GetComponent<ChararcterFX>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, 0);
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //FlipController(rb.velocity.x);
    }
    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");
        StartCoroutine("HitKnockback");
    }
    protected virtual IEnumerator HitKnockback()
    {
        isKnockback = true;
        rb.velocity = new Vector2(knockbackVector.x * -facingDir, knockbackVector.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnockback = false;
        rb.velocity = new Vector2(0, 0);
    }
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheckPosition.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheckPosition.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheckPosition.position, new Vector3(wallCheckPosition.position.x + wallCheckDistance * facingDir, wallCheckPosition.position.y));
        Gizmos.DrawLine(groundCheckPosition.position, new Vector3(groundCheckPosition.position.x, groundCheckPosition.position.y - groundCheckDistance));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void FlipController(float x)
    {
        if (x > 0 && !facingRight)
        {
            Flip();
        }
        else if (x < 0 && facingRight)
        {
            Flip();
        }
    }
    public void ZeroVelocity()
    {
        if (isKnockback)
            return;

        rb.velocity = new Vector2(0, 0);
    }
    public void SetVelocity(float x, float y)
    {
        if (isKnockback)
            return;

        rb.velocity = new Vector2 (x, y);
        FlipController(x);
    }
}
