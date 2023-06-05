using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    

    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion


    public delegate void OnSlotCountChange(int val);
    public OnSlotCountChange onSlotCountChange;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;
    public List<Item> items = new List<Item>();
    private int slotCnt;
    public int SlotCnt
    {
        get => slotCnt;
        set{
            slotCnt=value;
            onSlotCountChange.Invoke(slotCnt);
        }
    }
    void Start()
    {
        SlotCnt = 4; //시작 도감 숫자
    }

    public bool AddItem(Item _item)
    {
        if(items.Count < SlotCnt)
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke(); //아이템을 추가하면 onchangeitme을 호출
            
            return true;
        }
        else
    {
        Debug.Log("인벤토리가 가득 찼습니다.");
        return false;
    }
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }
    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

   


    // 아이템을 인벤토리에 추가하는 메서드
    public bool Add(Item item)
    {
        if (items.Count >= SlotCnt)
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            return false;
        }

        items.Add(item);

        if (onInventoryChangedCallback != null)
        {
            onInventoryChangedCallback.Invoke();
        }

        return true;
    }

    // 아이템을 인벤토리에서 제거하는 메서드
    public void Remove(Item item)
    {
        items.Remove(item);

        if (onInventoryChangedCallback != null)
        {
            onInventoryChangedCallback.Invoke();
        }
    }

     public void CloseInventory()
    {
        // 인벤토리 UI를 비활성화
        gameObject.SetActive(false);
        
        // Tooltip 오브젝트를 비활성화
        Tooltip[] tooltips = GetComponentsInChildren<Tooltip>();
        foreach (Tooltip tooltip in tooltips)
        {
            tooltip.gameObject.SetActive(false);
        }
    }

    public void OpenInventory()
{
    // 인벤토리 UI를 활성화
    gameObject.SetActive(true);

    // 모든 슬롯에 대해 검사하여 아이템이 있는 경우 툴팁을 활성화
    foreach (Transform child in transform)
    {
        Slot slot = child.GetComponent<Slot>();
        if (slot != null && slot.item != null)
        {
            Tooltip tooltip = slot.GetComponentInChildren<Tooltip>();
            if (tooltip != null)
            {
                tooltip.gameObject.SetActive(true);
                tooltip.SetupTooltip(slot.item.itemName, slot.item.itemDescription, slot.item.Atk);
            }
        }
    }
}

}
