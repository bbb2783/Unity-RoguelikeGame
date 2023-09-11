using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;
    public Image itemImage3; // 세 번째 이미지를 표시할 UI 요소 추가

    private void Awake()
    {
        // 세 번째 이미지 UI 요소를 찾아서 초기화
        itemImage3 = transform.Find("Item Image").GetComponent<Image>();
        itemIcon = GetComponent<Image>();
    }


    public void UpdateSlotUI()
    {
        if (item != null && item.itemImage3 != null)
        {
            itemImage3.sprite = item.itemImage3;
            itemImage3.gameObject.SetActive(true);
        }
        else
        {
            itemImage3.gameObject.SetActive(false);
        }
    }

    public void RemoveSlot()
    {
        item = null;
        itemImage3.gameObject.SetActive(false);
    }
}
