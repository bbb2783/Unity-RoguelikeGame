using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPoolManager : MonoBehaviour
{
    //프리펩과 리스트 개수는  1:1
    //프리펩 보관 변수
    public GameObject[] ZombieArr; //prefab

    //풀 담당 리스트
    List<GameObject>[] ZombiePool; //pools

    void Awake()
    {
        ZombiePool = new List<GameObject>[ZombieArr.Length];

        for (int index = 0; index < ZombiePool.Length; index++){
            ZombiePool[index] = new List<GameObject>();
        }

        Debug.Log(ZombiePool.Length);
    }

    public GameObject GetZombie(int index)
    {
        GameObject select = null;
        //선택한 풀의 놀고있는 게임 오브젝트 접근
        
        
        foreach (GameObject item in ZombiePool[index]){
            if (!item.activeSelf){
                //발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //못찾았으면 새롭게 생성하고 select 변수에 할당
        if (!select){
            select = Instantiate(ZombieArr[index], transform);
            ZombiePool[index].Add(select);
        }

        
        return select;
    }

}
