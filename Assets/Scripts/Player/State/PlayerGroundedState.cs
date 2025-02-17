using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (Input.GetButtonDown("ThrowSword") && HasNoSword())
        {
            stateMachine.ChangeState(player.aimSwordState);
        }
        
        if (Input.GetButton("Parry"))
        {
            stateMachine.ChangeState(player.counterAttackState);
        }
        if (Input.GetButton("Attack"))
        {
            stateMachine.ChangeState(player.primaryAttackState);
        }
        if(Input.GetButtonDown("Jump") && player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.jumpState);
        }
        if(!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
    }
    private bool HasNoSword()
    {
        if (!player.sword)
        {
            return true;
        }
        player.sword.GetComponent<SwordController>().ReturnSword();
        return false;
    }
}
