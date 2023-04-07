using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI nameTxt;
    public TextMeshProUGUI descriptionTxt;
    public TextMeshProUGUI atkTxt;
    public TextMeshProUGUI atkValueTxt;

    /*private void Update()
    {
        // 현재 마우스 위치를 스크린 좌표로 가져옵니다.
        Vector3 mousePos = Input.mousePosition;

        // 마우스 위치를 월드 좌표로 변환합니다.
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        // Tooltip 오브젝트를 마우스 위치로 이동합니다.
        transform.position = worldPos;
    }*/

    public void SetupTooltip(string name, string des, int atk)
    {
        nameTxt.text=name;
        descriptionTxt.text=des;

        if(atk == 0)
        {
            atkTxt.gameObject.SetActive(false);
            atkValueTxt.gameObject.SetActive(false);
        }
        else
        {
            atkTxt.gameObject.SetActive(true);
            atkValueTxt.gameObject.SetActive(true);
            atkValueTxt.text = atk.ToString();
        }
    }
}