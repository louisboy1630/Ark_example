using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sword : MonoBehaviour
{
    public float radian = 0;// 紀錄數值
    public float perRadian = 0.01f; // 變化數度
    public float radius = 0.4f; // 變化距離
    //Vector3 oldPosition; // 初始位置

    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        //oldPosition = transform.position; //紀錄初始位置
    }

    // Update is called once per frame
    void Update()
    {
        radian += perRadian;
        float dy = Mathf.Cos(radian) / 5;
        transform.position = new Vector3(transform.position.x, player.position.y + 1.35f + dy, transform.position.z);
    }
}
