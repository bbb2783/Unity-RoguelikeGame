using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGameManager : MonoBehaviour
{
    public static MGameManager instance;
    public MonsterPlayerMove player;
    public MPoolManager MonsterPool;

    void Awake()
    {
        instance = this;
    }
}