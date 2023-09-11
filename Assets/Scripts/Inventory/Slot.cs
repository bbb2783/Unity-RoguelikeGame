using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item item;
    public Image itemIcon;
    public Image itemImage3; // �� ��° �̹����� ǥ���� UI ��� �߰�

    private void Awake()
    {
        // �� ��° �̹��� UI ��Ҹ� ã�Ƽ� �ʱ�ȭ
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
