using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGun : MonoBehaviour
{
    public float turnSpd;
    
    private Rigidbody2D gunRB;//총 움직임
    private Vector2 mousePos;//마우스 값 받아옴
    private float angle;//마우스 각
    int modeSet = 0;
    
    private float scaleX;
    private float scaleY;
    private float scaleZ;
    SpriteRenderer rend;
    Rigidbody2D rigid;
    
    void start()
    {
        gunRB = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x); 

        if(angle < 1.5 && angle > -1.5)
        {
            angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x); 
            transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), turnSpd * Time.deltaTime);
        }
        else 
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), turnSpd * Time.deltaTime);
        }
        
        if(Input.GetKeyDown(KeyCode.Q)) modeSet = 1;
        else if(Input.GetKeyDown(KeyCode.E)) modeSet = 2;
        else if(Input.GetKeyDown(KeyCode.R)) modeSet = 3;
        else if(Input.GetKeyDown(KeyCode.LeftShift)) modeSet = 0;

        if(modeSet == 0){//기본공격
            if(Input.GetMouseButtonDown(0))
            {
                GameObject Bullet = Instantiate(Resources.Load<GameObject>("Prefab/Bullet"), transform.position, transform.rotation);
                Bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Bullet.GetComponent<MBullet>().bulletSpd;
            }
        }
        else if(modeSet == 1){
            //가로베기
            if(Input.GetMouseButtonDown(0))
            {
                GameObject Ray = Instantiate(Resources.Load<GameObject>("Prefab/Knife"), transform.position, transform.rotation);
            }
        }
        else if(modeSet == 2){
            //전기장
            if(Input.GetMouseButtonDown(0))
            {
                GameObject Ray = Instantiate(Resources.Load<GameObject>("Prefab/Electric"), transform.position, transform.rotation);
            }
        }
        else if(modeSet == 3){
            //레일건
            if(Input.GetMouseButtonDown(0))
            {
                GameObject Ray = Instantiate(Resources.Load<GameObject>("Prefab/Ray"), transform.position, transform.rotation);
            }
        }
        
    }
}
