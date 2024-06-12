using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    private Vector2 _moveDir;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
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

    private void HandleMovement(Vector2 vector)
    {
        _moveDir = vector;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        
        HandleJump();

        _player.MoveCompo.SetMovement(_moveDir);
    }
    private void HandleJump()
    {
        _player.jumpTimer += Time.deltaTime;
        float jumpHeight = Mathf.Clamp01(_player.jumpTimer / _player.jumpHoldTime);
        _player.verticalVelocity = _player.jumpPower * jumpHeight;
        if (_player.verticalVelocity >= _player.jumpPower)
            _stateMachine.ChangeState(State.Move);
    }
}
