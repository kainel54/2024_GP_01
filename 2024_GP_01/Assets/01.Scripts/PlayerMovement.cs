using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    public UnityEvent OnMovementBoost;

    public Animator AnimCompo { get; private set; }
    [SerializeField] private InputSO _input;
    private Camera _cam;
    private CharacterController _charContorller;
    private Vector3 _desiredMoveDir;
    private Vector2 _moveDir;

    [Header("Movement Setting")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed = 0.1f;

    [Header("Acceleration Settings")]
    private float _currentAcceleration = 1;
    [SerializeField] private float _runAcceleration = 2;
    private float _accelerationMultiplier = 1f;
    [SerializeField] private float _boostMultiplier = 1.5f;
    [SerializeField] private float _boostDuration = 1;
    [SerializeField] private float _accelerateLerp = .006f;
    [SerializeField] private float _decelerateLerp = .05f;
    [SerializeField] private float _runRotationSpeed = 100;
    [SerializeField] private float _chargeStrafeSpeed = 15;

    [Header("JumpSetting")]
    [SerializeField] private float _jumpPower = 8;
    [SerializeField] private float _verticalVelocity;
    [SerializeField] private float gravity = 9.8f;

    [Header("CollisionSetting")]
    [SerializeField] private LayerMask _groundLayer;

    [Header("Booleans")]
    public bool _isGrounded;
    [SerializeField] private bool _isJumping;
    [SerializeField] private bool _blockRotationPlayer;
    public bool _isRunning;
    public bool _isBoosting;
    public bool _finishedBoost;


    private void Awake()
    {
        _cam = Camera.main;
        Transform virTrm = transform.Find("Visual");
        AnimCompo = virTrm.GetComponent<Animator>();
        _charContorller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        InputMagnitude();
    }


    private void InputMagnitude()
    {
        //Calculate the Input Magnitude
        float inputMagnitude = _input.Movement.sqrMagnitude;

        //Physically move player
        if (inputMagnitude > 0.1f || _isRunning)
        {
            //anim.SetFloat("InputMagnitude", (isRunning ? 1 : inputMagnitude) * currentAcceleration, .1f, Time.deltaTime);
            PlayerMoveAndRotation();
        }
        else
        {
            //anim.SetFloat("InputMagnitude", inputMagnitude * currentAcceleration, .1f, Time.deltaTime);
        }
    }

    private void PlayerMoveAndRotation()
    {
        Vector3 foward = _cam.transform.forward;
        Vector3 right = _cam.transform.right;

        foward.y = 0;
        right.y = 0;

        foward.Normalize();
        right.Normalize();

        if (_isRunning)
        {
            transform.eulerAngles += new Vector3(0, _input.Movement.x * Time.deltaTime * _rotationSpeed, 0);

            _charContorller.Move(transform.forward * _moveSpeed * _currentAcceleration * Time.deltaTime);

            return;
        }
        _desiredMoveDir = foward * _input.Movement.y + right * _input.Movement.x;

        if(_blockRotationPlayer == false)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_desiredMoveDir), _rotationSpeed * _currentAcceleration);
            _charContorller.Move(_desiredMoveDir * Time.deltaTime * (_moveSpeed * _currentAcceleration));
        }
    }
}
