using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通过控制位置移动，在不使用刚体的情况下
public class MovingSphere2 : MonoBehaviour
{
    //最大速度
    [SerializeField, Range(0, 100f)]
    float maxSpeed = 10f;
    //最大加速度
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;
    //可移动范围
    [SerializeField]
    Rect allowedArea = new Rect(-5f, -5f, 10f, 10f);
    //弹性
    [SerializeField, Range(0f, 1f)]
    float bounciness = 0.5f;

    //速度
    private Vector3 velocity;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        Vector3 desiredVelocity = new Vector3(playerInput.x, 0f, playerInput.y) * maxSpeed;
        float maxSpeedChange = maxAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;
        if (newPosition.x < allowedArea.xMin) {
            newPosition.x = allowedArea.xMin;
            velocity.x = -velocity.x * bounciness;
        }
        else if (newPosition.x > allowedArea.xMax) {
            newPosition.x = allowedArea.xMax;
            velocity.x = -velocity.x * bounciness;
        }
        if (newPosition.z < allowedArea.yMin) {
            newPosition.z = allowedArea.yMin;
            velocity.z = -velocity.z * bounciness;
        }
        else if (newPosition.z > allowedArea.yMax) {
            newPosition.z = allowedArea.yMax;
            velocity.z = -velocity.z * bounciness;
        }
        transform.localPosition = newPosition;
    }
}
