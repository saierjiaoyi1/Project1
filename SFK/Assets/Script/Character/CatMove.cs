using UnityEngine;
using System.Collections;

public class CatMove : MonoBehaviour
{
    public CharacterController cc;

    public float speed = 4;
    public float gravity = 4;

    private float _dropSpeed;

    public CatAnimationController animationController;

    private Vector2 _moveDirection;

    void Update()
    {
        Move();
        if (!cc.isGrounded)
        { Drop(); }
        else { _dropSpeed = 0; }
    }


    private void Move()
    {
        if (_moveDirection.magnitude == 0)
        {
            Stop();
            return;
        }

        Vector3 moveDir = new Vector3(_moveDirection.x, 0, _moveDirection.y);
        moveDir = moveDir.normalized * speed * Time.deltaTime;


        cc.Move(moveDir);
        animationController.startWalk = true;

        //TurnToDirection();
        var rot = Quaternion.LookRotation(moveDir);
        transform.localEulerAngles = new Vector3(0, rot.eulerAngles.y, 0);
    }

    void Stop()
    {
        animationController.stopWalk = true;
    }

    void Jump()
    {
        animationController.doJump = true;
    }

    void Drop()
    {
        _dropSpeed += gravity * Time.deltaTime;
        cc.Move(Vector3.down * Time.deltaTime * _dropSpeed);
    }

}
