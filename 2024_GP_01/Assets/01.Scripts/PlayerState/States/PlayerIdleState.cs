using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnMoveEvent += HandleMovementEvent;
    }

    public override void Exit()
    {
        _player.InputCompo.OnMoveEvent -= HandleMovementEvent;
        base.Exit();
    }
    private void HandleMovementEvent()
    {
        float inputThreshold = 0.05f;
        
        if (_player.MoveCompo.Velocity.sqrMagnitude > inputThreshold)
        {
            _stateMachine.ChangeState(State.Walk);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }
}
