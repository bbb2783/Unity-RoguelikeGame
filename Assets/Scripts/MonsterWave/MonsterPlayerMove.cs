using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPlayerMove : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    private Vector2 mousePos;//마우스 값 받아옴
    private Animator anim; //for 애니메이션 제어

    private float scaleX;
    private float scaleY;
    private float scaleZ;

    Rigidbody2D rigid;

    void start()
    {
        //anim = GetComponent<Animator>();
    }

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

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePos.x < rigid.position.x)
        {
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
        else transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
        
        if (inputVec.x != 0 || inputVec.y != 0/*Input.GetKeyDown(KeyCode.W)*/)
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

    


    void OnCollisionStay2D(Collision2D collision)//플레이어 체력관리
    {
        if(!MGameManager.instance.isLive) return;
        
        if (collision.gameObject.name == "ForestZombieG(Clone)"||collision.gameObject.name == "ForestZombieR(Clone)") 
        {
            MGameManager.instance.playerHealth -= Time.deltaTime * 20;    
        }

        if(MGameManager.instance.playerHealth < 0)
        {
            for(int index=2; index<transform.childCount; index++){
                transform.GetChild(index).gameObject.SetActive(false);
            }
        }

        //아이템 상호작용
        if (collision.gameObject.name == "Hill(Clone)") {
    
            collision.gameObject.SetActive(false);	//Hill Object 비활성화            
            if(MGameManager.instance.playerHealth <= 150)
            {
                MGameManager.instance.playerHealth += 50;
            }
            else
            {
                MGameManager.instance.playerHealth = 200;
            }
            
        }
        if (collision.gameObject.name == "Booster(Clone)") {
            collision.gameObject.SetActive(false);	//Hill Object 비활성화
            /*
            	상호작용
            */
        }
    }
}
