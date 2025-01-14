using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;
    protected bool triggerCalled;

    protected float velX, velY, stateTimer;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Enter()
    {
        player.animator.SetBool(animBoolName, true);
        triggerCalled = false;
    }
    public virtual void Update()
    {
        CheckInput();
        player.animator.SetFloat("velY", player.rb.velocity.y);
        stateTimer -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        player.animator.SetBool(animBoolName, false);
    }
    private void CheckInput()
    {
        velX = Input.GetAxisRaw("Horizontal");
        velY = Input.GetAxisRaw("Vertical");
    }
    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
