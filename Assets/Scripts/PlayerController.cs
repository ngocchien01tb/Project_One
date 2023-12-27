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
    private bool _isGrounded;
    private bool _canJump;
    private float _movementInputDirection;
    private int _amountOfJumpLeft;

    public int amountOfJump = 1;
    public float movementSpeed = 10.0f;
    public float jumpForce = 16.0f;
    public float groundCheckRadius;

    public Transform groundCheck;

    public LayerMask whatIsGround;
    
    

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int IsGrounded = Animator.StringToHash("isGrounded");

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _amountOfJumpLeft = amountOfJump;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckIfCanJump();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }
    private void CheckSurroundings()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    private void CheckIfCanJump()
    {
        if (_isGrounded && _rb.velocity.y<=0)
        {
            _amountOfJumpLeft = amountOfJump;
        }

        _canJump = _amountOfJumpLeft > 0;
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

        if (_rb.velocity.x !=0)
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
        _anim.SetBool(IsGrounded,_isGrounded);
        _anim.SetFloat(YVelocity,_rb.velocity.y);
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
        if (_canJump)
        {
             _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
             _amountOfJumpLeft --;
        }
       
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
