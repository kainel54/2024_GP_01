using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine _stateMachine;
    protected Player _player;

    private int _animBoolHash;
    protected bool _endTriggerCalled; //애니메이션 종료신호 받기 위함.

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animBoolName);
    }

    // 상태로 들어오는 거
    public virtual void Enter()
    {
        _player.AnimCompo.SetBool(_animBoolHash, true); 
        _endTriggerCalled = false;
    }

    // 상태를 수행하는 거
    public virtual void UpdateState()
    {
        //현재로서 여기는 할게 없다.
    }

    // 상태를 나가는 거
    public virtual void Exit()
    {
        _player.AnimCompo.SetBool(_animBoolHash, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        _endTriggerCalled = true;
    }
}
