using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 1f;

    void Update()
    {
        // 가로 이동 반환값 : LeftArrow = -1 RightArrow = 1
        var h = Input.GetAxisRaw("Horizontal");
        // 세로 이동 반환값 : DownArrow = -1 UpArrow = 1        
        var v = Input.GetAxisRaw("Vertical");

        //단위 벡터 (크기가 1인 벡터)
        var dir = new Vector3(h, v, 0).normalized;

        this.transform.Translate(dir * this.speed * Time.deltaTime);
    }
}
