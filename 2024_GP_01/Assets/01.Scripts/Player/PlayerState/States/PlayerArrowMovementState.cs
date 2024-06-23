using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowMovementState : PlayerState
{
    private Vector2 _movementDir;

    public PlayerArrowMovementState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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
        if (movement.sqrMagnitude <= inputThreshold)
        {
            _stateMachine.ChangeState(State.ArrowIdle);
        }
        else
        {
            _movementDir = movement;
        }


        if (_player.isRunning)
        {
            _stateMachine.ChangeState(State.ArrowRunning);
        }
    }

    private void HandleFire(bool value)
    {
        if (!value)
        {
            _stateMachine.ChangeState(State.Move);
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _player.MoveCompo.SetMovement(_movementDir);
        _player.AnimCompo.SetFloat("InputMagnitude", _movementDir.magnitude * _player.currentAcceleration, .1f, Time.deltaTime);
    }
}
