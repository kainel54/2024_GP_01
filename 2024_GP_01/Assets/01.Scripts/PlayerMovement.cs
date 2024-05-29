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
    private Player _player;
    public Vector3 Velocity { get; private set; }
    private Vector2 _moveDir;

    

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        //CheckGround();
        Move();
        //CheckJump();
    }

    private void CheckGround()
    {
        _player.isGrounded = Physics.Raycast(transform.position + (transform.up * .05f), Vector3.down, .2f, _player.whatIsGround);
    }

    private void CheckJump()
    {
        if (_player.isGrounded)
        {
            float jumpHeight = 5f;
            _player.verticalVelocity = _player.jumpPower * jumpHeight;
            if (_player.verticalVelocity >= _player.jumpPower)
                _player.isJumping = false;
        }
        else
        {
            _player.verticalVelocity -= _player.gravity * Time.deltaTime;
        }

        _player.CharCompo.Move(Vector3.up * _player.verticalVelocity * Time.deltaTime);
    }


    private void Move()
    {
        _player.CharCompo.Move(Velocity);
    }

    public void SetMovement(Vector3 movement)
    {
        Vector3 foward = _player.CamCompo.transform.forward;
        Vector3 right = _player.CamCompo.transform.right;

        foward.y = 0;
        right.y = 0;

        foward.Normalize();
        right.Normalize();

        Velocity = movement * Time.fixedDeltaTime;
        Velocity = foward * _player.InputCompo.Movement.y + right * _player.InputCompo.Movement.x;
    }
    public void SetRuningAngles()
    {
        transform.eulerAngles += new Vector3(0, _player.InputCompo.Movement.x * Time.deltaTime * _player.rotationSpeed, 0);
    }
}
