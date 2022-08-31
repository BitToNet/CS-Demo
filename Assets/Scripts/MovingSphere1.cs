using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//刚体的情况下运动
public class MovingSphere1 : MonoBehaviour
{
    //最大速度
    [SerializeField, Range(0, 100f)]
    float maxSpeed = 10f;
    //最大加速度
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;

    //速度
    private Vector3 velocity;
    
    Rigidbody body;

    void Awake () {
        body = GetComponent<Rigidbody>();
    }

    private Vector3 desiredVelocity;
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);
        // Debug.Log("---------"+playerInput);
        desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        
    
       
    }

    private void FixedUpdate()
    {
        velocity = body.velocity;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        body.velocity = velocity;
    }
}
