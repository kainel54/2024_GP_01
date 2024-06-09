using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public UnityEvent OnMovementBoost;
    private Player _player;
    public Vector3 Velocity;
    private Vector2 _moveDir;
    private Coroutine boostCoroutine;


    private void Awake()
    {
        _player = GetComponent<Player>();

        _player.InputCompo.OnRunEvent += RunAction_performed;
        _player.InputCompo.OnRunEvent += RunAction_canceled;
        
        //_player.arrowSystem.OnTargetHit.AddListener(Boost);
    }

    private void OnDestroy()
    {
        _player.InputCompo.OnRunEvent -= RunAction_performed;
        _player.InputCompo.OnRunEvent -= RunAction_canceled;
    }

    private void Update()
    {
        CheckGround();
        
        if (!_player.isJumping)
        {
            _player.verticalVelocity -= _player.gravity * Time.deltaTime;
        }
        _player.CharCompo.Move(Vector3.up * _player.verticalVelocity * Time.deltaTime);
        
        float lerp = _player.finishedBoost ? _player.accelerateLerp : _player.decelerateLerp;
        _player.currentAcceleration = Mathf.Lerp(_player.currentAcceleration, _player.isRunning ? (_player.runAcceleration * _player.accelerationMultiplier) : 1, lerp * Time.deltaTime) ;
        
    }
    public void Boost()
    {
        if (!_player.holdRunInput)
            return;

        if (!_player.isRunning && _moveDir.magnitude <= 0)
            return;

        OnMovementBoost.Invoke();

        if (!_player.isGrounded)
            _player.StateMachine.ChangeState(State.Flip);

        _player.finishedBoost = false;

        if (boostCoroutine != null)
            StopCoroutine(boostCoroutine);

        boostCoroutine = StartCoroutine(BoostCoroutine());

        IEnumerator BoostCoroutine()
        {
            if (!_player.isGrounded)
                _player.isJumping = true;

            _player.isBoosting = true;
            _player.accelerationMultiplier = _player.boostMultiplier;

            yield return new WaitForSeconds(_player.boostDuration);

            _player.isBoosting = false;
            _player.accelerationMultiplier = 1;
            _player.finishedBoost = true;

            yield return new WaitForSeconds(1);

            _player.finishedBoost = false;
        }
    }

    private void CheckGround()
    {
        _player.isGrounded = Physics.Raycast(transform.position + (transform.up * .05f), Vector3.down, .3f, _player.whatIsGround);
        _player.AnimCompo.SetBool("isGrounded", _player.isGrounded);
        _player.AnimCompo.SetFloat("GroundedValue", _player.isGrounded ? 0 : 1, .1f, Time.deltaTime);
    }

    public void SetMovement(Vector2 movement)
    {
        _moveDir = movement;
        Vector3 forward = _player.CamCompo.transform.forward;
        Vector3 right = _player.CamCompo.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        if (_player.isRunning)
        {
            //Vector3 sideMovement = arrowSystem.isCharging ? transform.right * moveAxis.x : Vector3.zero;

            transform.eulerAngles += new Vector3(0, _moveDir.x * Time.deltaTime * _player.runRotationSpeed, 0);

            _player.CharCompo.Move(transform.forward * _player.moveSpeed * _player.currentAcceleration * Time.deltaTime);

            return;
        }

        Velocity = forward * _moveDir.y + right * _moveDir.x;

        if (_player.blockRotationPlayer == false)
        {
            //Camera
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Velocity), _player.rotationSpeed * _player.currentAcceleration);
            _player.CharCompo.Move(Velocity * Time.deltaTime * (_player.moveSpeed * _player.currentAcceleration));
        }
    }
    public void SetRuningAngles()
    {
        transform.eulerAngles += new Vector3(0, _player.InputCompo.Movement.x * Time.deltaTime * _player.rotationSpeed, 0);
    }

    private void RunAction_performed(bool value)
    {
        Debug.Log("RunInput");
        if (value)
        {
            _player.holdRunInput = true;

            if (canRun() && _moveDir.magnitude > 0)
                _player.isRunning = true;
        }
    }

    private bool canRun()
    {
        bool state;
        state = _player.boostSystem != null ? (_player.boostSystem.boostAmount > 0 ? true : false) : false;
        return state;
    }

    private void RunAction_canceled(bool value)
    {
        if (!value)
        {
            Debug.Log("RunOutPut");
            _player.isRunning = false;
            _player.holdRunInput = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(transform.position + (transform.up * .05f), Vector3.down);
    }
}
