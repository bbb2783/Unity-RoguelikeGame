using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;

    public void UpdateSlotUI()
{
    if (item != null && item.itemImage != null)
    {
        itemIcon.sprite = item.itemImage;
        itemIcon.gameObject.SetActive(true);
    }
    else
    {
        itemIcon.gameObject.SetActive(false);
    }
}


    public void RemoveSlot()
    {
        item = null;
        itemIcon.gameObject.SetActive(false);
    }
}
