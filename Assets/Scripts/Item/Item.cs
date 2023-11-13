using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType
{
    Equipment,
    Consumables,
    Etc
}

[System.Serializable]
public class Item
{
    public int Atk;
    public string itemDescription;
    public ItemType itemType;
    public string itemName;
    public Sprite itemImage;
    public Sprite itemImage2;
    public Sprite itemImage3;

    public GameObject itemPrefab; // 스폰할 아이템 프리팹
    //public Vector3 spawnPosition;  // 아이템을 스폰할 위치


    public bool Use()
    {
        return false;
    }
}
