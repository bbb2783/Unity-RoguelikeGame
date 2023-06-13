using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//몬스터웨이브 씬에서 채집 씬으로 이동
//플레이어의 체력이 1이상이고, 스폰레벨이 2일때 3초후 이동
public class MmonsterToCollect : MonoBehaviour
{
    public GameObject MSpawner;
    public GameObject MGameManager;
    public int MonsterSceneNum;
    float isLevel;
    float isPlayerLive;
    public GameObject finishTxt;

    void Awake()
    {
        isLevel = MSpawner.GetComponent<MSpawner>().spawnLevel;
        isPlayerLive = MGameManager.GetComponent<MGameManager>().playerHealth;
    }
    
    
    void Update()
    {
        if((MSpawner.GetComponent<MSpawner>().spawnLevel==2)
            &&(MGameManager.GetComponent<MGameManager>().playerHealth>0))
        {
            finishTxt.SetActive(true);
            Invoke("Sceneloader", 3f);
        }
    }

    void Sceneloader()
    {
        switch(MonsterSceneNum)
            {
                case 1:
                    SceneManager.LoadScene("D_forest_mid"); return;
                case 2:
                    SceneManager.LoadScene("D_river_mid"); return;
                case 3:
                    SceneManager.LoadScene("D_city_mid"); return;
            }
    }
}