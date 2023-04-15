using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBullet : MonoBehaviour
{
    public float bulletSpd;

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

    private void OntriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
