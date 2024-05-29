using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum State
{
    Idle,
    Walk,
    //Run,
    //Jump,
    //ShootCharging,
    //Shoot
}

public class Player : MonoBehaviour
{
    [Header("Movement Setting")]
    public float moveSpeed;
    public float rotationSpeed = 0.1f;

    [Header("Acceleration Settings")]
    public float currentAcceleration = 1;
    public float runAcceleration = 2;
    public float accelerationMultiplier = 1f;
    public float boostMultiplier = 1.5f;
    public float boostDuration = 1;
    public float accelerateLerp = .006f;
    public float decelerateLerp = .05f;
    public float runRotationSpeed = 100;
    public float chargeStrafeSpeed = 15;

    [Header("JumpSetting")]
    public float jumpPower = 8;
    public float verticalVelocity;
    public float gravity = 9.8f;

    [Header("CollisionSetting")]
    public LayerMask whatIsGround;

    [Header("Booleans")]
    public bool isGrounded;
    public bool isJumping;
    public bool blockRotationPlayer;
    public bool isRunning;
    public bool isBoosting;
    public bool finishedBoost;
    public Animator AnimCompo { get; private set; }
    public Camera CamCompo { get; private set; }
    public InputSO InputCompo;
    public PlayerStateMachine StateMachine { get; private set; }
    public CharacterController CharCompo { get; private set; }
    public PlayerMovement MoveCompo { get; private set; }
    public bool CanStateChangeable { get; private set; } = true;

    private Vector3 _velocity;
    private void Awake()
    {
        Transform visualTrm = transform.Find("Visual");
        AnimCompo = visualTrm.GetComponent<Animator>();
        CharCompo = GetComponent<CharacterController>();
        MoveCompo = GetComponent<PlayerMovement>();
        CamCompo = Camera.main;

        StateMachine = new PlayerStateMachine();

        foreach (State stateEnum in Enum.GetValues(typeof(State)))
        {
            string typeName = stateEnum.ToString(); // Idle, Run, Fall

            try
            {
                Type t = Type.GetType($"Player{typeName}State");
                PlayerState state = Activator.CreateInstance(t, this, StateMachine, typeName) as PlayerState;

                StateMachine.AddState(stateEnum, state);
            }
            catch (Exception ex)
            {
                Debug.LogError($"{typeName} is loading error check Message");
                Debug.LogError(ex.Message);
            }

        }
    }
    private void Start()
    {
        StateMachine.Initialize(State.Idle,this);
    }

    private void Update()
    {
        StateMachine.CurrentState.UpdateState();
    }


}
