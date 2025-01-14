using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Transform player;
    private EnemySkeleton enemy;
    private int moveDir;
    public SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySkeleton _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                enemy.ZeroVelocity();
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                }
                return;
            }
        }
        else
        {
            if(stateTimer <= 0f)
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }
        if (!enemy.IsGroundDetected() || enemy.IsWallDetected())
        {
            enemy.SetVelocity(0, enemy.rb.velocity.y);
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if (player.position.x <  enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir * 1.5f, enemy.rb.velocity.y);
    }
    private bool CanAttack()
    {
        if(Time.time >= enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
