using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGameManager : MonoBehaviour
{
    public static MGameManager instance;
    
    [Header("# Game control")]
    public float gameTime;
    public float maxGameTime = 2*150f;
    public bool isLive;

    [Header("# Player control")]
    public MonsterPlayerMove player;
    public MPoolManager MonsterPool;
    public float playerHealth;
    public float playerMaxHealth = 200;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime) {
            gameTime = maxGameTime;
        }  
    }
}