using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.AI;

public class CatMove : MonoBehaviour
{
    private Cat _cat;
    public CharacterController cc;
    private NavMeshAgent _navMeshAgent;

    public float speedBase = 3;
    public float speedRun
    {
        get { return speedBase + 2.9f; }
    }
    float speed
    {
        get
        {
            if (_dest != null && _dest.isRun)
            {
                return speedRun;
            }
            return speedBase;
        }
    }
    public float gravity = 8;

    private float _dropSpeed;

    public CatAnimationController animationController;

    private Vector2 _moveDirection;

    private Vector3 _jumpTargetPos;
    private Vector3 _jumpFromPos;
    private bool _isJumping = false;
    public float jumpPreTime = 0.5f;
    public float jumpTime = 0.35f;
    private float _jumpPreTimer = 0f;
    private float _jumpTimer = 0f;

    public Transform rotatePart;

    private void Awake()
    {
        _cat = GetComponent<Cat>();
        cc.detectCollisions = true;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_navMeshAgent.enabled)
        {
            CheckNavArrive();
            return;
        }
        if (_isJumping)
        {
            DoJump();
            return;
        }

        UpdateMoveDirection();
        Move();
        if (!cc.isGrounded)
        { Drop(); }
        else { _dropSpeed = 0; }
    }


    private void Move()
    {
        if (Mathf.Approximately(_moveDirection.magnitude, 0))
        {
            //_cat.cab.OnArrived();
            Stop();
            return;
        }

        Vector3 moveDir = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        moveDir = moveDir.normalized * speed * Time.deltaTime;
        cc.Move(moveDir);
        animationController.startWalk = true;

        var rot = Quaternion.LookRotation(moveDir);
        rotatePart.localEulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
        //rotatePart.DOLocalRotate(new Vector3(0, rot.eulerAngles.y, 0), 0.5f);
    }

    public void Stop()
    {
        animationController.stopWalk = true;
        if (_navMeshAgent.enabled)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.enabled = false;
        }

        if (cc.enabled)
            cc.Move(Vector3.zero);
    }

    void Drop()
    {
        _dropSpeed += gravity * Time.deltaTime;
        if (cc.enabled)
            cc.Move(Vector3.down * Time.deltaTime * _dropSpeed);

        // if (cc.isGrounded && _dropSpeed >= 0)
        //     _isJumping = false;
    }

    public void ResetMove(float pSpeed)
    {
        speedBase = pSpeed;
        _navMeshAgent.enabled = false;
        _isJumping = false;
        _dropSpeed = 0;
        _dest = null;
        animationController.doJump = false;
        animationController.doSound = false;
        animationController.doEat = false;
        animationController.startWalk = false;
        animationController.stopWalk = false;
        cc.enabled = true;
        Stop();
    }

    public void Jump(Vector3 targetPos)
    {
        _navMeshAgent.enabled = false;
        cc.enabled = false;
        _jumpTargetPos = targetPos;
        _jumpFromPos = transform.position;
        _isJumping = true;
        animationController.doJump = true;
        _jumpPreTimer = jumpPreTime;
        _jumpTimer = jumpTime;

        var deltaDir = _jumpTargetPos - _jumpFromPos;
        deltaDir.y = 0;
        var rot = Quaternion.LookRotation(deltaDir);
        rotatePart.DOLocalRotate(new Vector3(0, rot.eulerAngles.y, 0), 0.5f);
    }

    void DoJump()
    {
        if (_jumpPreTimer > 0)
        {
            _jumpPreTimer -= Time.deltaTime;
            if (_jumpPreTimer <= 0)
            {
                //Debug.Log("start jump");
            }
            return;
        }
        if (_jumpTimer > 0)
        {
            _jumpTimer -= Time.deltaTime;
            if (_jumpTimer <= 0)
            {
                _jumpTimer = 0;
            }
            var t = _jumpTimer / jumpTime;
            var pos = Vector3.Lerp(_jumpFromPos, _jumpTargetPos, 1 - t);
            //var jumpHeightAdd = 1.0f;
            //pos += jumpHeightAdd * Vector3.up * (0.5f - Mathf.Abs(0.5f - t));
            transform.position = pos;
            return;
        }

        EndJump();
    }

    public void EndJump()
    {
        var has = _cat.cab.CheckNextFollow();
        if (has)
        {
            return;
        }

        //Debug.Log("end jump");
        _cat.cab.SetStunned(false);
        cc.enabled = true;
        _isJumping = false;
    }

    private CatDestination _dest;
    public void Go(CatDestination dest)
    {
        _dest = dest;
        if (_dest == null)
        {
            Stop();
            return;
        }

        if (_dest.useNavMeshAgent)
        {
            _navMeshAgent.enabled = true;
            _navMeshAgent.speed = (_dest.isRun ? speedRun : speedBase);
            //_navMeshAgent.stoppingDistance = _dest.arriveDistance;
            _navMeshAgent.destination = _dest.pos;
            animationController.startWalk = true;
        }
        else
        {
            _navMeshAgent.enabled = false;
        }
        //public bool useNavMeshAgent;
        //public Vector3 pos;
        //public bool isRun;
        //public float arriveDistance;
    }

    void UpdateMoveDirection()
    {
        _moveDirection = Vector2.zero;
        if (GameSystem.instance.state != GameSystem.GameState.Playing)
            return;

        if (_dest == null)
            return;

        if (_dest.useNavMeshAgent)
            return;

        var delta = _dest.pos - transform.position;
        if (delta.magnitude<=0.15f)
        {
            _cat.cab.OnArrived();
        }
        else
        {
            delta.Normalize();
            _moveDirection = new Vector2(delta.x, delta.z);
        }
   
    }

    void CheckNavArrive()
    {
        if (_dest.arriveDistance > Vector3.Distance(transform.position, _navMeshAgent.destination))
        {
            _cat.cab.OnArrived();
            Stop();
        }
    }
}
