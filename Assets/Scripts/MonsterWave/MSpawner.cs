using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSpawner : MonoBehaviour
{
    public Transform[] MSPoint;
    public SpawnData[] SpawnData;

    public int spawnLevel;
    float MTimer;

    void Awake()
    {
        MSPoint = GetComponentsInChildren<Transform>();
    }
    
    void Update()
    {
        MTimer += Time.deltaTime;
        spawnLevel = Mathf.FloorToInt(MGameManager.instance.gameTime / 150f);

        if (MTimer > SpawnData[spawnLevel].spawnTime) {
            MTimer = 0;
            MSpawn();
        }  
    }

    void MSpawn()//몬스터 스폰
    {
        if(spawnLevel==0)
        {
            GameObject MonsterZombieMove = MGameManager.instance.MonsterPool.GetZombie(spawnLevel);
            MonsterZombieMove.transform.position = MSPoint[Random.Range(1,MSPoint.Length)].position;
            MonsterZombieMove.GetComponent<MonsterZombieMove>().Init(SpawnData[spawnLevel]);
        }
        else if(spawnLevel==1)
        {
            GameObject MonsterZombieMove = MGameManager.instance.MonsterPool.GetZombie(spawnLevel);
            GameObject MonsterZombieMove2 = MGameManager.instance.MonsterPool.GetZombie(spawnLevel-1);
            MonsterZombieMove.transform.position = MSPoint[Random.Range(1,MSPoint.Length)].position;
            MonsterZombieMove2.transform.position = MSPoint[Random.Range(1,MSPoint.Length)].position;
            MonsterZombieMove.GetComponent<MonsterZombieMove>().Init(SpawnData[spawnLevel]);
            MonsterZombieMove2.GetComponent<MonsterZombieMove>().Init(SpawnData[spawnLevel-1]);
        }
        else {return;}
    }
}

[System. Serializable] //직렬화
public class SpawnData
{
    public float spawnTime;
    public int zombieHealth;
    public float zombieSpeed;
}