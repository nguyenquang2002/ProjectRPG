using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.animator.SetBool("SuccessfullCounterAttack", false);
        player.ZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        player.animator.SetBool("SuccessfullCounterAttack", false);
    }

    public override void Update()
    {
        base.Update();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (hit.GetComponent<Enemy>().CanBeStun())
                {
                    stateTimer = 10;
                    player.animator.SetBool("SuccessfullCounterAttack", true);
                }
            }
        }

        if(stateTimer < 0 || triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
