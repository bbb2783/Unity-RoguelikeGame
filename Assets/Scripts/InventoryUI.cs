using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    public GameObject inventoryPanel; //인벤토리 오브젝트
    bool activeInventory = false; // 항상 false 값으로 표현 

    public Slot[] slots; // 인벤토리 슬롯을 배열로 선언
    public Transform slotHolder;
    
    private void Start() 
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>(); //기존에있는 인벤토리를 확인하여 배열에 넣어줌
        inven.onSlotCountChange +=SlotChange;
        inven.onChangeItem +=RedrawSlotUI;
        inventoryPanel.SetActive(activeInventory);
    }

    private void SlotChange(int val)
    {
        for(int i =0; i<slots.Length; i++)
        {
            if(i<inven.SlotCnt)
            {
                slots[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                slots[i].GetComponent<Button>().interactable = false;
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            activeInventory = !activeInventory; // i키를 눌려서 false된 인벤토리를 true값으로 바꿔서 실행
            inventoryPanel.SetActive(activeInventory);
        }
    }

    public void AddSlot()
    {
        inven.SlotCnt++;
    }

    void RedrawSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
