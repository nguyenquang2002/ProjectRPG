using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeleton : Enemy
{
    #region States
    public SkeletonIdleState idleState { get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }
    #endregion
    
    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState(this, stateMachine, "isIdle", this);
        moveState = new SkeletonMoveState(this, stateMachine, "isMoving", this);
        battleState = new SkeletonBattleState(this, stateMachine, "isMoving", this);
        attackState = new SkeletonAttackState(this, stateMachine, "isAttacking", this);
        stunnedState = new SkeletonStunnedState(this, stateMachine, "isStunning", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
    public override bool CanBeStun()
    {
        if (base.CanBeStun())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
}
