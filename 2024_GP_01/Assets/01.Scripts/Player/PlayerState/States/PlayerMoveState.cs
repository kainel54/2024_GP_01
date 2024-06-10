using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundState
{
    private Vector2 _movementDir;
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnMoveEvent += HandleMovement;
        _player.InputCompo.OnFireEvent += HandleFire;
    }
    public override void Exit()
    {
        _player.InputCompo.OnMoveEvent -= HandleMovement;
        _player.InputCompo.OnFireEvent -= HandleFire;

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
            _movementDir = movement;
        }
    }
    private void HandleFire(bool value)
    {
        if (value)
        {
            if (_player.isRunning)
            {
                _stateMachine.ChangeState(State.ArrowRunning);
            }
            else
            {
                if( _player.isRunning&&_player.isGrounded)
                _stateMachine.ChangeState(State.ArrowMovement);
            }
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _player.MoveCompo.SetMovement(_movementDir);
        _player.AnimCompo.SetFloat("InputMagnitude", _movementDir.magnitude * _player.currentAcceleration, .1f, Time.deltaTime);
    }
}
