using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSelf : MonoBehaviour
{
    //旋转速度
    public float speed = 90;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //顺时针
        transform.Rotate(new Vector3(0, 0, -speed * Time.deltaTime));
    }
}
