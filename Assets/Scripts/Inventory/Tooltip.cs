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
    private void Update()
{
    // 현재 마우스 위치를 가져옴
    Vector2 localPoint;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(
        transform.parent.GetComponent<RectTransform>(), 
        Input.mousePosition, 
        Camera.main, 
        out localPoint
    );

    // Tooltip 위치를 마우스 위치에서 Tooltip 크기의 절반만큼 더한 곳으로 설정
    Vector2 offset = new Vector2(
        GetComponent<RectTransform>().rect.width / 2f,
        -GetComponent<RectTransform>().rect.height / 2f
    );
    transform.localPosition = localPoint + offset;

    // Tooltip이 화면 밖으로 벗어나지 않도록 보정
    RectTransform rectTransform = GetComponent<RectTransform>();
    Vector3[] corners = new Vector3[4];
    rectTransform.GetWorldCorners(corners);
    Vector2 pivot = rectTransform.pivot;
    Vector2 size = new Vector2(corners[3].x - corners[0].x, corners[1].y - corners[0].y);
    Vector2 minAnchor = new Vector2(pivot.x, pivot.y) - new Vector2(0.5f, 0.5f);
    Vector2 maxAnchor = minAnchor + size / transform.parent.GetComponent<RectTransform>().rect.size;
    minAnchor.x = Mathf.Max(minAnchor.x, 0f);
    maxAnchor.x = Mathf.Min(maxAnchor.x, 1f);
    minAnchor.y = Mathf.Max(minAnchor.y, 0f);
    maxAnchor.y = Mathf.Min(maxAnchor.y, 1f);
    rectTransform.anchorMin = minAnchor;
    rectTransform.anchorMax = maxAnchor;
}

public void DeactivateTooltip()
{
    gameObject.SetActive(false);
}
}