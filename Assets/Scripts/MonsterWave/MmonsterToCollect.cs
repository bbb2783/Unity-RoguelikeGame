using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//몬스터웨이브 씬에서 채집 씬으로 이동
//플레이어의 체력이 1이상이고, 스폰레벨이 2일때 3초후 이동
public class MmonsterToCollect : MonoBehaviour
{
    //MSpawner mSpawner;
    //MonsterPlayerMove monsterPlayerMove;
    public GameObject MSpawner;
    public GameObject MonsterPlayerMove;
    float isLevel;
    float isPlayerLive;


    void Awake()
    {
        //mSpawner = GameObject.FindWithTag("MSpawner").GetComponent<MSpawner>();
        //monsterPlayerMove = GameObject.FindWithTag("Player").GetComponent<MonsterPlayerMove>();
        //Invoke ("sceneChange",3);
        isLevel = MSpawner.GetComponent<MSpawner>().spawnLevel;
        isPlayerLive = MonsterPlayerMove.GetComponent<MonsterPlayerMove>().playerHealth;
    }
    
    
    void Update()
    {
        if((MSpawner.GetComponent<MSpawner>().spawnLevel==2)
            &&(MonsterPlayerMove.GetComponent<MonsterPlayerMove>().playerHealth>0))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}