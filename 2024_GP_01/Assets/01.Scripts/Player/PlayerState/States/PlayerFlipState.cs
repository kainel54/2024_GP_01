using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlipState : PlayerState
{
    private Vector2 _moveDir;
    public PlayerFlipState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnMoveEvent += HandleMovement;
    }

    public override void Exit()
    {
        _player.InputCompo.OnMoveEvent += HandleMovement;
        base.Exit(); 
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _player.MoveCompo.SetMovement(_moveDir);
        HandleJump();
        if (_player.finishedBoost)
        {
            _stateMachine.ChangeState(State.Move);
        }
    }

    private void HandleMovement(Vector2 vector)
    {
        _moveDir = vector;
    }
    private void HandleJump()
    {
        _player.jumpTimer += Time.deltaTime;
        float jumpHeight = Mathf.Clamp01(_player.jumpTimer / _player.jumpHoldTime);
        _player.verticalVelocity = _player.boostJumpPower * jumpHeight;
        if (_player.verticalVelocity >= _player.boostJumpPower)
            _stateMachine.ChangeState(State.Move);
    }
}
