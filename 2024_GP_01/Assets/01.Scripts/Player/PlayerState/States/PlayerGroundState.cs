using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnJumpEvent += HandleJump;
    }


    public override void Exit()
    {
        _player.InputCompo.OnJumpEvent -= HandleJump;
        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _player.verticalVelocity -= _player.gravity * Time.deltaTime;
    }
    private void HandleJump()
    {
        if (_player.isGrounded)
        {
            _stateMachine.ChangeState(State.Jump);
        }
    }
}
