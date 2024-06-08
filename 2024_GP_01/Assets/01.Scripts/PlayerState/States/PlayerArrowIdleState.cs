using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowIdleState : PlayerState
{
    public PlayerArrowIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnFireEvent += HandleFire;
        _player.InputCompo.OnMoveEvent += HandleMove;
    }


    public override void Exit()
    {
        _player.InputCompo.OnFireEvent += HandleFire;
        _player.InputCompo.OnMoveEvent -= HandleMove;

        base.Exit();
    }
    private void HandleMove(Vector2 vector)
    {
        float inputThreshold = 0.05f;
        float velocity = new Vector2(vector.x, vector.y).magnitude;

        if (velocity > inputThreshold)
        {
            _stateMachine.ChangeState(State.ArrowMovement);
        }
        else
        {
            _player.AnimCompo.SetFloat("InputMagnitude", velocity * _player.currentAcceleration, .1f, Time.deltaTime);
        }
    }
    private void HandleFire(bool value)
    {
        if (value == false)
        {
            _stateMachine.ChangeState(State.Idle);
        }
    }
}
