using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public GameObject uiObject; // UI 객체를 연결할 변수

    private void Start()
    {
        // 게임 시작 시 UI를 비활성화
        uiObject.SetActive(false);
    }

    private void Update()
    {
        // 'Insert' 키를 누르면 UI를 활성화
        if (Input.GetKeyDown(KeyCode.Insert))
        {
            ToggleUI(); // UI 활성화/비활성화 토글
        }
    }

    void ToggleUI()
    {
        uiObject.SetActive(!uiObject.activeSelf); // UI의 상태를 반전
    }
}
