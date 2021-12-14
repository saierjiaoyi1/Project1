using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonRotate : MonoBehaviour
{


    public GameObject go;
    private float axisX;//鼠标沿水平方向移动的增量
    //private float axisY;//鼠标沿竖直方向移动的增量
    //物体移动
    private float scaleMin = 5f;
    public float scaleMax = 15f;


    private Vector3 startPos;//一开始鼠标的位置
    private Vector3 nowPos;//鼠标的位置
    private Vector3 latePos;//延迟鼠标的位置
    void Update()


    {
        //物体旋转


        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;//一开始获得鼠标位置
        }




        if (Input.GetMouseButton(0))
        {


            nowPos = Input.mousePosition;//获得鼠标位置


            //判断一开始鼠标是否移动
            if (nowPos != startPos)
            {
                //按下期间鼠标是否移动
                if (nowPos != latePos)
                {
                    axisX = -(nowPos.x - startPos.x) * Time.deltaTime;
                    //axisY = (nowPos.y - startPos.y) * Time.deltaTime;
                }
                else
                {
                    axisX = 0;
                    //axisY = 0;
                }
            }
            else
            {
                axisX = 0;
                //axisY = 0;
            }
        }
        else
        {
            axisX = 0;
            //axisY = 0;
        }


        go.transform.Rotate(new Vector3(0, axisX, 0), Space.World);

    }
}
