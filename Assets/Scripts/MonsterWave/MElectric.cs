using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MElectric : MonoBehaviour
{
    public float BDamage;
    //public int per;

    private float BulletTimer;
    void Start()
    {
        BulletTimer = Time.time;
    }

    void Update()
    {
        if (Time.time - BulletTimer >= 10)
        {
            Destroy(gameObject);
        }
    }

    public void Init(float BDamage)//대미지 초기화
    {
        this.BDamage = BDamage;
    }
}
