using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class PlayerController : MonoBehaviour
{

    private bool _isFacingRight = true;

    private Rigidbody2D _rb;
    private Animator _anim;
    private bool _isWalking;

    private float _movementInputDirection;

    public float movementSpeed = 10.0f;

    public float jumpForce = 16.0f;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
    }

    private void CheckMovementDirection()
    {
        switch (_isFacingRight)
        {
            case true when _movementInputDirection < 0:
            case false when _movementInputDirection >0:
                Flip();
                break;
        }

        if (_rb.velocity.y !=0)
        {
            _isWalking = true;
        }
        else
        {
            _isWalking = false;
        }
    }
    //Animtion------------------------------------

    private void UpdateAnimations()
    {
        
        _anim.SetBool(IsWalking,_isWalking);
    }

    private void CheckInput()
    {
        _movementInputDirection = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    //Jum-------------------------------------
    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }

    private void ApplyMovement()
    {
        _rb.velocity = new Vector2(movementSpeed * _movementInputDirection, _rb.velocity.y);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.Rotate(0.0f,180.0f,0.0f);
    }
}
