using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1.5f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("Jump"))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }

        if(stateTimer <= 0)
        {
            player.SetVelocity(0.5f * -player.facingDir, player.rb.velocity.y);
            stateMachine.ChangeState(player.idleState);
            return;
        }

        if (velX != 0 && velX != player.facingDir)
            stateMachine.ChangeState(player.idleState);

        if(velY < 0)
            player.SetVelocity(0, player.rb.velocity.y);
        else
            player.SetVelocity(0, player.rb.velocity.y * 0.75f);

        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
