using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class OrbitCamera : MonoBehaviour
{
    //被跟踪物体
    [SerializeField] Transform focus = default;

    //追踪半径
    [SerializeField, Min(0f)] float focusRadius = 1f;

    //相机离焦点距离
    [SerializeField, Range(1f, 20f)] float distance = 5f;

    //焦点居中
    [SerializeField, Range(0f, 1f)] float focusCentering = 0.5f;

    //旋转速度
    [SerializeField, Range(1f, 360f)] float rotationSpeed = 90f;

    //视觉角度
    [SerializeField, Range(-89f, 89f)] float minVerticalAngle = -30f, maxVerticalAngle = 60f;

    Vector3 focusPoint;
    Vector2 orbitAngles = new Vector2(45f, 0f);

    //滑动冲突
    public JoyStick joystick;
    private bool isTouching = false;


    void Awake()
    {
        focusPoint = focus.position;
    }

    private void Start()
    {
        joystick.onJoystickDownEvent += OnJoystickDownEvent;
        joystick.onJoystickUpEvent += OnJoystickUpEvent;
        joystick.onJoystickDragEvent += OnJoystickDragEvent;
        joystick.onJoystickDragEndEvent += OnJoystickDragEndEvent;
    }

    void LateUpdate()
    {
        // 根据聚焦半径更新目标位置 focusPoint
        UpdateFocusPoint();
        Quaternion lookRotation;// 四元数转化，代表三维空间每个角转化的角度 Quaternion.Euler(0,90,90)
        if (ManualRotation()) {
            ConstrainAngles();
            lookRotation = Quaternion.Euler(orbitAngles);
        }
        else {
            // 原始向量
            lookRotation = transform.localRotation;
        }
        Vector3 lookDirection = lookRotation * Vector3.forward;
        Vector3 lookPosition = focusPoint - lookDirection * distance;
        transform.SetPositionAndRotation(lookPosition, lookRotation);
    }

    void OnValidate()
    {
        if (maxVerticalAngle < minVerticalAngle)
        {
            maxVerticalAngle = minVerticalAngle;
        }
    }

    void ConstrainAngles()
    {
        orbitAngles.x =
            Mathf.Clamp(orbitAngles.x, minVerticalAngle, maxVerticalAngle);

        if (orbitAngles.y < 0f)
        {
            orbitAngles.y += 360f;
        }
        else if (orbitAngles.y >= 360f)
        {
            orbitAngles.y -= 360f;
        }
    }

    bool ManualRotation()
    {
        Vector2 input = new Vector2(
            Input.GetAxis("Vertical Camera"),
            Input.GetAxis("Horizontal Camera")
        );
        const float e = 0.001f;
        if (input.x < e || input.x > e || input.y < e || input.y > e)
        {
            orbitAngles += rotationSpeed * Time.unscaledDeltaTime * input;
            return true;
        }

        return false;
    }

    void UpdateFocusPoint()
    {
        //目标位置
        Vector3 targetPoint = focus.position;
        if (focusRadius > 0f)
        {
            float distance = Vector3.Distance(targetPoint, focusPoint);
            float t = 1f;
            if (distance > 0.01f && focusCentering > 0f)
            {
                t = Mathf.Pow(1f - focusCentering, Time.unscaledDeltaTime);
            }

            if (distance > focusRadius)
            {
                t = Mathf.Min(t, focusRadius / distance);
            }

            focusPoint = Vector3.Lerp(targetPoint, focusPoint, t);
        }
        else
        {
            focusPoint = targetPoint;
        }
    }

    void OnDestroy()
    {
        joystick.onJoystickDownEvent -= OnJoystickDownEvent;
        joystick.onJoystickUpEvent -= OnJoystickUpEvent;
        joystick.onJoystickDragEvent -= OnJoystickDragEvent;
        joystick.onJoystickDragEndEvent -= OnJoystickDragEndEvent;
    }

    private void OnJoystickDownEvent(Vector2 obj)
    {
        isTouching = true;
    }

    private void OnJoystickUpEvent()
    {
        isTouching = false;
    }

    private void OnJoystickDragEvent(Vector2 obj)
    {
        isTouching = true;
    }


    private void OnJoystickDragEndEvent(Vector2 obj)
    {
        isTouching = false;
    }
}