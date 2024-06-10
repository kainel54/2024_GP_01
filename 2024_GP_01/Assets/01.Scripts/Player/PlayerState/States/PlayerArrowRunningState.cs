using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowRunningState : PlayerGroundState
{
    private Vector2 _movementDir;
    public PlayerArrowRunningState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnMoveEvent += HandleMovement;
        _player.InputCompo.OnFireEvent += HandleFire;
    }

    private void HandleFire(bool value)
    {
        if (!value)
        {
            _stateMachine.ChangeState(State.Move);
        }
    }


    public override void Exit()
    {
        _player.InputCompo.OnMoveEvent -= HandleMovement;
        _player.InputCompo.OnFireEvent -= HandleFire;

        base.Exit();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _player.MoveCompo.SetMovement(_movementDir);
    }

    private void HandleMovement(Vector2 vector)
    {
        _movementDir = vector;
        if (!_player.isRunning)
        {
            _stateMachine.ChangeState(State.ArrowMovement);
        }
    }
}
