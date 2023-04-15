using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSpawner : MonoBehaviour
{
    public Transform[] MSPoint;
    public SpawnData[] SpawnData;

    int spawnLevel;
    float MTimer;

    void Awake()
    {
        MSPoint = GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        MTimer += Time.deltaTime;
        spawnLevel = Mathf.FloorToInt(MGameManager.instance.gameTime / 10f);

        if (MTimer > SpawnData[spawnLevel].spawnTime) {
            MTimer = 0;
            MSpawn();
        }  
    }

    void MSpawn()//몬스터 스폰
    {
        GameObject MonsterZombieMove = MGameManager.instance.MonsterPool.GetZombie(spawnLevel);
        MonsterZombieMove.transform.position = MSPoint[Random.Range(1,MSPoint.Length)].position;
        MonsterZombieMove.GetComponent<MonsterZombieMove>().Init(SpawnData[spawnLevel]);
    }
}

[System. Serializable] //직렬화
public class SpawnData
{
    public float spawnTime;
    //public int spriteType;
    public int zombieHealth;
    public float zombieSpeed;
}