using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//刚体的情况下运动，+跳跃
public class MovingSphere1 : MonoBehaviour
{
    //最大速度
    [SerializeField, Range(0, 100f)]
    float maxSpeed = 10f;
    //最大加速度
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;
    //跳跃高度
    [SerializeField, Range(0f, 10f)]
    float jumpHeight = 2f;

    //速度
    private Vector3 velocity;
    
    bool onGround;
    
    Rigidbody body;

    void Awake () {
        body = GetComponent<Rigidbody>();
    }

    private Vector3 desiredVelocity;
    //跳跃
    bool desiredJump;
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        desiredJump |= Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        velocity = body.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        if (desiredJump) {
            desiredJump = false;
            Jump();
        }
        body.velocity = velocity;
        onGround = false;
    }
    
    void Jump() {
        if (onGround) {
            velocity.y += Mathf.Sqrt(-2f * Physics.gravity.y * jumpHeight);
        }
    }
    
    void OnCollisionEnter () {
        onGround = true;
    }

    void OnCollisionStay () {
        onGround = true;
    }
}
