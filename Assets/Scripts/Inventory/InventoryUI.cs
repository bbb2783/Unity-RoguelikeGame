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
        LoadInventory();
        RedrawSlotUI();
        inventoryPanel.SetActive(activeInventory);
    }
    private void OnDestroy()
    {
        SaveInventory(); // 게임 종료 시 인벤토리 정보 저장
    }

    private void OnApplicationQuit() //테스트용 초기화
    {
        // 게임 종료 시 인벤토리 아이템 정보 초기화
        inven.items.Clear();
        inven.SlotCnt = 0;
        PlayerPrefs.DeleteAll(); // 모든 PlayerPrefs 삭제 (아이템 정보와 슬롯 카운트)
    }

    private void SaveInventory()
    {
        // 인벤토리 정보를 PlayerPrefs에 저장
        for (int i = 0; i < inven.items.Count; i++)
        {
            PlayerPrefs.SetString("Item_" + i, JsonUtility.ToJson(inven.items[i]));
        }
        PlayerPrefs.SetInt("SlotCount", inven.SlotCnt);
        PlayerPrefs.Save();
    }

    private void LoadInventory()
    {
        // 저장된 인벤토리 정보를 PlayerPrefs로부터 불러오기
        int savedSlotCount = PlayerPrefs.GetInt("SlotCount", 0);
        inven.SlotCnt = savedSlotCount;

        for (int i = 0; i < savedSlotCount; i++)
        {
            string itemJson = PlayerPrefs.GetString("Item_" + i);
            if (!string.IsNullOrEmpty(itemJson))
            {
                Item savedItem = JsonUtility.FromJson<Item>(itemJson);
                inven.items.Add(savedItem);
            }
        }
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

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            transform.position = Input.mousePosition;
        }
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

            // 아이템 이미지 설정은 여기서 수행
            if (slots[i].item != null)
            {
                slots[i].itemIcon.sprite = inven.items[i].itemImage3; // itemImage3를 사용하여 이미지 설정
                slots[i].itemIcon.gameObject.SetActive(true);
            }
        
            slots[i].UpdateSlotUI();
        }
    }
}

