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
        HandleJump();
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
        
        
        if (_player.isGrounded)
        {
            if (_moveDir.sqrMagnitude <= 0.05f)
            {
                _stateMachine.ChangeState(State.Idle);
            }
            else
            {
                _stateMachine.ChangeState(State.Move);
            }
        }
        _player.MoveCompo.SetMovement(_moveDir * 1.2f);
    }
    private void HandleJump()
    {
        _player.isJumping = true;
        float jumpHeight = Mathf.Clamp01(_player.jumpTimer / _player.jumpHoldTime);
        _player.verticalVelocity = _player.jumpPower * jumpHeight;
        if (_player.verticalVelocity >= _player.jumpPower)
            _player.isJumping = false;
    }
}
