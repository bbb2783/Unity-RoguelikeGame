using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterZombieMove : MonoBehaviour
{
    public float speed;
    public float zombieHealth;
    public float zombieMaxHealth;
    //public RuntimeAnimatorController[] animacon;
    WaitForFixedUpdate wait;

    public Rigidbody2D target;

    bool isLive;

    Rigidbody2D rigid;
    //Animator anime;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //anime = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
    }

    void FixedUpdate()
    {
        if(!isLive)
            return;
        
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = MGameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        zombieHealth = zombieMaxHealth;
    }

    public void Init(SpawnData data)
    {
        //anime.runtimeAnimatorController = animacon[data.spriteType];
        speed = data.zombieSpeed;
        zombieMaxHealth = data.zombieHealth;
        zombieHealth = data.zombieHealth;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet")) return; //총알과 충돌한게 아니면 리턴

        zombieHealth -= collision.GetComponent<MBullet>().BDamage;
        //StartCoroutine(KnockBack());

        if (zombieHealth > 0)
        {
            //live
        }
        else 
        {
            //die
            Dead();
        }

        /*IEnumerator KnockBack()
        {
            yield return wait; //다음 하나의 물리 프레임 딜레이
            Vector3 playerPos = MGameManager.instance.player.transform.position;
            Vector3 dirVec = transform.position = playerPos;
            rigid.AddForce(dirVec.normalized *3, ForceMode2D.Impulse);
        }*/

        void Dead()
        {
            gameObject.SetActive(false);
        }
    }
}
