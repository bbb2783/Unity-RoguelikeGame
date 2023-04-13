using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGameManager : MonoBehaviour
{
    public static MGameManager instance;
    public float gameTimer;
    public float maxGameTime = 2*10f;

    public MonsterPlayerMove player;
    public MPoolManager MonsterPool;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        gameTimer += Time.deltaTime;

        if (gameTimer > maxGameTime) {
            gameTimer = maxGameTime;
        }  
    }
}