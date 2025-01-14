using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [Header("Stun info")]
    public float stunDuration = 0.8f;
    public Vector2 stunDirection;
    protected bool canBeStun = true;
    [SerializeField] protected GameObject counterImage;

    [Header("Move info")]
    public float moveSpeed = 2f;
    public float ildeTime = 1.5f;

    [Header("Attack info")]
    public float attackDistance = 1.5f;
    public float playerCheckDistance = 4f;
    public float attackCooldown = .5f;
    [HideInInspector] public float lastTimeAttacked;
    public float battleTime = .5f;
    public LayerMask whatIsPlayer;
    protected EnemyStateMachine stateMachine { get; private set; }
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public virtual void OpenCounterAttackWindow()
    {
        canBeStun = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttackWindow()
    {
        canBeStun = false;
        counterImage.SetActive(false);
    }
    public virtual bool CanBeStun()
    {
        if (canBeStun)
        {
            CloseCounterAttackWindow();
            return true;
        }
        return false;
    }
    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheckPosition.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));
    }
    
}
