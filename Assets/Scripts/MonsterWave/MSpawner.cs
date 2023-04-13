using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSpawner : MonoBehaviour
{
    public Transform[] MSPoint;
    public SpawnData[] SpawnData;
    
    int level;
    float MTimer;

    void Awake()
    {
        MSPoint = GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        MTimer += Time.deltaTime;
        level = Mathf.FloorToInt(MGameManager.instance.gameTimer/20f);

        if (MTimer > (level == 0 ? 0.5f : 0.2f)) {
            MTimer = 0;
            MSpawn();
        }  
    }

    void MSpawn()//몬스터 스폰
    {
        GameObject MonsterZombieMove = MGameManager.instance.MonsterPool.GetZombie(level);
        MonsterZombieMove.transform.position = MSPoint[Random.Range(1,MSPoint.Length)].position;
    }
}

[System.Serializable]
public class SpawnData
{
    public int spriteType;
    public float spawnTime;
    public int monsterHealth;
    public float monsterSpeed;
}
