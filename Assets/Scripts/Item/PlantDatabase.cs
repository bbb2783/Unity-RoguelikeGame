using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlantDatabase : MonoBehaviour
{
    public static PlantDatabase instance;
    public Sprite itemImage3;
    private void Awake()
    {
        instance = this;
    }
    
    public List<Item> itemDB = new List<Item>();

    public GameObject fieldItemPrefab;
    public Vector3[] pos;
    public List<GameObject> spawnedItems = new List<GameObject>();



    private void Start()
{
    List<int> randomIndexes = GenerateUniqueRandomNumbers(0, pos.Length, 10);
    for (int i = 0; i < randomIndexes.Count; i++)
    {
        int index = randomIndexes[i];
        GameObject go = Instantiate(fieldItemPrefab, pos[index], Quaternion.identity);
        
        // 아이템 데이터베이스에서 아이템 선택
        Item selectedItem = itemDB[Random.Range(0, itemDB.Count)];

        // 이미지 설정은 여기서 수행
        Image itemImage = go.GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = selectedItem.itemImage3; // 아이템의 itemImage3를 사용하여 이미지 설정
        }

        go.GetComponent<FieldItem>().SetItem(selectedItem);

        spawnedItems.Add(go);
    }
}




    private List<int> GenerateUniqueRandomNumbers(int min, int max, int count)
    {
        List<int> numbers = new List<int>();
        for (int i = min; i < max; i++)
        {
            numbers.Add(i);
        }

        List<int> result = new List<int>();
        while (result.Count < count && numbers.Count > 0)
        {
            int index = Random.Range(0, numbers.Count);
            result.Add(numbers[index]);
            numbers.RemoveAt(index);
        }

        return result;
    }

    public bool IsAllItemsPickedUp()
    {
        foreach (GameObject item in spawnedItems)
        {
            if (item.activeSelf)
            {
                return false;
            }
        }
        return true;
    }
}
