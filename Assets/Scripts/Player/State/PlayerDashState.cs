using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerDashState : PlayerState
{
    private float gravity;
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        gravity = player.rb.gravityScale;
        stateTimer = player.dashTime;
        player.rb.gravityScale = 0;
        SkillManager.instance.cloneSkill.CreateClone(player.transform, player.dashDir);
    }

    public override void Exit()
    {
        base.Exit();
        player.ZeroVelocity();
        player.rb.gravityScale = gravity;
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(player.dashDir * player.dashSpeed, 0);
        
        if(!player.IsGroundDetected() && player.IsWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        if(stateTimer <= 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
