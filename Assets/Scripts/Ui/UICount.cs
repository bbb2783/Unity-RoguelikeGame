using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UICount : MonoBehaviour
{
    public TextMeshProUGUI itemCountText;

    private void UpdateItemCount()
    {   
        // PlantDatabase의 instance를 가져옴
        PlantDatabase plantDatabase = PlantDatabase.instance;

        // 필드 아이템 개수를 가져와서 표시
        int itemCount = plantDatabase.itemDB.Count;  // 수정된 부분
        int totalItemCount = plantDatabase.pos.Length;
        itemCountText.text = itemCount + "/" + totalItemCount;
    }

    private void OnEnable()  // 수정된 부분
    {
        UpdateItemCount();
    }
}
