using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;
    public Image itemImage3; 

    private void Awake()
    {
        itemImage3 = transform.Find("Item Image").GetComponent<Image>();
        itemIcon = GetComponent<Image>();
    }


    public void UpdateSlotUI()
{
    if (item != null && itemImage3 != null)
    {
        itemImage3.sprite = item.itemImage3;

        if (item.itemImage3 != null)
        {
            itemImage3.gameObject.SetActive(true);
        }
        else
        {
            itemImage3.gameObject.SetActive(false);
        }
    }
    else
    {
        // item이 null이거나 itemImage3가 null인 경우
        if (itemImage3 != null)
        {
            itemImage3.gameObject.SetActive(false);
        }
    }
}



    public void RemoveSlot()
    {
        item = null;
        itemImage3.gameObject.SetActive(false);
    }

}
