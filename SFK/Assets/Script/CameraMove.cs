using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    public float speed;

    public float endValue;
    public float duration;
    public CatAnimationController catAnimationController;
    public Camera cam;

    private void Start()
    {
        cam.DOOrthoSize(endValue, duration).SetEase(Ease.InOutCubic).OnComplete(Hello);
    }

    void Hello()
    {
        Debug.Log("Hello");
        catAnimationController.doEat = true;
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

        Move(dir);
    }


    private void Move(Vector3 direction)
    {
        transform.position += direction * Time.deltaTime * speed;
    }
}
