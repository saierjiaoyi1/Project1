﻿using UnityEngine;

public class HumainMove : MonoBehaviour
{
    public CharacterController cc;

    public float speed
    {
        get
        {
            if (_shiftKeyDown)
                return runSpeed;

            return walkSpeed;
        }
    }
    public float gravity = 4;

    private float _dropSpeed;
    public float walkSpeed = 3;
    public float runSpeed = 5;
    public HumanAnimationController animationController;

    private Vector2 _moveDirection;

    bool _upKeyDown;
    bool _downKeyDown;
    bool _rightKeyDown;
    bool _leftKeyDown;
    bool _shiftKeyDown;
    public Transform rotatePart;

    private void Start()
    {
        cc.detectCollisions = true;
    }

    void Update()
    {
        _moveDirection = new Vector2(0, 0);

        if (GameSystem.instance.state == GameSystem.GameState.Playing)
        {
            ReadKeyDown();
            ReadKeyUp();
            SetKeyBoolValue();
        }

        Move();

        if (!cc.isGrounded)
        { Drop(); }
        else { _dropSpeed = 0; }
    }

    private void ReadKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)|| Input.GetKeyDown(KeyCode.RightShift))
        {
            _shiftKeyDown = true;
            animationController.startAcc = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _rightKeyDown = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _leftKeyDown = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _upKeyDown = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _downKeyDown = true;
        }
    }

    private void ReadKeyUp()
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)|| Input.GetKeyUp(KeyCode.RightShift))
        {
            _shiftKeyDown = false;
            animationController.stopAcc = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _rightKeyDown = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _leftKeyDown = false;
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            _upKeyDown = false;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            _downKeyDown = false;
        }
    }

    private void SetKeyBoolValue()
    {
        if (_rightKeyDown)
        {
            _moveDirection.x += 1;
        }
        if (_leftKeyDown)
        {
            _moveDirection.x -= 1;
        }
        if (_upKeyDown)
        {
            _moveDirection.y += 1;
        }
        if (_downKeyDown)
        {
            _moveDirection.y -= 1;
        }
    }

    private void Move()
    {
        if (Mathf.Approximately(_moveDirection.magnitude, 0))
        {
            Stop();
            return;
        }

        Vector3 moveDir = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        moveDir = moveDir.normalized * speed * Time.deltaTime;

        cc.Move(moveDir);
        animationController.startWalk = true;
     
        //TurnToDirection
        var rot = Quaternion.LookRotation(moveDir);
        rotatePart.localEulerAngles = new Vector3(0, rot.eulerAngles.y + 90, 0);
    }

    public void ResetMove()
    {
        Stop();
        _dropSpeed = 0;
        _shiftKeyDown = false;

        animationController.doPick = false;
        animationController.doCatch = false;
        animationController.doStandup = false;
        animationController.startWalk = false;
        animationController.stopWalk = false;
        animationController.startAcc = false;
        animationController.stopAcc = false;
    }

    void Stop()
    {
        animationController.stopWalk = true;
    }

    void Drop()
    {
        _dropSpeed += gravity * Time.deltaTime;
        cc.Move(Vector3.down * Time.deltaTime * _dropSpeed);
    }
}
