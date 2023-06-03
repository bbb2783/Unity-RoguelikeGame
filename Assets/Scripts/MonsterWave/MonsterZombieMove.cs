using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterZombieMove : MonoBehaviour
{
    public float speed;
    public float zombieHealth;
    public float zombieMaxHealth;
    WaitForFixedUpdate wait;

    public Rigidbody2D target;

    bool isLive;

    private Animator anim; //for 애니메이션 제어

    private float scaleX;
    private float scaleY;
    private float scaleZ;

    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        wait = new WaitForFixedUpdate();

        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
        scaleZ = transform.localScale.z;
    }

    void FixedUpdate()
    {
        if(!isLive)
            return;

        if (zombieHealth <= 0)
            {return;}    
        
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if(target.position.x < rigid.position.x)
        {
            transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
        }
        else transform.localScale = new Vector3(-scaleX, scaleY, scaleZ);
    }

    void OnEnable()
    {
        target = MGameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        zombieHealth = zombieMaxHealth;
    }

    public void Init(SpawnData data)
    {
        speed = data.zombieSpeed;
        zombieMaxHealth = data.zombieHealth;
        zombieHealth = data.zombieHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return; //총알과 충돌한게 아니면 리턴

        zombieHealth -= collision.GetComponent<MBullet>().BDamage;

        if (zombieHealth > 0)
        {
            //live
        }
        else 
        {
            anim.SetTrigger("isDie");
        }

        
    }

    void Dead()
        {
            gameObject.SetActive(false);
        }
}
