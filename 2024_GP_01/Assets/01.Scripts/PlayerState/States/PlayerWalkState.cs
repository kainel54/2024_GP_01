using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    private Vector3 _movementDir;
    public PlayerWalkState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnMoveEvent += HandleMovement;
    }

    
    public override void Exit()
    {
        _player.InputCompo.OnMoveEvent -= HandleMovement;
        base.Exit();
    }
    private void HandleMovement(Vector2 movement)
    {
        float inputThreshold = 0.05f;
        if (movement.sqrMagnitude<=inputThreshold)
        {
            _stateMachine.ChangeState(State.Idle);
        }
        else
        {
            _movementDir = _player.MoveCompo.Velocity.normalized;
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _player.MoveCompo.SetMovement(_movementDir * _player.moveSpeed) ;
    }
}
