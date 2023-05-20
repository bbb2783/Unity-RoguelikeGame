using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlayerMove : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;


    Rigidbody2D rigid;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);//위치
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(!MGameManager.instance.isLive) return;
        
        MGameManager.instance.playerHealth -= Time.deltaTime * 20;

        if(MGameManager.instance.playerHealth < 0)
        {
            for(int index=2; index<transform.childCount; index++){
                transform.GetChild(index).gameObject.SetActive(false);
            }
        }

        //아이템 상호작용
        if (collision.gameObject.name == "Hill(Clone)") {
    
            collision.gameObject.SetActive(false);	//Hill Object 비활성화            
            MGameManager.instance.playerHealth += 50;
        }
        if (collision.gameObject.name == "Booster(Clone)") {
            collision.gameObject.SetActive(false);	//Hill Object 비활성화
            /*
            	상호작용
            */
        }
    }
}
