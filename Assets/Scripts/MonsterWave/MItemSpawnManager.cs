using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MItemSpawnManager : MonoBehaviour
{
    public GameObject[] SupportItem;

    int count = 1;
    private BoxCollider2D area;
    private List<GameObject> ItemList = new List<GameObject>();

    void Start()
    {
        area = GetComponent<BoxCollider2D>();
        StartCoroutine("Spawn", 10);
    }

    //게임 오브젝트를 복제하여 scene에 추가
    private IEnumerator Spawn(float delayTime)
    {
        for (int i = 0; i < count; i++) //count만큼 Item 생성
        {
            int selection = Random.Range(0, SupportItem.Length);
            
            Vector3 spawnPos = GetRandomPosition(); //랜덤 위치 return

            GameObject selectedItem = SupportItem[selection];

            GameObject instance = Instantiate(selectedItem, spawnPos, Quaternion.identity);
            ItemList.Add(instance);
        }
        
        area.enabled = false;
        yield return new WaitForSeconds(delayTime);   //주기 : 20초

        for (int i = 0; i < count; i++) //책 삭제
            Destroy(ItemList[i].gameObject);

        ItemList.Clear();           //bookList 비우기
        area.enabled = true;
        StartCoroutine("Spawn", 10);    //책 다시 스폰

    }

    //BoxCollider2D 내의 랜덤한 위치를 return
    private Vector2 GetRandomPosition()
    {
        Vector2 basePosition = transform.position;  //오브젝트의 위치
        Vector2 size = area.size;                   //box colider2d, 즉 맵의 크기 벡터

        //x, y축 랜덤 좌표 얻기
        float posX = basePosition.x + Random.Range(-size.x / 2f, size.x / 2f);
        float posY = basePosition.y + Random.Range(-size.y / 2f, size.y / 2f);

        Vector2 spawnPos = new Vector2(posX, posY);

        return spawnPos;
    }

}
