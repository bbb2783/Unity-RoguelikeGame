using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
public class InventoryUI : MonoBehaviour
{
    Inventory inven;
    public GameObject inventoryPanel; //인벤토리 오브젝 트
    bool activeInventory = false; // 항상 false 값으로 표현 

    public Slot[] slots; // 인벤토리 슬롯을 배열로 선언
    public Transform slotHolder;
    Transform playerTransform; // 플레이어 캐릭터의 Transform 컴포넌트
    public Transform[] spawnPoints; // 아이템 뿌리는 장소
   
    
    List<Vector3> itemPositions = new List<Vector3>();
    private void Start() 
    {
        inven = Inventory.instance;
        slots = slotHolder.GetComponentsInChildren<Slot>(); //기존에있는 인벤토리를 확인하여 배열에 넣어줌
        inven.onSlotCountChange +=SlotChange;
        inven.onChangeItem +=RedrawSlotUI;
        LoadInventory();
        RedrawSlotUI();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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
    for (int i = 0; i < slots.Length; i++)
    {
        if (i < inven.SlotCnt)
        {
            // 슬롯이 존재하고 유효한지 확인
            if (slots[i] != null)
            {
                Button slotButton = slots[i].GetComponent<Button>();
                if (slotButton != null)
                {
                    slotButton.interactable = true;
                }
            }
        }
        else
        {
            if (slots[i] != null)
            {
                Button slotButton = slots[i].GetComponent<Button>();
                if (slotButton != null)
                {
                    slotButton.interactable = false;
                }
            }
        }
    }
}


    private void Update()
    {

        if(Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            transform.position = Input.mousePosition;
            HandleItemClick();
        }
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            activeInventory = !activeInventory; // i키를 눌려서 false된 인벤토리를 true값으로 바꿔서 실행
            inventoryPanel.SetActive(activeInventory);
        }

    }

    private void HandleItemClick()
    {
        // 클릭 이벤트 처리 메서드
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10; // 화면에서 아이템이 표시되는 Z 좌표

        // 마우스 포인터의 스크린 좌표를 월드 좌표로 변환
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // 클릭 위치에서 아이템을 찾거나 처리하는 로직 추가
        // 예: 아이템 스폰 또는 아이템 클릭 시 동작 등

        // 아이템이 있는지 확인 후 처리
        if (!IsItemInPosition(worldPosition))
        {
            // 아이템이 없는 경우에만 인벤토리 UI를 업데이트
            RedrawSlotUI();
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
        if (i < inven.items.Count)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();

            // 아이템의 세 번째 이미지를 직접 설정
            if (slots[i].item != null && slots[i].item.itemImage3 != null)
            {
                // 이미지 제거 대신 새 이미지로 업데이트
                slots[i].itemImage3.sprite = slots[i].item.itemImage3;
                slots[i].itemImage3.gameObject.SetActive(true);
            }
            else
            {
                // 이미지가 없는 경우 비활성화
                slots[i].itemImage3.gameObject.SetActive(false);
            }
        }
        else
        {
            // 인벤토리에 아이템이 없는 슬롯의 이미지 비활성화
            slots[i].RemoveSlot();
        }
    }
}

    public void OnItemClick(int itemIndex)
{
    if (itemIndex >= 0 && itemIndex < inven.items.Count)
    {
        Item clickedItem = inven.items[itemIndex];

        // 가능한 스폰 위치 중에서 아이템이 없는 위치를 찾습니다.
        Transform spawnPoint = GetNextAvailableSpawnPoint();

        if (spawnPoint != null)
        {
            // 아이템이 없는 스폰 위치에 아이템을 스폰합니다.
            Vector3 spawnPosition = spawnPoint.position;
            GameObject spawnedItem = Instantiate(clickedItem.itemPrefab, spawnPosition, Quaternion.identity);

            // 아이템 스폰 후 해당 슬롯의 아이템을 제거
            inven.RemoveItem(clickedItem);

            // 인벤토리 UI 갱신
            RedrawSlotUI();
        }
    }
}

int spawnPointIndex = 0;

Transform GetNextAvailableSpawnPoint()
{
    int originalIndex = spawnPointIndex;

    do
    {
        Transform spawnPoint = spawnPoints[spawnPointIndex];

        // 현재 스폰 포인트에 아이템이 없으면 해당 위치를 반환합니다.
        if (!IsItemInPosition(spawnPoint.position))
        {
            spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Length; // 다음 인덱스로 업데이트
            return spawnPoint;
        }

        spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Length; // 다음 인덱스로 업데이트
    } while (spawnPointIndex != originalIndex); // 모든 스폰 포인트를 확인했는지 검사

    // 모든 스폰 포인트에 아이템이 있다면 null을 반환합니다.
    return null;
}





    bool IsItemInPosition(Vector3 position)
{
    float radius = 1.0f; // 아이템을 확인할 반경

    Collider[] colliders = Physics.OverlapSphere(position, radius);
    foreach (Collider collider in colliders)
    {
        if (collider.CompareTag("Item")) // 아이템을 식별할 태그를 사용
        {
            return true; // 아이템이 해당 위치에 존재
        }
    }
    return false; // 아이템이 해당 위치에 없음
}




}
