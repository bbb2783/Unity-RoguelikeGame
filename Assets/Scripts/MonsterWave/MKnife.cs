using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKnife : MonoBehaviour
{
    public float BDamage;

    private float BulletTimer;
    void Start()
    {
        BulletTimer = Time.time;
    }

    void Update()
    {
        if (Time.time - BulletTimer >= 0.5f)
        {
            Destroy(gameObject);
        }
    }

    public void Init(float BDamage)//대미지 초기화
    {
        this.BDamage = BDamage;
    }
}
