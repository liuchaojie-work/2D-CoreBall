using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    //针的飞行速度
    private float speed = 15.0f;
    //是否飞行目标
    private bool isFly = false;
    //是否到达发射点
    private bool isReach = false;
    //发射位置
    private Transform startPoint;
    //圆的位置
    private Transform circle;

    private Vector3 targetCirclePos;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = GameObject.Find("StartPoint").transform;
        circle = GameObject.Find("Circle").transform;
        targetCirclePos = circle.position;
        targetCirclePos.y -= 1.60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isFly == false)
        {
            if(isReach == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPoint.position, speed * Time.deltaTime);
                if(Vector3.Distance(transform.position,startPoint.position)<0.05f)
                {
                    isReach = true;
                }
            }
            
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetCirclePos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetCirclePos) < 0.05f)
            {
                transform.position = targetCirclePos;
                transform.parent = circle;
                isFly = false;
            }
        }
    }

    public void StartFly()
    {
        if(isReach)
        {
            isFly = true;
        }    
    }
}
