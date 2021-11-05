using UnityEngine;
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
        get { return speedBase + 2.7f; }
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

    private bool _isJumping = false;
    public Transform rotatePart;

    private void Awake()
    {
        cc.detectCollisions = true;
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (_navMeshAgent.enabled)
        {
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
            Stop();
            return;
        }

        Vector3 moveDir = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        moveDir = moveDir.normalized * speed * Time.deltaTime;
        cc.Move(moveDir);
        animationController.startWalk = true;

        var rot = Quaternion.LookRotation(moveDir);
        //rotatePart.localEulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
        rotatePart.DOLocalRotate(new Vector3(0, rot.eulerAngles.y, 0), 0.5f);
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
        _navMeshAgent.enabled = false;

        _dropSpeed = 0;
        _dest = null;
        animationController.doJump = false;
        animationController.doSound = false;
        animationController.doEat = false;
        animationController.startWalk = false;
        animationController.stopWalk = false;
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
        _moveDirection = new Vector2(delta.x, delta.z);
    }
}
