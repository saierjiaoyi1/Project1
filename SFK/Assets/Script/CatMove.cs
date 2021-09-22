using UnityEngine;
using System.Collections;

public class CatMove : MonoBehaviour
{
    public float speed;
    public Transform cat;
    public CatAnimationController catAnimationController;

    private bool _isMoveRight;
    private bool _isMoveLeft;

    void Start()
    {

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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Jump();
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
                var newPos = cat.position;
                newPos += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
                cat.position = newPos;
                catAnimationController.startWalk = true;
                TurnLeft();
            }
            else if (_isMoveRight)
            {
                var newPos = cat.position;
                newPos += new Vector3(1, 0, 0) * speed * Time.deltaTime;
                cat.position = newPos;
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

    void TurnRight()
    {
        cat.localEulerAngles = new Vector3(0, 90, 0);
    }

    void TurnLeft()
    {
        cat.localEulerAngles = new Vector3(0, 270, 0);
    }
}
