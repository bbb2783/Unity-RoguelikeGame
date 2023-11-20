using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpriteSet : MonoBehaviour
{
    public float turnSpd;
    private Vector2 mousePos;//마우스 값 받아옴
    private float angle;//마우스 각

    Rigidbody2D rigid;
    SpriteRenderer GunAndArm;

    void Start()
    {
        GunAndArm = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        angle = Mathf.Atan2(mousePos.y - transform.position.y, mousePos.x - transform.position.x); 

        if(angle < 1.5 && angle > -1.5)
        {
            GunAndArm.color = new Color(1,1,1,1f);
        }
            
        else 
        {
            GunAndArm.color = new Color(1,1,1,0f);
        }
    }
}
