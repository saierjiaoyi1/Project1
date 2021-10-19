﻿using UnityEngine;
using DG.Tweening;

public class CameraMove : MonoBehaviour
{
    public float speed;
    public float startOrthoSize;
    public float endOrthoSize;
    public float duration;

    public Camera cam;
    public Transform camStartPos;
    public Transform camEndPos;

    private void Start()
    {
        cam.transform.position = camStartPos.position;
        cam.transform.eulerAngles = camStartPos.eulerAngles;
        cam.orthographicSize = startOrthoSize;

        cam.DOOrthoSize(endOrthoSize, duration).SetEase(Ease.InOutCubic).OnComplete(CameraMoveDone);
        cam.transform.DOMove(camEndPos.position, duration);
        cam.transform.DORotate(new Vector3(10, 0, 0), duration);
    }

    void CameraMoveDone()
    {
        Debug.Log("CameraMoveDone");
        EnableGameplayController();
        CatFlee();
    }

    void EnableGameplayController()
    {
        GameSystem.instance.state = GameSystem.GameState.Playing;
    }

    void CatFlee()
    {
        //cat go out

        //play sound
        Cat.cats[0].Test();
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
        if (GameSystem.instance.state == GameSystem.GameState.Playing)
        {
            transform.position += direction * Time.deltaTime * speed;
        }
    }
}
