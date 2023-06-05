using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPlayerMove : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    private Animator anim; //for 애니메이션 제어

    private float scaleX;
    private float scaleY;
    private float scaleZ;

    Rigidbody2D rigid;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        if(inputVec.x > 0)
        {
            transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
        }
        else if(inputVec.x < 0) 
        {
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
        if (inputVec.x != 0 || inputVec.y != 0)
        {
            anim.SetBool("isWalk", true);
        }
        else {anim.SetBool("isWalk", false);}
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);//위치
    }
}

