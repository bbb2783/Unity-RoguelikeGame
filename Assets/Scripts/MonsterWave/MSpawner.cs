using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSpawner : MonoBehaviour
{
    public Transform[] MSPoint;
    float MTimer;

    void Awake()
    {
        MSPoint = GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        MTimer += Time.deltaTime;

        if (MTimer > 0.5f) {
            MTimer = 0;
            MSpawn();
        }  
    }

    void MSpawn()//몬스터 스폰
    {
        GameObject MonsterZombieMove = MGameManager.instance.MonsterPool.GetZombie(Random.Range(0,1));
        MonsterZombieMove.transform.position = MSPoint[Random.Range(1,MSPoint.Length)].position;
    }
}
