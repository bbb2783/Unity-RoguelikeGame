using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private RectTransform parentRectTransform;
    private RectTransform rectTransform;

    private Vector2 initialPosition;
    private Vector2 offset;

    private void Start()
    {
        parentRectTransform = transform.parent.GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();

        if (PlayerPrefs.HasKey(gameObject.name + "_PosX") && PlayerPrefs.HasKey(gameObject.name + "_PosY"))
        {
            float initialPosX = PlayerPrefs.GetFloat(gameObject.name + "_PosX");
            float initialPosY = PlayerPrefs.GetFloat(gameObject.name + "_PosY");
            initialPosition = new Vector2(initialPosX, initialPosY);
        }
        else
        {
            initialPosition = rectTransform.anchoredPosition;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");

        // 드래그 시작 시에는 부모 오브젝트의 상위로 이동시켜 다른 UI 요소들보다 앞에 표시되도록 합니다.
        transform.SetAsLastSibling();

        // 드래그 시작 시에 마우스 포인터와 UI 사이의 거리(offset)를 계산합니다.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out offset);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 드래그 중인 포인터 위치를 가져옵니다.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPos);

        // 마우스 포인터와 UI 사이의 거리(offset)를 더해줍니다.
        localPos += offset;

        // 부모 RectTransform 영역 내에서 위치를 제한합니다.
        Rect parentRect = parentRectTransform.rect;
        Rect rect = rectTransform.rect;
        localPos.x = Mathf.Clamp(localPos.x, parentRect.xMin + rect.width / 2, parentRect.xMax - rect.width / 2);
        localPos.y = Mathf.Clamp(localPos.y, parentRect.yMin + rect.height / 2, parentRect.yMax - rect.height / 2);

        // 로컬 좌표를 전역 좌표로 변환합니다.
        Vector3 position = parentRectTransform.TransformPoint(localPos);

        // 현재 위치를 설정합니다.
        rectTransform.position = position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");

        // 드롭한 위치를 로컬 좌표로 변환합니다.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPos);

        // 로컬 좌표를 부모 RectTransform 영역 내에서 제한합니다.
        Rect parentRect = parentRectTransform.rect;
        Rect rect = rectTransform.rect;
        localPos.x = Mathf.Clamp(localPos.x, parentRect.xMin + rect.width / 2, parentRect.xMax - rect.width / 2);
        localPos.y = Mathf.Clamp(localPos.y, parentRect.yMin + rect.height / 2, parentRect.yMax - rect.height / 2);

        // 부모 RectTransform 영역 내에서의 로컬 좌표를 전역 좌표로 변환합니다.
        Vector3 position = parentRectTransform.TransformPoint(localPos);

        // 현재 위치를 설정합니다.
        rectTransform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");

        // 드래그 종료 시에는 드롭한 위치를 로컬 좌표로 변환합니다.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPos);

        // 로컬 좌표를 부모 RectTransform 영역 내에서 제한합니다.
        Rect parentRect = parentRectTransform.rect;
        Rect rect = rectTransform.rect;
        localPos.x = Mathf.Clamp(localPos.x, parentRect.xMin + rect.width / 2, parentRect.xMax - rect.width / 2);
        localPos.y = Mathf.Clamp(localPos.y, parentRect.yMin + rect.height / 2, parentRect.yMax - rect.height / 2);

        // 부모 RectTransform 영역 내에서의 로컬 좌표를 전역 좌표로 변환합니다.
        Vector3 position = parentRectTransform.TransformPoint(localPos);

        // 현재 위치를 설정합니다.
        rectTransform.position = position;

        // 현재 위치를 PlayerPrefs에 저장합니다.
        PlayerPrefs.SetFloat(gameObject.name + "_PosX", rectTransform.anchoredPosition.x);
        PlayerPrefs.SetFloat(gameObject.name + "_PosY", rectTransform.anchoredPosition.y);
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = initialPosition;
    }
}
