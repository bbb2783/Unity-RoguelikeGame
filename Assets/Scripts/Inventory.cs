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
        return false;
    }
    public void RemoveItem(Item item)
    {
        items.Remove(item);
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FieldItem")
        {
            FieldItem fieldItem = collision.GetComponent<FieldItem>();
                if(AddItem(fieldItem.GetItem()))
                {
                    fieldItem.DestroyItem();
                }
        }
        }*/


    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

   
    //public int space = 20; // 인벤토리 슬롯 개수

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

}
