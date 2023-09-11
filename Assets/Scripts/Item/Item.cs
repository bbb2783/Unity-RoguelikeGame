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
    public bool Use()
    {
        return false;
    }
}
