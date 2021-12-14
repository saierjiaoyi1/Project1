using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ButtonRotate : MonoBehaviour
{


    public GameObject go;
    private float axisX;//�����ˮƽ�����ƶ�������
    //private float axisY;//�������ֱ�����ƶ�������
    //�����ƶ�
    private float scaleMin = 5f;
    public float scaleMax = 15f;


    private Vector3 startPos;//һ��ʼ����λ��
    private Vector3 nowPos;//����λ��
    private Vector3 latePos;//�ӳ�����λ��
    void Update()


    {
        //������ת


        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;//һ��ʼ������λ��
        }




        if (Input.GetMouseButton(0))
        {


            nowPos = Input.mousePosition;//������λ��


            //�ж�һ��ʼ����Ƿ��ƶ�
            if (nowPos != startPos)
            {
                //�����ڼ�����Ƿ��ƶ�
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
