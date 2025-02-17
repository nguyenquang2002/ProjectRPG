using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    #region State
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerPrimaryAttackState primaryAttackState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerAimSwordState aimSwordState { get; private set; }
    public PlayerThrowSwordState throwSwordState { get; private set; }
    public PlayerCatchSwordState catchSwordState { get; private set; }
    #endregion

    [Header("Attack Details")]
    public float[] attackMovement;
    public float counterAttackDuration = 0.2f;
    
    
    public bool isBusy { get; private set; }

    [Header("Movement")]
    [SerializeField] public float speed = 5;
    [SerializeField] public float catchSwordImpact = 1f;

    [Header("Jump")]
    [SerializeField] public float jumpForce = 7;

    [Header("Dash")]
    [SerializeField] public float dashSpeed = 10;
    [SerializeField] public float dashDir = 1;
    [SerializeField] public float dashTime = 0.2f;
    private float dashTimer = 0;
    [SerializeField] private float dashCooldown = 0.4f;

    
    public SkillManager skill { get; private set; }
    public GameObject sword { get; private set; }

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "isIdle");
        moveState = new PlayerMoveState(this, stateMachine, "isMoving");
        jumpState = new PlayerJumpState(this, stateMachine, "inAir");
        airState = new PlayerAirState(this, stateMachine, "inAir");
        dashState = new PlayerDashState(this, stateMachine, "isDashing");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "isWallSliding");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "inAir");
        primaryAttackState = new PlayerPrimaryAttackState(this, stateMachine, "isAttacking");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        aimSwordState = new PlayerAimSwordState(this, stateMachine, "AimSword");
        throwSwordState = new PlayerThrowSwordState(this, stateMachine, "ThrowSword");
        catchSwordState = new PlayerCatchSwordState(this, stateMachine, "CatchSword");
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.Initialize(idleState);

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        stateMachine.curentState.Update();
        CheckDashInput();
    }

    public void AssignNewSword(GameObject _newSword)
    {
        sword = _newSword;
    }
    public void CatchSword()
    {
        stateMachine.ChangeState(catchSwordState);
        sword = null;
    }

    public IEnumerator BusyFor(float _second)
    {
        isBusy = true;
        yield return new WaitForSeconds(_second);
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.curentState.AnimationFinishTrigger();

    private void CheckDashInput()
    {
        if (IsWallDetected())
            return;

        dashTimer -= Time.deltaTime;
        if (Input.GetButtonDown("Dash") && dashTimer <= 0)
        {
            dashTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");
            if (dashDir == 0)
                dashDir = facingDir;
            stateMachine.ChangeState(dashState);
        }
    }

}
