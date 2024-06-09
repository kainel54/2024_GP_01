using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowRunningState : PlayerState
{
    private Vector2 _movementDir;
    public PlayerArrowRunningState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnMoveEvent += HandleMovement;
    }


    public override void Exit()
    {
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
            _stateMachine.ChangeState(State.Move);
        }
    }
}
