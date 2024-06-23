using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine {
    public PlayerState CurrentState { get; private set; }
    public Dictionary<State, PlayerState> stateDictionary;

    private Player _player;
    public PlayerStateMachine()
    {
        stateDictionary = new Dictionary<State, PlayerState>();
    }

    //��ó�� �ʱ�ȭ ��� �ż���
    public void Initialize(State startState, Player player)
    { 
        _player = player;
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }

    //���¸� �ٲ��ִ±��
    public void ChangeState(State newState)
    {
        if (_player.CanStateChangeable == false) return;

        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }
    //���¸� �߰��ϴ� ���
    public void AddState(State stateEnum, PlayerState playerState)
    {
        stateDictionary.Add(stateEnum, playerState);
    }
}
