using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBullet : MonoBehaviour
{
    public float bulletSpd;
    public float BDamage;
    //public int per;

    private float BulletTimer;
    void Start()
    {
        BulletTimer = Time.time;
    }

    void Update()
    {
        if (Time.time - BulletTimer >= 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Monster"))
        {
            Destroy(gameObject);
        }
        if (collision.tag.Equals("wall")||collision.tag.Equals("FieldObject"))
        {
            Destroy(gameObject);
        }
    }

    public void Init(float BDamage)//대미지 초기화
    {
        this.BDamage = BDamage;
    }
}
