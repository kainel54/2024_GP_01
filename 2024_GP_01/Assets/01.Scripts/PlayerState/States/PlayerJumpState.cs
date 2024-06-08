using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnJumpEvent += HandleJump;
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
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
