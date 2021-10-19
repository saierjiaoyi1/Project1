using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    public float speed;

    public float endValue;
    public float duration;
    public CatAnimationController catAnimationController;
    public Camera cam;
    public Transform camStartPos;
    public Transform camEndPos;

    private void Start()
    {
        cam.DOOrthoSize(endValue, duration).SetEase(Ease.InOutCubic).OnComplete(CameraMoveDone);
        cam.transform.DOMove(camEndPos.position, duration);
    }

    void CameraMoveDone()
    {
        Debug.Log("CameraMoveDone");
        EnableGameplayController();
        CatFlee();
    }

    void EnableGameplayController()
    {

    }

    void CatFlee()
    {
        catAnimationController.doEat = true;
        //play sound
    }
    void Update()
    {
        var dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            dir += Vector3.up;
        }
        if (Input.GetKey(KeyCode.A))
        {
            dir += Vector3.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            dir += Vector3.down;
        }
        if (Input.GetKey(KeyCode.D))
        {
            dir += Vector3.right;
        }

        Move(dir.normalized);
    }


    private void Move(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * speed;
    }
}
