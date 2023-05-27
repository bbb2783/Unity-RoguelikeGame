using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGun : MonoBehaviour
{
    public float turnSpd;
    
    private Rigidbody2D gunRB;//총 움직임
    private Vector2 mousePos;//마우스 값 받아옴
    private float angle;//마우스 각
    
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
            rend.flipX = true;
            rend.flipY = true;
            angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x); 
            transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), turnSpd * Time.deltaTime);
        }
        else 
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,
            Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward), turnSpd * Time.deltaTime);
        }
        
        if(Input.GetMouseButtonDown(0))
        {
            GameObject Bullet = Instantiate(Resources.Load<GameObject>("Prefab/Bullet"), transform.position, transform.rotation);
            Bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Bullet.GetComponent<MBullet>().bulletSpd;
        }
    }
}
