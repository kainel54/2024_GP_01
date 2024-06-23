using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine _stateMachine;
    protected Player _player;

    private int _animBoolHash;
    protected bool _endTriggerCalled; //�ִϸ��̼� �����ȣ �ޱ� ����.

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animBoolName)
    {
        _player = player;
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animBoolName);
    }

    // ���·� ������ ��
    public virtual void Enter()
    {
        _player.AnimCompo.SetBool(_animBoolHash, true); 
        _endTriggerCalled = false;
    }

    // ���¸� �����ϴ� ��
    public virtual void UpdateState()
    {
        //����μ� ����� �Ұ� ����.
    }

    // ���¸� ������ ��
    public virtual void Exit()
    {
        _player.AnimCompo.SetBool(_animBoolHash, false);
    }

    public virtual void AnimationFinishTrigger()
    {
        _endTriggerCalled = true;
    }
}
