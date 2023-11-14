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
        if(!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))
            return;

        if (zombieHealth <= 0)
            {return;}    
        
        
        Vector2 dirVec = target.position - rigid.position; //움직임
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
        
        if(MGameManager.instance.gameTime>=300)
        {zombieHealth = 0;}
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
        if (collision.CompareTag("Bullet")){//총알 충돌처리
            anim.SetTrigger("isHit");
            zombieHealth -= collision.GetComponent<MBullet>().BDamage;
        } 
        else if(collision.CompareTag("Knife")){//가로베기 충돌처리
            anim.SetTrigger("isHit");
            zombieHealth -= collision.GetComponent<MKnife>().BDamage;
            StartCoroutine(KnockBack());
        }
        else if(collision.CompareTag("Electric")){//전기장 충돌처리
            anim.SetTrigger("isHit");
            zombieHealth -= collision.GetComponent<MElectric>().BDamage;
            speed = 0;
        }
        else if(collision.CompareTag("Ray")){//레일건 충돌처리
            anim.SetTrigger("isHit");
            zombieHealth -= collision.GetComponent<MRay>().BDamage;
        }
        else {return;}

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

    IEnumerator KnockBack()
    {
        yield return wait; //딜레이
        Vector3 playerPos =  MGameManager.instance.player.transform.position;
        Vector3 dirVec = transform.position - playerPos;
        rigid.AddForce(dirVec.normalized * 10, ForceMode2D.Impulse);
    }
}
