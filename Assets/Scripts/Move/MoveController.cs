using UnityEngine;
using System.Collections;
using System;

public class MoveController : MonoBehaviour {


    public JoyStick joystick;

    bool isRun;
    
    //最大速度
    [SerializeField, Range(0, 100f)]
    float maxSpeed = 10f;
    //最大加速度
    [SerializeField, Range(0f, 100f)]
    float maxAcceleration = 10f;
    float currentAcceleration = 10f;
    //速度
    private Vector3 velocity;
    
    float h, v;

    Vector3 moveVec;

    // Use this for initialization
    void Start () {
        //joystick.onJoystickDownEvent += OnJoystickDownEvent;
        joystick.onJoystickUpEvent += OnJoystickUpEvent;
        joystick.onJoystickDragEvent += OnJoystickDragEvent;
        //joystick.onJoystickDragEndEvent += OnJoystickDragEndEvent;
    }


    void OnDestroy()
    {
        //joystick.onJoystickDownEvent -= OnJoystickDownEvent;
        joystick.onJoystickUpEvent -= OnJoystickUpEvent;
        joystick.onJoystickDragEvent -= OnJoystickDragEvent;
        //joystick.onJoystickDragEndEvent -= OnJoystickDragEndEvent;
    }

    private void OnJoystickUpEvent()
    {
        //停止移动
        isRun = false;
        h = 0;
        v = 0;

        moveVec = new Vector3(h, 0, v).normalized;
    }

    /// <summary>
    /// 按下
    /// </summary>
    /// <param name="obj"></param>
    private void OnJoystickDownEvent(Vector2 obj)
    {
        //停止移动
        isRun = false;
        h = 0;
        v = 0;

        moveVec = new Vector3(h, 0, v).normalized;
    }

    /// <summary>
    /// 传入一个方向 向量
    /// </summary>
    /// <param name="obj"></param>
    private void OnJoystickDragEvent(Vector2 obj)
    {
        //开始移动
        isRun = true;
        h = obj.x;
        v = obj.y;

        moveVec = new Vector3(h, 0, v).normalized;
    }

    /// <summary>
    /// 拖动结束
    /// </summary>
    /// <param name="obj"></param>
    private void OnJoystickDragEndEvent(Vector2 obj)
    {
       
    }

    // Update is called once per frame
    void Update () {
        if ((h != 0 || v != 0))
        {
            currentAcceleration = maxAcceleration;
        }else
        {
            currentAcceleration = 10000000000000f;
        }
        Vector3 desiredVelocity = new Vector3(h, 0f, v) * maxSpeed;
        float maxSpeedChange = currentAcceleration * Time.deltaTime;
        velocity.x =
            Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);
        velocity.z =
            Mathf.MoveTowards(velocity.z, desiredVelocity.z, maxSpeedChange);
        Vector3 displacement = velocity * Time.deltaTime;
        Vector3 newPosition = transform.localPosition + displacement;
        transform.localPosition = newPosition;
     
        
        if ( isRun && (h != 0 || v != 0) )
        {
            
            // 根据摄像机方向 进行移动 和摄像机保持相对平行视角
            //moveVec = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0) * moveVec;
            // nav.Move(moveVec * Time.deltaTime * 5);
            RotatePlayer();
        }
    }

    private void RotatePlayer()
    {
        //向量v围绕y轴旋转cameraAngle.y度
        //向量旋转到正前方
        //Vector3 vec = Quaternion.Euler(0, 0, 0) * moveVec;
        Vector3 vec =  moveVec;
        Debug.Log(vec.ToString());
        if (vec == Vector3.zero)
            return;
        //人物看向那个方向
        Quaternion look = Quaternion.LookRotation(vec);
        transform.rotation = Quaternion.Lerp(transform.rotation, look, Time.deltaTime * 100);
    }
}
