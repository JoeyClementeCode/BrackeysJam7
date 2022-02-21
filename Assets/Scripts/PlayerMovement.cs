using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpForce;
    private float _jump;
    private bool _shouldJump;
    private bool _isGrounded;
    private Rigidbody2D _rb;
    private float _xMovement;
    [SerializeField] private float _detectionRadius;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private bool _airControl;


    
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _xMovement = Input.GetAxis("Horizontal");
        if(Input.GetButtonDown("Jump"))
        {
            _shouldJump = true;
        }
        
        
        
        

        _jump = _shouldJump&&_isGrounded?  _jumpForce : 0;

    }

    

    private void FixedUpdate()
    {   
        _isGrounded = false;
        
        var detectionCircle = Physics2D.OverlapCircleAll(_groundCheck.position, _detectionRadius, _layerMask);
                
        foreach (var col in detectionCircle)
        {
            if (col.gameObject != gameObject)
            {
                _isGrounded = true;
            }
        }

        if (_isGrounded || _airControl)
        {
            _rb.velocity = new Vector2(_xMovement * _moveSpeed, _rb.velocity.y + _jump);
        }

        _shouldJump = false;
    }
}
