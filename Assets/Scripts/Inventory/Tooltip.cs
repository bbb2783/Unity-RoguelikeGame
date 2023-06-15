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

    private RectTransform parentRectTransform;
    private RectTransform tooltipRectTransform;

    private void Awake()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        tooltipRectTransform = GetComponent<RectTransform>();
    }

    public void SetupTooltip(string name, string des, int atk)
    {
        nameTxt.text = name;
        descriptionTxt.text = des;

        if (atk == 0)
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

    public void UpdateTooltipPosition(Vector2 mousePosition)
    {
        // Tooltip 위치를 마우스 위치로 설정
        tooltipRectTransform.position = mousePosition;

        // Tooltip이 화면 밖으로 벗어나지 않도록 보정
        Vector2 anchoredPosition = tooltipRectTransform.anchoredPosition;
        Vector2 sizeDelta = tooltipRectTransform.sizeDelta;
        Vector2 parentSizeDelta = parentRectTransform.sizeDelta;
        Vector2 minAnchor = new Vector2(0f, 0f);
        Vector2 maxAnchor = new Vector2(1f, 1f);

        // 좌측 보정
        if (anchoredPosition.x < 0f)
        {
            minAnchor.x = anchoredPosition.x / parentSizeDelta.x;
        }
        // 우측 보정
        else if (anchoredPosition.x + sizeDelta.x > parentSizeDelta.x)
        {
            maxAnchor.x = (anchoredPosition.x + sizeDelta.x) / parentSizeDelta.x;
        }

        // 하단 보정
        if (anchoredPosition.y < 0f)
        {
            minAnchor.y = anchoredPosition.y / parentSizeDelta.y;
        }
        // 상단 보정
        else if (anchoredPosition.y + sizeDelta.y > parentSizeDelta.y)
        {
            maxAnchor.y = (anchoredPosition.y + sizeDelta.y) / parentSizeDelta.y;
        }

        tooltipRectTransform.anchorMin = minAnchor;
        tooltipRectTransform.anchorMax = maxAnchor;
    }

    public void DeactivateTooltip()
    {
        gameObject.SetActive(false);
    }
}
