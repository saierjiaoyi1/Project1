using UnityEngine;

public class HumainMove : MonoBehaviour
{
    public CharacterController cc;
    public float speed = 4;
    public float gravity = 4;
    private float _dropSpeed;
    public CatAnimationController catAnimationController;

    private bool _isMoveRight;
    private bool _isMoveLeft;

    private void Start()
    {
        cc.detectCollisions = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _isMoveRight = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _isMoveLeft = true;
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            _isMoveRight = false;
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _isMoveLeft = false;
        }
        Move();

        if (!cc.isGrounded)
        {
            Drop();
        }
        else
        {
            _dropSpeed = 0;
        }
    }

    private void Move()
    {
        if (_isMoveRight && _isMoveLeft)
        {
            Stop();
        }
        else
        {
            if (_isMoveLeft)
            {
                cc.Move(Vector3.left * Time.deltaTime * speed);
                catAnimationController.startWalk = true;
                TurnLeft();
            }
            else if (_isMoveRight)
            {
                cc.Move(Vector3.right * Time.deltaTime * speed);
                catAnimationController.startWalk = true;
                TurnRight();
            }
            else
            {
                Stop();
            }
        }
    }

    void Stop()
    {
        catAnimationController.stopWalk = true;
    }

    void Jump()
    {
        catAnimationController.doJump = true;
    }

    void Drop()
    {
        _dropSpeed += gravity * Time.deltaTime;
        cc.Move(Vector3.down * Time.deltaTime * _dropSpeed);
    }

    void TurnRight()
    {
        transform.localEulerAngles = new Vector3(0, 90, 0);
    }

    void TurnLeft()
    {
        transform.localEulerAngles = new Vector3(0, 270, 0);
    }
}
