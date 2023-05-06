using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform parentRectTransform;
    private RectTransform rectTransform;

    private float initialPosX;
    private float initialPosY;

    private void Start()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();

        if (PlayerPrefs.HasKey(gameObject.name + "_PosX"))
        {
            initialPosX = PlayerPrefs.GetFloat(gameObject.name + "_PosX");
            initialPosY = PlayerPrefs.GetFloat(gameObject.name + "_PosY");
        }
        else
        {
            initialPosX = rectTransform.localPosition.x;
            initialPosY = rectTransform.localPosition.y;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 부모 RectTransform 영역 내에서 위치를 제한합니다.
        Rect parentRect = parentRectTransform.rect;
        Rect rect = rectTransform.rect;
        Vector2 clampedPos = eventData.position;
        clampedPos.x = Mathf.Clamp(clampedPos.x, parentRect.xMin + rect.width / 2, parentRect.xMax - rect.width / 2);
        clampedPos.y = Mathf.Clamp(clampedPos.y, parentRect.yMin + rect.height / 2, parentRect.yMax - rect.height / 2);

        // 클램핑된 포지션을 전역 좌표에서 로컬 좌표로 변환합니다.
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, clampedPos, eventData.pressEventCamera, out Vector2 localPos))
        {
            rectTransform.localPosition = localPos;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");

        // 마우스 버튼을 놓으면 현재 RectTransform 위치를 저장합니다.
        PlayerPrefs.SetFloat(gameObject.name + "_PosX", rectTransform.localPosition.x);
        PlayerPrefs.SetFloat(gameObject.name + "_PosY", rectTransform.localPosition.y);
    }

    public void ResetPosition()
    {
        rectTransform.localPosition = new Vector3(initialPosX, initialPosY, rectTransform.localPosition.z);
    }
}
