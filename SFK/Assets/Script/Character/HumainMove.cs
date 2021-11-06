using UnityEngine;
using DG.Tweening;

public class HumainMove : Ticker
{
    public CharacterController cc;
    public float vitaMax = 100;
    public float vitaReg = 5;
    public float vitaCost = 10f;
    public UnityEngine.UI.Slider bar;
    private float _vita;

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

    private float _forceNoControllerTimer;
    private System.Action _postNoControlAction;

    private void Start()
    {
        cc.detectCollisions = true;
    }

    protected override void Tick()
    {
        AddVita(vitaReg * TickTime);
    }

    void AddVita(float Delta)
    {
        _vita += Delta;
        _vita = Mathf.Clamp(_vita, 0, vitaMax);
        bar.value = _vita / vitaMax;
    }

    public void Caught()
    {
        animationController.doCatch = true;
        _forceNoControllerTimer = 2;
        _moveDirection = new Vector2(0, 0);
        _postNoControlAction = GameSystem.instance.CheckWin;
    }

    protected override void Update()
    {
        base.Update();
        //Debug.Log(transform.position.x);
        _moveDirection = new Vector2(0, 0);
        if (GameSystem.instance.state == GameSystem.GameState.Playing)
        {
            ReadKeyDown();
            ReadKeyUp();

            if (_forceNoControllerTimer > 0)
            {
                _forceNoControllerTimer -= Time.deltaTime;
                if (_forceNoControllerTimer <= 0)
                {
                    _postNoControlAction?.Invoke();
                    _postNoControlAction = null;
                    // Debug.Log("_postNoControlAction");
                }
                return;
            }
            else
            {
                SetKeyBoolValue();
            }
        }

        Move();
        if (!cc.isGrounded)
        { Drop(); }
        else { _dropSpeed = 0; }
    }

    private void ReadKeyDown()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
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
        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
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
        if (_shiftKeyDown)
        {
            AddVita(-vitaCost * Time.deltaTime);
        }
        if (_vita < 1)
        {
            _shiftKeyDown = false;
            animationController.stopAcc = true;
        }
        cc.Move(moveDir);
        animationController.startWalk = true;

        var rot = Quaternion.LookRotation(moveDir);
        rotatePart.DOLocalRotate(new Vector3(0, rot.eulerAngles.y, 0), 0.6f);
    }

    public void ResetMove()
    {
        AddVita(vitaMax);
        cc.enabled = false;
        _dropSpeed = 0;

        _upKeyDown = false;
        _downKeyDown = false;
        _rightKeyDown = false;
        _leftKeyDown = false;
        _shiftKeyDown = false;

        _forceNoControllerTimer = 0;

        animationController.doPick = false;
        animationController.doCatch = false;
        animationController.doStandup = false;
        animationController.startWalk = false;
        animationController.stopWalk = false;
        animationController.startAcc = false;
        animationController.stopAcc = false;
        //Stop();
        animationController.stopWalk = true;
    }

    void Stop()
    {
        cc.Move(Vector3.zero);
        animationController.stopWalk = true;
        //Debug.Log("Stop" + transform.position.x);
    }

    void Drop()
    {
        _dropSpeed += gravity * Time.deltaTime;
        cc.Move(Vector3.down * Time.deltaTime * _dropSpeed);
        //Debug.Log("Drop" + transform.position.x);
    }
}
