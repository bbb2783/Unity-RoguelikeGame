using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGameManager : MonoBehaviour
{
    public static MGameManager instance;
    
    [Header("# Game control")]
    public float gameTime;
    public float maxGameTime;
    public bool isLive;

    [Header("# Player control")]
    public MonsterPlayerMove player;
    public MPoolManager MonsterPool;
    public float playerHealth;
    public float playerMaxHealth = 200;
    public float playerMana;
    public float playerMaxMana = 500;

    void Awake()
    {
        instance = this;
        maxGameTime = Random.Range(30f,151f);
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
        }  
    }
}