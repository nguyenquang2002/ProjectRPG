using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState curentState { get; private set; }
    public void Initialize(PlayerState _startState)
    {
        curentState = _startState;
        curentState.Enter();
    }
    public void ChangeState(PlayerState _newState)
    {
        curentState.Exit();
        curentState = _newState;
        curentState.Enter();
    }
}
