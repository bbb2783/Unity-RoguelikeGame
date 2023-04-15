using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGameManager : MonoBehaviour
{
    public static MGameManager instance;
    
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    public MonsterPlayerMove player;
    public MPoolManager MonsterPool;

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