using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputCompo.OnMoveEvent += HandleMovement;
        _player.InputCompo.OnFireEvent += HandleFire;
        _player.AnimCompo.SetFloat("InputMagnitude", 0, .1f, Time.deltaTime);
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
        float velocity = new Vector2(movement.x,movement.y).magnitude;

        if (velocity > inputThreshold)
        {
            _stateMachine.ChangeState(State.Move);
        }
        else
        {
            _player.AnimCompo.SetFloat("InputMagnitude", velocity * _player.currentAcceleration, .1f, Time.deltaTime);
        }
    }
    private void HandleFire(bool value)
    {
        if (value&&_player.arrowSystem.isCharging&&_player.isGrounded)
        {
            _stateMachine.ChangeState(State.ArrowIdle);
        }
        
    }
}
