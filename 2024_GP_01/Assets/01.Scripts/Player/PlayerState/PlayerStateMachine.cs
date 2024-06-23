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

    //맨처음 초기화 담당 매서드
    public void Initialize(State startState, Player player)
    { 
        _player = player;
        CurrentState = stateDictionary[startState];
        CurrentState.Enter();
    }

    //상태를 바꿔주는기능
    public void ChangeState(State newState)
    {
        if (_player.CanStateChangeable == false) return;

        CurrentState.Exit();
        CurrentState = stateDictionary[newState];
        CurrentState.Enter();
    }
    //상태를 추가하는 기능
    public void AddState(State stateEnum, PlayerState playerState)
    {
        stateDictionary.Add(stateEnum, playerState);
    }
}
