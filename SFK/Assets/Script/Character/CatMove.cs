﻿using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.AI;

public class CatMove : MonoBehaviour
{
    public CharacterController cc;
    private NavMeshAgent _navMeshAgent;

    public float speedBase = 3;
    public float speedRun
    {
        get { return speedBase + 2.5f; }
    }

    public float gravity = 8;

    private float _dropSpeed;

    public CatAnimationController animationController;

    private Vector2 _moveDirection;

    private bool _isJumping = false;
    public Transform rotatePart;

    private void Awake()
    {
        cc.detectCollisions = true;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // if (GameSystem.instance.state == GameSystem.GameState.Playing)

        Move();
        if (!cc.isGrounded)
        { Drop(); }
        else { _dropSpeed = 0; }
    }


    private void Move()
    {
        if (Mathf.Approximately(_moveDirection.magnitude, 0))
        {
            Stop();
            return;
        }

        Vector3 moveDir = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        moveDir = moveDir.normalized * speedBase * Time.deltaTime;
        cc.Move(moveDir);
        animationController.startWalk = true;

        var rot = Quaternion.LookRotation(moveDir);
        //rotatePart.localEulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
        rotatePart.DOLocalRotate(new Vector3(0, rot.eulerAngles.y, 0), 0.5f);
    }

    void Stop()
    {
        animationController.stopWalk = true;
        if (cc.enabled)
            cc.Move(Vector3.zero);
    }

    void Jump(float jumpPower)
    {
        if (cc.isGrounded)
        {
            _isJumping = true;
            animationController.doJump = true;
            _dropSpeed = -jumpPower;
        }
    }

    void Drop()
    {
        _dropSpeed += gravity * Time.deltaTime;
        if (cc.enabled)
            cc.Move(Vector3.down * Time.deltaTime * _dropSpeed);

        if (cc.isGrounded && _dropSpeed >= 0)
        {
            _isJumping = false;
        }
    }

    public void ResetMove(float pSpeed)
    {
        Stop();
        speedBase = pSpeed;
        _navMeshAgent.speed = speedBase;
        _navMeshAgent.enabled = false;

        _dropSpeed = 0;

        animationController.doJump = false;
        animationController.doSound = false;
        animationController.doEat = false;
        animationController.startWalk = false;
        animationController.stopWalk = false;
    }

    public void Go(CatDestination dest)
    {
        //public bool useNavMeshAgent;
        //public Vector3 pos;
        //public bool isRun;
        //public bool isJump;
        //public bool isFall;
    }
}
